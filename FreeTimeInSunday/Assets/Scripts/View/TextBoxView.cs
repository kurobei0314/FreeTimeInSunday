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
    private SelectEventTypeDTO[] _eventTypes;

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
        var interactables = new bool[_eventIconTypes.Length];
        for (var i = 0; i < interactables.Length; i++) interactables[i] = true;
        InitializeSelectPanels(GetSelectPanelsInfoArray(eventIconTypes, interactables));
    }

    public void SetSelectableEvent(Func<EventIconType, SelectEventTypeDTO[]> getEventTypeAction)
    {
        _eventTypes = getEventTypeAction(_eventIconTypes[_selectIndex]);
        _selectIndex = 0;
        _text.text = GameInfo.SelectableEventTypeTitleText;
        var interactables = new bool[_eventTypes.Length];
        for (var i = 0; i < interactables.Length; i++) interactables[i] = true;
        var info = GetSelectPanelsInfoArray(_eventTypes.Select(dto => dto.EventType).ToArray(), _eventTypes.Select(dto => dto.IsSelectable).ToArray());
        InitializeSelectPanels(info);
    }

    public void DecideEvent(Action<EventType> updateViewAction)
        => updateViewAction.Invoke(_eventTypes[_selectIndex].EventType);

    public void SetActiveTextBox(bool isActive)
        => this.gameObject.SetActive(isActive);

    public void SetNextFocusSelection(int delta)
    {
        if (_selectIndex <= 0 || _selectIndex >= GetActiveTrueSelectPanelNum()) return;
        _selectPanels[_selectIndex].SetFocusStatus(false);
        _selectIndex += delta;
        _selectPanels[_selectIndex].SetFocusStatus(true);
    }

    private (string[] textArray, bool[] isSelectedArray, bool[] interactables) GetSelectPanelsInfoArray<T>(T[] types, bool[] interactables) where T : Enum
    {
        var isSelectedArray = new bool[types.Length];
        isSelectedArray[0] = true;
        SetActiveFalseSelectPanel();
        return (types.Select(type => type.ToString()).ToArray(), isSelectedArray, interactables);
    }

    private void InitializeSelectPanels((string[], bool[], bool[]) panelInfo)
    {
        var texts = panelInfo.Item1;
        var isSelectedArray = panelInfo.Item2;
        var interactables = panelInfo.Item3;
        SetActiveFalseSelectPanel();
        for (var i = 0; i < texts.Length; i++)
        {
            _selectPanels[i].Initialize(texts[i].ToString(), isSelectedArray[i], interactables[i]);
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

    public void SetActiveFalseSelectPanel()
    {
        for (var i = 0; i < _selectPanels.Length; i++) _selectPanels[i].gameObject.SetActive(false);
    }
}
