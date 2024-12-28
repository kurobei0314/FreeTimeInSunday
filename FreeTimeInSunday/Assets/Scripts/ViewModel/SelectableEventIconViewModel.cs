using System.Collections.Generic;

public class SelectableEventIconViewModel
{
    private List<EventIconView> _selectableEventType;

    public SelectableEventIconViewModel()
    {
        _selectableEventType = new List<EventIconView>();
    }

    public void AddSelectableEventIconType(EventIconView iconType)
    {
        _selectableEventType.Add(iconType);
    }

    public void RemoveSelectableEventIconType(EventIconView iconType)
    {
        _selectableEventType.Remove(iconType);
    }
}
