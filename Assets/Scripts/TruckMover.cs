using UnityEngine;
using UnityEngine.UI;

public class TruckMover : MonoBehaviour
{
    public float initialSpeed;
    public float minSpeed;
    public float maxSpeed;
    public float acceleration;
    public float incrementalAcceleration;
    public float turning;
    public float backTurning;
    public float rotationOnCrash;
    
    public GameObject man;
    public float manInertiaResistance;
    public float ejectMultiplier;

    public CameraMover cameraMover;
    
    public Text speedText;
    public Text throttleText;

    private Rigidbody _rb;
    private Rigidbody _manRb;
    private bool _active = true;
    private bool _manDetached = false;

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
            Move();
            UpdateUI();
        }
    }

    private void Move()
    {
        ApplyIncrementalAcceleration();
            
        var actualAcceleration = Input.GetAxis("Vertical") * acceleration;
        if (_rb.velocity.z >= maxSpeed && actualAcceleration > 0) actualAcceleration = 0;
        if (_rb.velocity.z <= minSpeed && actualAcceleration < 0) actualAcceleration = 0;
            
        var actualTurning = Input.GetAxis("Horizontal") * turning;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01)
        {
            actualTurning = -_rb.velocity.x * backTurning;
        }

        _rb.AddForce(Vector3.forward * actualAcceleration, ForceMode.Acceleration);
        _manRb.AddForce(Vector3.forward * (actualAcceleration * manInertiaResistance), ForceMode.Acceleration);
            
        _rb.AddForce(Vector3.right * actualTurning, ForceMode.Acceleration);
        _manRb.AddForce(Vector3.right * (actualTurning * manInertiaResistance), ForceMode.Acceleration);

        transform.rotation = Quaternion.LookRotation(_rb.velocity);
    }

    private void ApplyIncrementalAcceleration()
    {
        var speedIncrement = incrementalAcceleration * Time.fixedDeltaTime;
        
        _rb.AddForce(Vector3.forward * speedIncrement, ForceMode.VelocityChange);
        _manRb.AddForce(Vector3.forward * speedIncrement, ForceMode.VelocityChange);

        minSpeed += speedIncrement;
        maxSpeed += speedIncrement;
    }

    private void UpdateUI()
    {
        var speed = (int)_rb.velocity.z;
        var throttle = (int)Mathf.Clamp((speed - minSpeed) / (maxSpeed - minSpeed) * 100, 0, 100);

        speedText.text = $"{speed}mph";
        throttleText.text = $"PWR: {throttle}%";
    }

    private void HideUI()
    {
        speedText.gameObject.SetActive(false);
        throttleText.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_active || other.gameObject.CompareTag("Man"))
        {
            return;
        }

        DeactivateAndDetachMan(true);
    }

    public void DeactivateAndDetachMan(bool eject)
    {
        if (_manDetached)
        {
            return;
        }
        
        //Deactivate truck
        _active = false;
        _rb.constraints = RigidbodyConstraints.None;
        HideUI();
        // _rb.angularVelocity = Random.insideUnitCircle.normalized * rotationOnCrash;
        
        //Switch camera to man and slow time down
        cameraMover.SwitchTarget();
        Time.timeScale = 0.5f;

        //Detach man rigidbody from the truck and reset all constraints
        man.transform.parent = null;
        _manRb.isKinematic = false;
        _manRb.constraints = RigidbodyConstraints.None;
            
        //Add throw impulse to man if needed
        if (eject)
        {
            var ejectSpeed = _rb.velocity.magnitude;
            // _manRb.velocity = Vector3.up * (ejectMultiplier * ejectSpeed) + Vector3.forward * ejectSpeed;
            _manRb.AddForce(Vector3.up * (ejectMultiplier * ejectSpeed), ForceMode.VelocityChange);
            _manRb.AddTorque(Random.insideUnitCircle.normalized * rotationOnCrash, ForceMode.VelocityChange);
        }

        //Set flag that man is detached and drop references to him
        man = null;
        _manRb = null;
        _manDetached = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameZoneTrigger"))
        {
            Destroy(gameObject);
        }
    }
}
