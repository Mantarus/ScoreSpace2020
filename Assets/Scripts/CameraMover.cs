using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform truck;
    public Transform man;
    public Transform roadTrigger;
    public float cameraSpeed;
    public Vector3 cameraOffset;
    public float roadTriggerOffset;

    private Transform _target;

    private void Start()
    {
        _target = truck;
    }

    private void FixedUpdate()
    {
        var cameraPos = transform.position;
        var targetPos = _target.position;
        var triggerPos = roadTrigger.position;
        
        var desiredPosition = targetPos + cameraOffset - new Vector3(targetPos.x / 2, 0, 0);
        var smoothedPosition = Vector3.Lerp(cameraPos, desiredPosition, cameraSpeed);
        
        transform.position = smoothedPosition;
        roadTrigger.position = new Vector3(0, triggerPos.y, cameraPos.z + roadTriggerOffset);
    }

    public void SwitchTarget()
    {
        _target = man;
    }

}
