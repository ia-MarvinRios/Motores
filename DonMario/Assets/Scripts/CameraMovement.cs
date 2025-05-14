using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float speed;
    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);
        transform.position = Vector3.MoveTowards(transform.position,
                                                targetPosition,
                                                speed * Time.deltaTime);
    }
}
