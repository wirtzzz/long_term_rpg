﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_controller : MonoBehaviour
{
    public NavMeshAgent m_nav_mesh_agent;
    public GameObject player;
    public Animator m_animator;

    private void Start()
    {
        m_nav_mesh_agent = this.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        m_nav_mesh_agent.destination = player.transform.position;
        if(m_nav_mesh_agent.velocity != Vector3.zero){
            m_animator.SetBool("run", true);
        }
        else
        {
            m_animator.SetBool("run", false);
        }
        
    }
}
