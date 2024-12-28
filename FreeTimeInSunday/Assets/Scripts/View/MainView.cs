using UnityEngine;
using R3;
using R3.Triggers;

public class MainView : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerHPView _playerHPView;
    [SerializeField] private TextBoxView _textBoxView;
    [SerializeField] private TimeView _timeView;
    [SerializeField] private EventIconView[] _iconViews;

    void Start()
    {
        var iconViewModels = new SelectableEventIconViewModel();
        _playerView.OnTriggerEnter2DAsObservable().Where(col => col.gameObject.tag == "EventIcon")
        .Subscribe(col => 
        {
            iconViewModels.AddSelectableEventIconType(col.gameObject.GetComponent<EventIconView>());
        }).AddTo(this);

        _playerView.OnTriggerExit2DAsObservable().Where(col => col.gameObject.tag == "EventIcon")
        .Subscribe(col => 
        {
            iconViewModels.RemoveSelectableEventIconType(col.gameObject.GetComponent<EventIconView>());
        }).AddTo(this);
    }
}
