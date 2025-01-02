using UnityEngine;
using TMPro;

public class TimeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    public void Initialize(int afterElapsedTime)
    {
        var time = GameInfo.StartDayTime + afterElapsedTime;
        _text.text = time + ":00";
    }
}
