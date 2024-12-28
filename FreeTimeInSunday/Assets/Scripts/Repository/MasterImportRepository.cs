using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

public class MasterImportRepository
{
    public async UniTask LoadEventDTOs(Action<List<EventDTO>, List<EventIconDTO>> callback)
    {
      var eventVOdataStore = Addressables.LoadAssetAsync<EventVODataStore>("Assets/Master/EventVODataStore");
      await eventVOdataStore;
      var eventDTOs = eventVOdataStore.Result.Items.Select(vo => new EventDTO(vo)).ToList();

      var eventIconVOdataStore = Addressables.LoadAssetAsync<EventIconVODataStore>("Assets/Master/EventIconVODataStore");
      await eventIconVOdataStore;
      var eventIconDTOs = eventIconVOdataStore.Result.Items.Select(vo => new EventIconDTO(vo)).ToList();
      callback?.Invoke(eventDTOs, eventIconDTOs);
    }
}
