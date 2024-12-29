using TMPro;
using UnityEngine;

public class SelectPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Animator _animator;

    public void Initialize(string text, bool isSelected)
    {
        _text.text = text;
        SetFocusStatus(isSelected);
    }

    public void SetFocusStatus(bool isSelected)
        => _animator.SetBool("Focus", isSelected);
}
