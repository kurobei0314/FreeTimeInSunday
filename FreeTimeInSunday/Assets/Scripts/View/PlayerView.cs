using UnityEngine;
using UnityEngine.InputSystem;
using R3;
using R3.Triggers;

public class PlayerView : MonoBehaviour
{
    private Vector3 _walkAxis;

    public void Initialize()
    {
        Observable.EveryUpdate().Subscribe(_ => {
            this.transform.Translate(_walkAxis * Time.deltaTime * 10);
        }).AddTo(this);
    }

    public void UpdateWalkAxis(Vector3 axis)
        => _walkAxis = axis;
}
