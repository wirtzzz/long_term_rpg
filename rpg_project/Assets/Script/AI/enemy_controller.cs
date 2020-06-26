using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_controller : MonoBehaviour
{
    public NavMeshAgent m_nav_mesh_agent;
    public GameObject player;
    public Animator m_animator;
    public Character enemy_character;
    private void Start()
    {
        m_nav_mesh_agent = this.GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        m_nav_mesh_agent.destination = player.transform.position;
        if (m_nav_mesh_agent.velocity != Vector3.zero)
        {
            m_animator.SetBool("run", true);
        }
        else
        {
            m_animator.SetBool("run", false);
            m_animator.SetTrigger("attack");
            //player.GetComponent<Character>().TakeDamages(enemy_character.m_career.m_strength);
        }
        enemy_character.transform.rotation = Quaternion.Inverse(Quaternion.LookRotation(player.transform.position));
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player " + hit.gameObject.name);
        }
    }
}
