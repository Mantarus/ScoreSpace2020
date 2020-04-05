using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float rotationOnCrash;

    private Rigidbody _rb;
    private float _speed;
    private bool _active = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _speed = Random.Range(minSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            _rb.velocity = transform.forward * _speed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        _active = false;
        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.None;
            // _rb.angularVelocity = Random.insideUnitCircle.normalized * rotationOnCrash;
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
