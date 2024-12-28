using UnityEngine;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public class StartGameController : MonoBehaviour
{
    void Start()
    {
        var services = new ServiceCollection();
        services.AddSingleton<PlayerPresenter>();
        services.BuildServiceProvider().GetService<PlayerPresenter>();
        LoadMaster((dtoLookup) => {

        });
    }

    private void LoadMaster(Action<ILookup<EventType , EventDTO>> callback)
    {
        var masterImport = new MasterImportRepository();
        masterImport.LoadEventDTOs((dto) => callback(dto.ToLookup(dto => dto.EventType)));
    }
}
