using UnityEngine;
using UnityEngine.UI;

public class TruckMover : MonoBehaviour
{
    public float initialSpeed;
    public float minSpeed;
    public float maxSpeed;
    public float acceleration;
    public float turning;
    
    public Text coordsText;

    private Rigidbody _rb;
    private float _speed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.forward * initialSpeed;
        _speed = initialSpeed;
    }

    private void FixedUpdate()
    {
        var zValue = Input.GetAxis("Vertical") * acceleration;
        var xValue = Input.GetAxis("Horizontal") * turning;
        _speed += zValue * Time.deltaTime;
        _speed = Mathf.Clamp(_speed, minSpeed, maxSpeed);
        _rb.velocity = new Vector3(xValue , 0 , _speed);
        transform.rotation = Quaternion.LookRotation(_rb.velocity);
        UpdateText();
    }

    private void UpdateText()
    {
        var pos = transform.position;
        coordsText.text = $"X: {pos.x}\nY: {pos.y}\nZ: {pos.z}";
    }
}
