using UnityEngine;
using UnityEngine.UI;

public class TruckMover : MonoBehaviour
{
    public float initialSpeed;
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

    private void Update()
    {
        var zValue = Input.GetAxis("Vertical") * acceleration;
        var xValue = Input.GetAxis("Horizontal") * turning;
        var delta = Time.deltaTime;
        _speed += zValue * delta;
        _rb.velocity += new Vector3(xValue * delta, 0 , _speed * delta);
        UpdateText();
    }

    private void UpdateText()
    {
        var pos = transform.position;
        coordsText.text = $"X: {pos.x}\nY: {pos.y}\nZ: {pos.z}";
    }
}
