using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextBoxView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SelectPanelView[] _selectPanels;
    private int _selectIndex; 
    private EventIconType[] _eventIconTypes;

    public void SetDescriptionText(string descriptionText)
    {
        _text.text = descriptionText;
        SetActiveFalseSelectPanel();
    }

    public void SetSelectableEventIconType(EventIconType[] eventIconTypes)
    {
        SetActiveTextBox(true);
        _selectIndex = 0;
        _eventIconTypes = eventIconTypes;
        _text.text = GameInfo.SelectableEventIconTypeTitleText;
        InitializeSelectPanels(GetSelectPanelsInfoArray(eventIconTypes));
    }

    public void SetSelectableEvent(Func<EventIconType, EventType[]> getEventTypeAction)
    {
        var eventTypes = getEventTypeAction(_eventIconTypes[_selectIndex]);
        _selectIndex = 0;
        _text.text = GameInfo.SelectableEventTypeTitleText;
        InitializeSelectPanels(GetSelectPanelsInfoArray(eventTypes));
    }

    public void SetActiveTextBox(bool isActive)
        => this.gameObject.SetActive(isActive);

    public void SetNextFocusSelection(int delta)
    {
        if (_selectIndex <= 0 || _selectIndex >= GetActiveTrueSelectPanelNum()) return;
        _selectPanels[_selectIndex].SetFocusStatus(false);
        _selectIndex += delta;
        _selectPanels[_selectIndex].SetFocusStatus(true);
    }

    private (string[] textArray, bool[] isSelectedArray) GetSelectPanelsInfoArray<T>(T[] types) where T : Enum
    {
        var isSelectedArray = new bool[types.Length];
        isSelectedArray[0] = true;
        SetActiveFalseSelectPanel();
        return (types.Select(type => type.ToString()).ToArray(), isSelectedArray);
    }

    private void InitializeSelectPanels((string[], bool[]) panelInfo)
    {
        var texts = panelInfo.Item1;
        var isSelectedArray = panelInfo.Item2;
        SetActiveFalseSelectPanel();
        for (var i = 0; i < texts.Length; i++)
        {
            _selectPanels[i].Initialize(texts[i].ToString(), isSelectedArray[i]);
            _selectPanels[i].gameObject.SetActive(true);
        }
    }

    private int GetActiveTrueSelectPanelNum()
    {
        var num = 0;
        for(var i = 0; i < _selectPanels.Length; i++)
        {
            if (_selectPanels[i].gameObject.activeSelf) num++;
        }
        return num;
    }

    private void SetActiveFalseSelectPanel()
    {
        for (var i = 0; i < _selectPanels.Length; i++) _selectPanels[i].gameObject.SetActive(false);
    }
}
