using TMPro;
using UnityEngine;

public class SelectPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Animator _animator;

    public void Initialize(string text, bool isSelected, bool isInteractive)
    {
        _text.text = text;
        SetFocusStatus(isSelected);
        SetInteractive(isInteractive);
    }

    public void SetInteractive(bool isInteractive)
    {
        //TODO: ボタン押せない時の表示変更処理
    }

    public void SetFocusStatus(bool isSelected)
        => _animator.SetBool("Focus", isSelected);
}
