using System;
using UnityEngine;

public class BalanceChecker : MonoBehaviour
{
    public TruckMover truckMover;
    public AudioSource hitSound;

    private bool _alive = true;

    private void OnTriggerExit(Collider other)
    {
        if (_alive && (other.gameObject.CompareTag("WalkingZone") || other.gameObject.CompareTag("StandZone")))
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_alive)
        {
            hitSound.volume = Mathf.Clamp01(other.relativeVelocity.magnitude / 100);
            hitSound.Play();
        }
    }

    private void Die()
    {
        _alive = false;
        truckMover.DeactivateAndDetachMan(false);
    }

    public bool IsAlive()
    {
        return _alive;
    }
    
}
