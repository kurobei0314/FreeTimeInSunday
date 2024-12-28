using System;
using UnityEngine;

[Serializable]
public class EventIconVO
{
    [SerializeField]
    private int id;
    public int Id => id;

    [SerializeField]
    private EventIconType event_icon_type;
    public EventIconType EventIconType => event_icon_type;

    [SerializeField]
    private EventType event_type1;
    public EventType EventType1 => event_type1;

    [SerializeField]
    private EventType event_type2;
    public EventType EventType2 => event_type2;

    [SerializeField]
    private EventType event_type3;
    public EventType EventType3 => event_type3;
}
