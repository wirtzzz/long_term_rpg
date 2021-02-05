using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public Transform cam;
    public float turn_smooth_velocity = 1f;
    public float turn_smooth_time = 0.1f;
    private float horizontal, vertical;
    private Vector3 direction;
    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float tgt_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tgt_angle, ref turn_smooth_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 move_dir = Quaternion.Euler(0f, tgt_angle, 0f)*Vector3.forward;
            controller.Move(move_dir.normalized * speed * Time.deltaTime);
        }
    }
}