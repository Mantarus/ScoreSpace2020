using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform truck;
    public Transform roadTrigger;
    public float cameraSpeed;
    public Vector3 cameraOffset;
    public float roadTriggerOffset;

    private void FixedUpdate()
    {
        var cameraPos = transform.position;
        var truckPos = truck.position;
        var triggerPos = roadTrigger.position;
        
        var desiredPosition = truckPos + cameraOffset - new Vector3(truckPos.x / 2, 0, 0);
        var smoothedPosition = Vector3.Lerp(cameraPos, desiredPosition, cameraSpeed);
        
        transform.position = smoothedPosition;
        roadTrigger.position = new Vector3(0, triggerPos.y, cameraPos.z + roadTriggerOffset);
    }

}
