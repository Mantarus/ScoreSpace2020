using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed;
    public AudioSource hitSound;
    public Spawner spawner;

    private Rigidbody _rb;
    private bool _active = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetRbConstraints();
    }

    private void SetRbConstraints()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    private void FixedUpdate()
    {
        if (_active)
        {
            _rb.velocity = transform.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        hitSound.Play();
        _active = false;
        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameZoneTrigger"))
        {
            ResetCarState();
            spawner.StashCar(gameObject);
        }
    }

    private void ResetCarState()
    {
        _active = true;
        SetRbConstraints();
    }
}
