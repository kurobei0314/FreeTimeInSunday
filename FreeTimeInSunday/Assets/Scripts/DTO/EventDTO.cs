using UnityEngine;

public class ConditionEvent
{
  private EventType _type;
  private int _time;
  public ConditionEvent(EventType type, int time)
  {
    _type = type;
    _time = time;
  }

  public EventType Type => _type;
  public int Time => _time;
}

public class EventDTO
{
    private EventVO _vo;
    private ConditionEvent[] _conditionEvents;
    public EventDTO(EventVO vo)
    {
      _vo = vo;
      _conditionEvents = new ConditionEvent[2];
      _conditionEvents[0] = new ConditionEvent(vo.ConditionEventType1, vo.ConditionEventTime1);
      _conditionEvents[1] = new ConditionEvent(vo.ConditionEventType2, vo.ConditionEventTime2);
    }
    public EventType Type => _vo.Type;
    public int Times => _vo.Times;
    public string Description => _vo.Description;
    public string ResultDescription => _vo.ResultDescription;
    public string HPConsumption => _vo.HPConsumption;
    public ConditionEvent[] ConditionEvents => _conditionEvents;
}
