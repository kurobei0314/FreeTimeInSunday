using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DayResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Initialize(DayResultViewModel dayResultViewModel)
    {
        var textList = dayResultViewModel.ResultTextLists;
        _text.text = (textList.Count == 0) ? GameInfo.DayResultEmptyText : SetResultTextList(textList);
    }

    public string SetResultTextList(List<string> textList)
    {
        var text = string.Empty;
        for (var i = 0; i < textList.Count; i++) text += textList[i] + "\n";
        return text; 
    }
}
