using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainPresenter
{
    private ILookup<EventType, EventDTO> _eventLookUp;
    private  List<EventIconDTO> _eventIconDTOs;
    private PlayerModel _playerModel;
    private IUpdateMainViewDispatcher _dispatcher;

    public MainPresenter (ILookup<EventType, EventDTO> eventLookUp,
                          List<EventIconDTO> eventIconDTOs,
                          PlayerModel playerModel,
                          IUpdateMainViewDispatcher dispatcher)
    {
      _eventLookUp = eventLookUp;
      _eventIconDTOs = eventIconDTOs;
      _playerModel = playerModel;
      _dispatcher = dispatcher;
    }

    public EventType[] GetEventTypes(EventIconType eventIconType)
      => _eventIconDTOs.FirstOrDefault(dto => dto.EventIconType == eventIconType).EventTypes;
}
