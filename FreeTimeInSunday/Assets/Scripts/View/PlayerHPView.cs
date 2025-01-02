using UnityEngine;
using UnityEngine.UI;

public class PlayerHPView : MonoBehaviour
{
    [SerializeField] private Image[] _hpIconArray;
    [SerializeField] private Sprite _hpImage;
    [SerializeField] private Sprite _noHpImage;
    
    public void Initialize(int hp)
    {
        for (var i = 0; i < _hpIconArray.Length; i++)
        {
            _hpIconArray[i].sprite =  (i < hp) ? _hpImage : _noHpImage;
        }
    }
}
