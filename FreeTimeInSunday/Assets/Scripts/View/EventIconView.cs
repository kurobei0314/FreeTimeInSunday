using UnityEngine;

public class EventIconView : MonoBehaviour
{
    [SerializeField] private EventIconType _eventIconType;
    [SerializeField] private Animator _animator;
    public EventIconType EventIconType => _eventIconType;

    public void Initialize()
    {

    }

    public void SetFocusIcon()
        => _animator.SetBool("Focus", true);

    public void SetUnFocusIcon()
        => _animator.SetBool("Focus", false);
}
