using UnityEngine;

public class RoadMover : MonoBehaviour
{
    public float respawnDistance;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GameZoneTrigger"))
        {
            transform.position += Vector3.forward * respawnDistance;
        }
    }
}
