using System.Collections.Generic;
using System.Linq;

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

    public SelectEventTypeDTO[] GetEventTypes(EventIconType eventIconType)
    {
      var eventTypes = _eventIconDTOs.FirstOrDefault(dto => dto.EventIconType == eventIconType).EventTypes;
      return eventTypes.Select(type => new SelectEventTypeDTO(type, _playerModel.IsSelectableEventType(_eventLookUp[type].ToList()),  _playerModel.GetHPConsumptionEventType(_eventLookUp[type].ToList()))).ToArray();
    }

    public void UpdatePlayerModel(EventType eventType)
    {
      var dto = _playerModel.UpdateBySelectedEvent(_eventLookUp[eventType].ToList());
      _dispatcher.UpdateViewByDecideEvent(dto);
    }

    public void RefreshElapsedTime()
      => _dispatcher.UpdateRefreshTime(_playerModel.RefreshElapsedTime());
}
