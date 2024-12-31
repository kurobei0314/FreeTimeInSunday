using UnityEngine;
using R3;
using R3.Triggers;
using System.Linq;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Playables;

public class MainView : MonoBehaviour, IUpdateMainViewDispatcher
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerHPView _playerHPView;
    [SerializeField] private TextBoxView _textBoxView;
    [SerializeField] private TimeView _timeView;
    [SerializeField] private EventIconView[] _iconViews;
    [SerializeField] private PlayableDirector _eventDirector;
    [SerializeField] private PlayableDirector _resultDirector;

    private SelectableEventIconViewModel _iconViewModels;
    private MainStateViewModel _mainStateViewModel;
    private DayResultViewModel _dayResultViewModel;
    private Func<EventIconType, SelectEventTypeDTO[]> _getEventTypeAction;
    private Action<EventType> _decideEventAction;

    public void Initialize( int elapsedTime,
                            Func<EventIconType, SelectEventTypeDTO[]> getEventTypeAction,
                            Action<EventType> decideEventAction) 
    {
        _getEventTypeAction = getEventTypeAction;
        _decideEventAction = decideEventAction;

        _textBoxView.SetActiveFalseSelectPanel();
        _timeView.Initialize(elapsedTime);
        _playerView.Initialize();

        _iconViewModels = new SelectableEventIconViewModel();
        _mainStateViewModel = new MainStateViewModel();
        _dayResultViewModel = new DayResultViewModel();

        _playerView.OnTriggerEnter2DAsObservable().Where(col => col.gameObject.tag == "EventIcon")
        .Subscribe(col => 
        {
            if (!col.gameObject.TryGetComponent<EventIconView>(out var iconView)) return;
            _iconViewModels.AddSelectableEventIconType(iconView);
            _iconViews.FirstOrDefault(icon => icon.EventIconType == iconView.EventIconType).SetFocusIcon();
        }).AddTo(this);

        _playerView.OnTriggerExit2DAsObservable().Where(col => col.gameObject.tag == "EventIcon")
        .Subscribe(col => 
        {
            if (!col.gameObject.TryGetComponent<EventIconView>(out var iconView)) return;
            _iconViewModels.RemoveSelectableEventIconType(iconView);
            _iconViews.FirstOrDefault(icon => icon.EventIconType == iconView.EventIconType).SetUnFocusIcon();
        }).AddTo(this);
    }

    #region InputSystem
    public void OnDecide(InputValue input)
    {
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.PlayerMove:
                OnDecideForPlayerMoveState();
                break;
            case MainStateViewModel.State.DecideEventIcon:
                OnDecideForDecideEventIconState();
                break;
            case MainStateViewModel.State.DecideEvent:
                OnDecideForDecideEventState();
                break;
        }
    }

    public void OnUp()
    {
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.DecideEventIcon:
            case MainStateViewModel.State.DecideEvent:
                _textBoxView.SetNextFocusSelection(1);
                break;
        }
    }
    public void OnDown()
    {
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.DecideEventIcon:
            case MainStateViewModel.State.DecideEvent:
                _textBoxView.SetNextFocusSelection(-1);
                break;
        }
    }
    public void OnCancel()
    {
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.DecideEventIcon:
            case MainStateViewModel.State.DecideEvent:
                _textBoxView.SetActiveTextBox(false);
                _mainStateViewModel.SetState(MainStateViewModel.State.PlayerMove);
                break;  
        }
    }

    public void OnMove(InputValue input)
    {
        var axis = input.Get<Vector2>();
        _playerView.UpdateWalkAxis(new Vector3(axis.x, axis.y, 0.0f));
    }
    #endregion

    private void OnDecideForPlayerMoveState()
    {
        if (!_iconViewModels.IsExistedSelectableEventIconType()) return;
        _textBoxView.SetSelectableEventIconType(_iconViewModels.GetSelectableEventType());
        _mainStateViewModel.SetState(MainStateViewModel.State.DecideEventIcon);
    }

    private void OnDecideForDecideEventIconState()
    {
        _textBoxView.SetSelectableEvent(_getEventTypeAction);
        _mainStateViewModel.SetState(MainStateViewModel.State.DecideEvent);
    }

    private void OnDecideForDecideEventState()
    {
        _mainStateViewModel.SetState(MainStateViewModel.State.Event);
        _textBoxView.DecideEvent((selectEventType) =>
        {
            _decideEventAction.Invoke(selectEventType);
        });
    }

    void IUpdateMainViewDispatcher.UpdateViewByDecideEvent(SelectedEventResultViewModel resultViewModel)
    {
        _eventDirector.Play();
        _playerHPView.Initialize(resultViewModel.AfterHP);
        _textBoxView.SetDescriptionText(resultViewModel.Description);
        _timeView.Initialize(resultViewModel.AfterElapsedTime);
        _dayResultViewModel.AddResultText(resultViewModel.ResultDescription);
        // TODO: 再生し終わった時に、1日が終わったかどうかをみて終わってたらanimationを再生するようにする
    }

    public void Dispose()
    {

    }
}
