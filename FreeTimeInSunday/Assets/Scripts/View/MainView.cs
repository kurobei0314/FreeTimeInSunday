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
    [SerializeField] private PlayableDirector _eventStartDirector;
    [SerializeField] private PlayableDirector _eventEndDirector;
    [SerializeField] private PlayableDirector _resultDirector;
    [SerializeField] private PlayableDirector _resultEndDirector;
    [SerializeField] private DayResultView _dayResultView;

    private SelectableEventIconViewModel _iconViewModels;
    private MainStateViewModel _mainStateViewModel;
    private DayResultViewModel _dayResultViewModel;
    private Func<EventIconType, SelectEventTypeDTO[]> _getEventTypeAction;
    private Action<EventType> _decideEventAction;
    private Action _refreshElapsedTime;
    private SelectedEventResultViewModel _selectedEventResultViewModel;

    public void Initialize( int elapsedTime,
                            Func<EventIconType, SelectEventTypeDTO[]> getEventTypeAction,
                            Action<EventType> decideEventAction,
                            Action refreshElapsedTime) 
    {
        _getEventTypeAction = getEventTypeAction;
        _decideEventAction = decideEventAction;
        _refreshElapsedTime = refreshElapsedTime;

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
        if (_mainStateViewModel == null) return;
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
            case MainStateViewModel.State.Event:
                OnDecideEventState();
                break;
            case MainStateViewModel.State.SelectCannotSelectPanel:
                OnDecideForCannotSelectPanelState();
                break;
            case MainStateViewModel.State.Result:
                OnDecideForResultState();
                break;
        }
    }

    public void OnUp()
    {
        if (_mainStateViewModel == null) return;
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.DecideEventIcon:
            case MainStateViewModel.State.DecideEvent:
                _textBoxView.SetNextFocusSelection(-1);
                break;
        }
    }
    public void OnDown()
    {
        if (_mainStateViewModel == null) return;
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.DecideEventIcon:
            case MainStateViewModel.State.DecideEvent:
                _textBoxView.SetNextFocusSelection(1);
                break;
        }
    }
    public void OnCancel()
    {
        if (_mainStateViewModel == null) return;
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
        if (_mainStateViewModel == null) return;
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.PlayerMove:
                var axis = input.Get<Vector2>();
                _playerView.UpdateWalkAxis(new Vector3(axis.x, axis.y, 0.0f));
                break;
        }
    }

    public void OnTwitter()
    {
        if (_mainStateViewModel == null) return;
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.Result:
                naichilab.UnityRoomTweet.Tweet ("freetimeinsunday", "日曜日を楽しく過ごした", "unityroom", "unity1week", "予定のない日曜日。");
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
        _textBoxView.DecideEvent((selectEventTypeDTO) =>
        {
            if (selectEventTypeDTO.IsSelectable)
            {
                _decideEventAction.Invoke(selectEventTypeDTO.EventType);
                return;
            }
            _textBoxView.SetDescriptionText(GameInfo.CannotSelectPanel);
            _mainStateViewModel.SetState(MainStateViewModel.State.SelectCannotSelectPanel);
        });
    }

    private void OnDecideEventState()
    {
        _mainStateViewModel.SetState(MainStateViewModel.State.Animation);
        _eventEndDirector.Play();
    }

    private void OnDecideForCannotSelectPanelState()
    {
        _textBoxView.ReturnSelectableEvent();
        _mainStateViewModel.SetState(MainStateViewModel.State.DecideEvent);
    }

    #region TimelineHandler
    public void UpdateStateForStartEventAnimation()
    {
        _mainStateViewModel.SetState(MainStateViewModel.State.Event);
    }
    public void UpdateStateForStartResultAnimation()
    {
        _mainStateViewModel.SetState(MainStateViewModel.State.Result);
    }
    public void UpdateForAnimationEnd()
    {
        _playerHPView.Initialize(_selectedEventResultViewModel.AfterHP);
        _timeView.Initialize(_selectedEventResultViewModel.AfterElapsedTime);
        _dayResultViewModel.AddResultText(_selectedEventResultViewModel.ResultDescription);
        if (_selectedEventResultViewModel.IsPassedDay) 
        {
            _dayResultView.Initialize(_dayResultViewModel);
            _mainStateViewModel.SetState(MainStateViewModel.State.Animation);
            _resultDirector.Play();
            return;
        }
        _mainStateViewModel.SetState(MainStateViewModel.State.PlayerMove);
    }

    public void UpdateForResultEndAnimationEnd()
    {
        _refreshElapsedTime();
        _mainStateViewModel.SetState(MainStateViewModel.State.PlayerMove);
    }
    #endregion

    private void OnDecideForResultState()
    {
        _mainStateViewModel.SetState(MainStateViewModel.State.Animation);
        _resultEndDirector.Play();
    }

    void IUpdateMainViewDispatcher.UpdateViewByDecideEvent(SelectedEventResultViewModel resultViewModel)
    {
        _mainStateViewModel.SetState(MainStateViewModel.State.Animation);
        _eventStartDirector.Play();
        _selectedEventResultViewModel = resultViewModel;
        _textBoxView.SetDescriptionText(_selectedEventResultViewModel.Description);
    }

    void IUpdateMainViewDispatcher.UpdateRefreshTime(int refreshTime)
    {
        _timeView.Initialize(refreshTime);
    }

    public void Dispose()
    {

    }
}
