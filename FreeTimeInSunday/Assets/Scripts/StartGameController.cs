using UnityEngine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;

public class StartGameController : MonoBehaviour
{
    [SerializeField] private MainView _mainView;
    [SerializeField] private EventVODataStore _eventVODataStore;
    [SerializeField] private EventIconVODataStore _eventIconVODataStore;

    void Start()
    {
        var services = new ServiceCollection();
        services.AddSingleton<MainPresenter>();
        services.AddSingleton<PlayerModel>();
        var playerModel = services.BuildServiceProvider().GetService<PlayerModel>();
        // services.BuildServiceProvider().GetService<MainPresenter>();

        LoadMaster((eventDTOLookup, eventIconDTO) =>
        {
            var mainPresenter = new MainPresenter(eventDTOLookup, eventIconDTO, playerModel, _mainView);
            _mainView.Initialize( playerModel.ElapsedTime,
                                 (iconType) => mainPresenter.GetEventTypes(iconType), 
                                 (eventType) => mainPresenter.UpdatePlayerModel(eventType),
                                 () => mainPresenter.RefreshElapsedTime());
        });
    }

    private void LoadMaster(Action<ILookup<EventType, EventDTO>, List<EventIconDTO>> callback)
    {
        var eventDTOs = _eventVODataStore.Items.Select(vo => new EventDTO(vo)).ToList();
        var eventIconDTO = _eventIconVODataStore.Items.Select(vo => new EventIconDTO(vo)).ToList();
        var masterImport = new MasterImportRepository();
        // var _ = masterImport.LoadEventDTOs((eventDTO, eventIconDTO) => {
        callback(eventDTOs.ToLookup(dto => dto.EventType), eventIconDTO);
        // });
    }
}
