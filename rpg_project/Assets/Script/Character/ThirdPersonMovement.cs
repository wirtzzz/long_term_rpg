using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController m_controller;
    public float speed = 6f;
    public Camera m_camera;
    public float rotation_speed = 4f;
    public Animator m_animator;
    public Character m_character;
    private Vector3 m_mouse_position;
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical);

        Plane player_plane = new Plane(Vector3.up, transform.position);
        Ray r = m_camera.ScreenPointToRay(Input.mousePosition);

        float hit_distance = 0.0f;
        if (player_plane.Raycast(r, out hit_distance))
        {
            Vector3 target_point = r.GetPoint(hit_distance);
            Quaternion target_rotation = Quaternion.LookRotation(target_point - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, target_rotation, rotation_speed * Time.deltaTime);
        }

        if (dir.magnitude >= 0.1f)
        {
            m_animator.SetBool("run", true);
            m_controller.Move(dir * speed * Time.deltaTime);
        }
        else
            m_animator.SetBool("run", false);

        if (Input.GetMouseButtonDown(0))
        {
            m_animator.SetTrigger("attack");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ennemy"))
        {
            Debug.Log("Enemmy " +hit.gameObject.name);
        }
    }
}
