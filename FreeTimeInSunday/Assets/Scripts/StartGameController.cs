using UnityEngine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class StartGameController : MonoBehaviour
{
    void Start()
    {
        var services = new ServiceCollection();
        services.AddSingleton<PlayerPresenter>();
        services.BuildServiceProvider().GetService<PlayerPresenter>();
        LoadMaster((eventDTOLookup, eventIconDTO) => {
            
        });
    }

    private void LoadMaster(Action<ILookup<EventType , EventDTO>, List<EventIconDTO>> callback)
    {
        var masterImport = new MasterImportRepository();
        var _ = masterImport.LoadEventDTOs((eventDTO, eventIconDTO) => {
            callback(eventDTO.ToLookup(dto => dto.EventType), eventIconDTO);
        });
    }
}
