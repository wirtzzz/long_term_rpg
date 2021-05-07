using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public float initial_speed = 5f;
    private float speed;
    public Transform cam;
    public Animator m_animator;
    public float turn_smooth_velocity = 1f;
    public float turn_smooth_time = 0.1f;
    private float horizontal, vertical;
    private Vector3 direction;
    private Vector3 move_dir;
    private float gravity_value = 2500f;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 2 * initial_speed;
        }
        else
            speed = initial_speed;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float tgt_angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tgt_angle, ref turn_smooth_velocity, turn_smooth_time);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            move_dir = Quaternion.Euler(0f, tgt_angle, 0f) * Vector3.forward;
            m_animator.SetBool("run", true);
        }
        else
            m_animator.SetBool("run", false);
        //Ajout gravité
        move_dir = new Vector3(move_dir.x, move_dir.y - 1f, move_dir.z);
        //Move final du player
        controller.Move(move_dir.normalized * speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("attack");
        }
    }
}