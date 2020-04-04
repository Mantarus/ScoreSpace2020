using UnityEngine;

public class RoadMover : MonoBehaviour
{
    public float speed;
    public float respawnDistance;
    
    void Update()
    {
        transform.position += Vector3.back * (speed * Time.deltaTime);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RoadTrigger"))
        {
            transform.position += Vector3.forward * respawnDistance;
        }
    }
}
