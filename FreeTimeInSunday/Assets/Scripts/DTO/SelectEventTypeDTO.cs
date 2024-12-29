using UnityEngine;

public class SelectEventTypeDTO
{
    private EventType _eventType;
    private bool _isSelectable;

    public SelectEventTypeDTO(EventType eventType, bool isSelectable)
    {
      _eventType = eventType;
      _isSelectable = isSelectable;
    }

    public EventType EventType => _eventType;
    public bool IsSelectable => _isSelectable;
}
