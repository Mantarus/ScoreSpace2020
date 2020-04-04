using System;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform truck;
    public Transform man;
    public Transform triggers;
    public float cameraSpeed;
    public Vector3 cameraOffset;
    public float triggersZOffset;

    private Transform _target;

    private void Start()
    {
        _target = truck;
    }

    private void FixedUpdate()
    {
        var cameraPos = transform.position;
        var targetPos = _target.position;
        var triggerPos = triggers.position;
        
        var desiredPosition = targetPos + cameraOffset - new Vector3(targetPos.x / 2, 0, 0);
        var smoothedPosition = Vector3.Lerp(cameraPos, desiredPosition, cameraSpeed);
        
        transform.position = smoothedPosition;
        triggers.position = new Vector3(0, triggerPos.y, cameraPos.z + triggersZOffset);
    }

    public void SwitchTarget()
    {
        _target = man;
    }

}
