using System.Collections.Generic;
using System.Linq;
public class EventIconDTO
{
    private EventIconVO _vo;
    private EventType[] _conditionEvents;
    public EventIconDTO(EventIconVO vo)
    {
      _vo = vo;
      var conditionEventLists = new List<EventType>();
      conditionEventLists.Add(vo.EventType1);
      conditionEventLists.Add(vo.EventType2);
      conditionEventLists.Add(vo.EventType3);
      _conditionEvents = conditionEventLists.Where(type => type != EventType.なし).ToArray();
    }
    public EventIconType EventIconType => _vo.EventIconType;
    public EventType[] EventTypes => _conditionEvents;
}
