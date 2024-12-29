using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerModel
{
    private int _hp;
    public int HP => _hp;
    private int _elapsedTime;
    private Dictionary<EventType, int> _eventTimeDic;
    private List<int> _happenedEventId;

    public PlayerModel()
    {
        _hp = GameInfo.PlayerHP;
        _elapsedTime = 0;
        _eventTimeDic = new Dictionary<EventType,int>();
        _happenedEventId = new List<int>();

        foreach (var eventType in Enum.GetValues(typeof(EventType)).Cast<EventType>())
        {
            _eventTimeDic.Add(eventType, 0);
        }
    }

    public int AddElapsedTime(int time)
    {
        _elapsedTime += time;
        return _elapsedTime;
    }

    public bool IsFinishDay()
        => _elapsedTime >= GameInfo.DayTime;

    public int AddHP(int hp)
    {
        _hp += hp;
        return _hp;
    }

    public bool IsSelectableEventType(List<EventDTO> eventDTOs)
    {
        var nextEventDTO = GetNextEventDTO(eventDTOs);
        return nextEventDTO.HPConsumption <= _hp; 
    }
    private EventDTO GetNextEventDTO(List<EventDTO> eventDTOs)
    {
        var list = new List<EventDTO>();
        foreach (var eventDTO in eventDTOs)
        {
            if (CanHappenEvent(eventDTO)) list.Add(eventDTO);
        }
        var sortList = list.OrderBy(dto => dto.Times).ThenBy(dto => dto.IsRepeat);
        return sortList.FirstOrDefault();
    }

    private bool CanHappenEvent(EventDTO eventDTO)
    {
        if (!eventDTO.IsRepeat && _happenedEventId.Contains(eventDTO.Id)) return false;
        foreach (var conditionDTO in eventDTO.ConditionEvents)
        {
            if (_eventTimeDic[conditionDTO.Type] < conditionDTO.Time) return false;
        }
        return true;
    }
}
