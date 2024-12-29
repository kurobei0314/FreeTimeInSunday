using System;
using System.Collections.Generic;
using System.Linq;

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
      var conditionEventLists = new List<ConditionEvent>();
      conditionEventLists.Add(new ConditionEvent(vo.ConditionEventType1, vo.ConditionEventTime1));
      conditionEventLists.Add(new ConditionEvent(vo.ConditionEventType2, vo.ConditionEventTime2));
      _conditionEvents = conditionEventLists.Where(condition => condition.Type != EventType.なし).ToArray();
    }
    public int Id => _vo.Id;
    public EventType EventType => _vo.EventType;
    public int Times => _vo.Times;
    public bool IsRepeat => Convert.ToBoolean(_vo.IsRepeat);
    public string Description => _vo.Description;
    public string ResultDescription => _vo.ResultDescription;
    public int HPConsumption => _vo.HPConsumption;
    public int HourConsumption => _vo.HourConsumption;
    public ConditionEvent[] ConditionEvents => _conditionEvents;
}
