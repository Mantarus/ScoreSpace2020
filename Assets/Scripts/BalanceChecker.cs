using UnityEngine;

public class BalanceChecker : MonoBehaviour
{
    public TruckMover truckMover;

    private bool _alive = true;

    private void OnTriggerExit(Collider other)
    {
        if (_alive && (other.gameObject.CompareTag("WalkingZone") || other.gameObject.CompareTag("StandZone")))
        {
            Die();
        }
    }

    public void Die()
    {
        _alive = false;
        truckMover.DeactivateAndDetachMan(false);
    }
    
}
