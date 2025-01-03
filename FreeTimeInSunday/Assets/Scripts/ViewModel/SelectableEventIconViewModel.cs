using System.Collections.Generic;
using System.Linq;

public class SelectableEventIconViewModel
{
    private List<EventIconView> _selectableEventType;

    public SelectableEventIconViewModel()
    {
        _selectableEventType = new List<EventIconView>();
    }

    public void AddSelectableEventIconType(EventIconView iconType)
        => _selectableEventType.Add(iconType);

    public void RemoveSelectableEventIconType(EventIconView iconType)
        => _selectableEventType.Remove(iconType);

    public bool IsExistedSelectableEventIconType()
        =>  _selectableEventType.Count != 0;
    public EventIconType[] GetSelectableEventType()
        => _selectableEventType.Select(view => view.EventIconType).ToArray();
}
