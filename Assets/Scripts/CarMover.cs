using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed;
    public float rotationOnCrash;

    private Rigidbody _rb;
    private bool _active = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            _rb.velocity = Vector3.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        _active = false;
        _rb.constraints = RigidbodyConstraints.None;
        // _rb.angularVelocity = Random.insideUnitCircle.normalized * rotationOnCrash;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameZoneTrigger"))
        {
            Destroy(gameObject);
        }
    }
}
