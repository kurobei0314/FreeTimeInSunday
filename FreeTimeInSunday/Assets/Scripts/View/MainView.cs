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
    private SelectableEventIconViewModel _iconViewModels;
    private MainStateViewModel _mainStateViewModel;
    private Func<EventIconType, SelectEventTypeDTO[]> _getEventTypeAction;

    public void Initialize(Func<EventIconType, SelectEventTypeDTO[]> getEventTypeAction,
                            Func<EventType> getFinishedEventResultAction) 
    {
        _getEventTypeAction = getEventTypeAction;

        _iconViewModels = new SelectableEventIconViewModel();
        _mainStateViewModel = new MainStateViewModel();

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
            _eventDirector.Play();
        });
    }

    public void Dispose()
    {

    }
}
