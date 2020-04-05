using UnityEngine;
using UnityEngine.UI;

public class TruckMover : MonoBehaviour
{
    public float initialSpeed;
    public float minSpeed;
    public float maxSpeed;
    public float acceleration;
    public float turning;
    public float backTurning;
    public float rotationOnCrash;
    
    public GameObject man;
    public float ejectMultiplier;
    
    public CameraMover cameraMover;
    
    public Text coordsText;

    private Rigidbody _rb;
    private Rigidbody _manRb;
    private bool _active = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _manRb = man.GetComponent<Rigidbody>();
        
        _rb.velocity = Vector3.forward * initialSpeed;
        _manRb.velocity = Vector3.forward * initialSpeed;
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            var actualAcceleration = Input.GetAxis("Vertical") * acceleration;
            if (_rb.velocity.z >= maxSpeed && actualAcceleration > 0) actualAcceleration = 0;
            if (_rb.velocity.z <= minSpeed && actualAcceleration < 0) actualAcceleration = 0;
            
            var actualTurning = Input.GetAxis("Horizontal") * turning;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01)
            {
                actualTurning = -_rb.velocity.x * backTurning;
            }

            _rb.AddForce(Vector3.forward * actualAcceleration, ForceMode.Acceleration);
            _manRb.AddForce(Vector3.forward * actualAcceleration, ForceMode.Acceleration);
            
            _rb.AddForce(Vector3.right * actualTurning, ForceMode.Acceleration);
            _manRb.AddForce(Vector3.right * actualTurning, ForceMode.Acceleration);
            
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
        if (!other.gameObject.CompareTag("Man"))
        {
            _active = false;
            _rb.constraints = RigidbodyConstraints.None;
            // _rb.angularVelocity = Random.insideUnitCircle.normalized * rotationOnCrash;
            Time.timeScale = 0.5f;

            if (man != null && _manRb != null)
            {
                var speed = _rb.velocity.z;
                
                cameraMover.SwitchTarget();
                man.transform.parent = null;
                _manRb.isKinematic = false;
                _manRb.constraints = RigidbodyConstraints.None;
                _manRb.velocity = Vector3.up * (ejectMultiplier * speed) + Vector3.forward * speed;
                _manRb.angularVelocity = Random.insideUnitCircle.normalized * rotationOnCrash;

                man = null;
                _manRb = null;
            }
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
