using UnityEngine;
using UnityEngine.UI;

public class TruckMover : MonoBehaviour
{
    public float initialSpeed;
    public float minSpeed;
    public float maxSpeed;
    public float acceleration;
    public float turning;
    public float rotationOnCrash;
    public GameObject man;
    public float ejectMultiplier;
    public CameraMover cameraMover;
    
    public Text coordsText;

    private Rigidbody _rb;
    private Rigidbody _manRb;
    private float _speed;
    private bool _active = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = Vector3.forward * initialSpeed;
        _speed = initialSpeed;
        _manRb = man.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            var zValue = Input.GetAxis("Vertical") * acceleration;
            var xValue = Input.GetAxis("Horizontal") * turning;
            _speed += zValue * Time.deltaTime;
            _speed = Mathf.Clamp(_speed, minSpeed, maxSpeed);
            _rb.velocity = new Vector3(xValue, 0, _speed);
            transform.rotation = Quaternion.LookRotation(_rb.velocity);
        }

        UpdateText();
    }

    private void UpdateText()
    {
        var pos = transform.position;
        coordsText.text = $"X: {pos.x}\nY: {pos.y}\nZ: {pos.z}";
    }

    private void OnCollisionEnter(Collision other)
    {
        _active = false;
        _rb.constraints = RigidbodyConstraints.None;
        // _rb.angularVelocity = Random.insideUnitCircle.normalized * rotationOnCrash;
        // Time.timeScale = 0.3f;

        if (man != null && _manRb != null)
        {
            cameraMover.SwitchTarget();
            man.transform.parent = null;
            _manRb.isKinematic = false;
            _manRb.constraints = RigidbodyConstraints.None;
            _manRb.velocity = Vector3.up * (ejectMultiplier * _speed) + Vector3.forward * _speed;
            _manRb.angularVelocity = Random.insideUnitCircle.normalized * rotationOnCrash;

            man = null;
            _manRb = null;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameZoneTrigger"))
        {
            Destroy(gameObject);
        }
    }
}
