using UnityEngine;
using UnityEngine.UI;

public class ManWalker : MonoBehaviour
{

    public Rigidbody truck;
    public Text debugText;
    public float drunkFactor;

    private Vector3 _lastTruckVelocity;
    private Vector3 _truckAcceleration;

    private Vector3 _accumulatedInertia;

    private void Start()
    {
        _lastTruckVelocity = truck.velocity;
    }

    private void FixedUpdate()
    {
        CalcAcceleration();
        var inertia = GetInertia();
        _accumulatedInertia += inertia * Time.fixedDeltaTime;
        transform.localPosition += _accumulatedInertia * (drunkFactor * Time.fixedDeltaTime);
    }

    private void CalcAcceleration()
    {
        var curTruckVelocity = truck.velocity;
        _truckAcceleration = (curTruckVelocity - _lastTruckVelocity) / Time.fixedDeltaTime;
        _lastTruckVelocity = curTruckVelocity;
    }

    private Vector3 GetInertia()
    {
        var vertInertia = -_truckAcceleration;
        var horizontalInertia = new Vector3(Input.GetAxis("Horizontal") * -10, 0, 0);
        var inertia = vertInertia + horizontalInertia;
        debugText.text = $"Inertia: {inertia}";
        return inertia;
    }
    
}
