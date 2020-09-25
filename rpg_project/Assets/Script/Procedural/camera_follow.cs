using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour
{
    public Transform tgt;
    public float smooth_speed = 0.125f;
    public Vector3 offset;
    public bool camera_set = false;

    private void LateUpdate()
    {
        if (camera_set)
            transform.position = tgt.position + offset;
    }
}
