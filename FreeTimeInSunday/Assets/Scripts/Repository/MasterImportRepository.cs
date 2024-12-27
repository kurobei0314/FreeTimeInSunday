using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;

public class MasterImportRepository
{
    public async UniTask LoadEventDTO(Action<List<EventDTO>> callback)
    {
      var dataStore = Addressables.LoadAssetAsync<EventVODataStore>("Assets/Master/EventVODataStore");
      await dataStore;
      var eventDTOs = dataStore.Result.Select(vo => new EventDTO(vo)).ToList();
      callback?.Invoke(eventDTOs);
    }
}
