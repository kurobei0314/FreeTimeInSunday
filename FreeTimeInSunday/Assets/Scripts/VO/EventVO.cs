using System;
using UnityEngine;

[Serializable]
public class EventVO
{
    [SerializeField]
    private int id;
    public int Id => id;
    
    [SerializeField]
    private EventType event_type;
    public EventType EventType => event_type;

    [SerializeField]
    private int times;
    public int Times => times;

    [SerializeField]
    private string description;
    public string Description => description;

    [SerializeField]
    private string result_description;
    public string ResultDescription => result_description;

    [SerializeField]
    private string hp_consumption;
    public string HPConsumption => hp_consumption;

    [SerializeField]
    private EventType condition_event_type1;
    public EventType ConditionEventType1 => condition_event_type1;

    [SerializeField]
    private int condition_event_time1;
    public int ConditionEventTime1 => condition_event_time1;

    [SerializeField]
    private EventType condition_event_type2;
    public EventType ConditionEventType2 => condition_event_type2;

    [SerializeField]
    private int condition_event_time2;
    public int ConditionEventTime2 => condition_event_time2;
}
