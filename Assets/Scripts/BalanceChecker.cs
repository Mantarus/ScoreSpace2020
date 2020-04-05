using UnityEngine;

public class BalanceChecker : MonoBehaviour
{
    public TruckMover truckMover;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WalkingZone"))
        {
            truckMover.DeactivateAndDetachMan(false);
        }
    }
}
