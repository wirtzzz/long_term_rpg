using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_controller : MonoBehaviour
{
    public NavMeshAgent m_nav_mesh_agent;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        m_nav_mesh_agent.destination = player.transform.position;
    }
}
