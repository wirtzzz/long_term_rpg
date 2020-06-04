using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController m_controller;
    public float speed = 6f;
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(horizontal, 0f, vertical);
        if (dir.magnitude >= 0.1f)
        {
            m_controller.Move(dir * speed * Time.deltaTime);
        }
    }
}
