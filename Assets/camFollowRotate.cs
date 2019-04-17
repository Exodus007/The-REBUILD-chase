using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollowRotate : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [Range(0.01f, 1.0f)]
    public float rotationSpeed = 0.5f;
    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,target.rotation,rotationSpeed);
        }
    }
}
