public class EventIconDTO
{
    private EventIconVO _vo;
    private EventType[] _conditionEvents;
    public EventIconDTO(EventIconVO vo)
    {
      _vo = vo;
      _conditionEvents = new EventType[3];
      _conditionEvents[0] = vo.EventType1;
      _conditionEvents[1] = vo.EventType2;
      _conditionEvents[2] = vo.EventType3;
    }
    public EventIconType EventIconType => _vo.EventIconType;
    public EventType[] EventTypes => _conditionEvents;
}
