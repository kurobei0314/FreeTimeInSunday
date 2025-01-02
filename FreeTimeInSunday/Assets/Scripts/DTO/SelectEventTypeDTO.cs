using UnityEngine;

public class SelectEventTypeDTO
{
    private EventType _eventType;
    private bool _isSelectable;
    private int _num;

    public SelectEventTypeDTO(EventType eventType, bool isSelectable, int num)
    {
      _eventType = eventType;
      _isSelectable = isSelectable;
      _num = num;
    }

    public EventType EventType => _eventType;
    public bool IsSelectable => _isSelectable;
    public int Num => _num;

}
