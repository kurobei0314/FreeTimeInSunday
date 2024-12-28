using UnityEngine;
using Qitz.DataUtil;

[CreateAssetMenu]
public class EventIconVODataStore : BaseDataStore<EventIconVO>
{
    [ContextMenu("サーバーからデータを読み込む")]
    protected override void LoadDataFromServer()
    {
        base.LoadDataFromServer();
    }
}

