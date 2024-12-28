using UnityEngine;
using UnityEngine.InputSystem;
using R3;
using R3.Triggers;

public class PlayerView : MonoBehaviour
{
    private Vector3 _walkAxis;

    public void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => {
            this.transform.Translate(_walkAxis * Time.deltaTime);
        }).AddTo(this);
    }

    #region InputSystem
    public void OnMove(InputValue input)
    {
        var axis = input.Get<Vector2>();
        _walkAxis = new Vector3(axis.x, axis.y, 0.0f);
    }
    #endregion
}
