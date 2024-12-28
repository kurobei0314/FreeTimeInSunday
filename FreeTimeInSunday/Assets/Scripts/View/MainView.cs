using UnityEngine;
using R3;
using R3.Triggers;
using System.Linq;
using UnityEngine.InputSystem;

public class MainView : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerHPView _playerHPView;
    [SerializeField] private TextBoxView _textBoxView;
    [SerializeField] private TimeView _timeView;
    [SerializeField] private EventIconView[] _iconViews;
    private SelectableEventIconViewModel _iconViewModels;
    private MainStateViewModel _mainStateViewModel;

    public void Initialize() 
    {
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
        }
    }

    public void OnUp()
    {
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.DecideEventIcon:
                break;
        }
    }
    public void OnDown()
    {
        switch (_mainStateViewModel.state)
        {
            case MainStateViewModel.State.DecideEventIcon:
                break;
        }
    }
    #endregion

    private void OnDecideForPlayerMoveState()
    {
        if (!_iconViewModels.IsExistedSelectableEventIconType()) return;
        _textBoxView.SetSelectableEventIconType(_iconViewModels.GetSelectableEventType());
    }

    private void OnDecideForDecideEventIconState()
    {
        // SetSelectableEvent();
    }

    public void Dispose()
    {

    }
}
