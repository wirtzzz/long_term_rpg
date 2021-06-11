using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_controller : MonoBehaviour
{
    public NavMeshAgent m_nav_mesh_agent;

    protected void Start()
    {
 
        m_nav_mesh_agent = this.GetComponent<NavMeshAgent>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("non");
        if (hit.gameObject.tag == "weapon")
        {
            Debug.Log("I AM DEAD");
        }
    }

}
