using UnityEngine;
using Qitz.DataUtil;

[CreateAssetMenu]
public class EventVODataStore : BaseDataStore<EventVO>
{
    [ContextMenu("サーバーからデータを読み込む")]
    protected override void LoadDataFromServer()
    {
        base.LoadDataFromServer();
    }
}
