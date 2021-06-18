using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_controller : npc_management
{
    private const float GHOST_ALTITUDE = 0.0f;

    void Awake()
    {
        //this.gameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + GHOST_ALTITUDE, this.transform.position.z);
    }

    private void Update()
    {
        if(this.GetComponent<Character>().m_pv <= 0 && m_Current_State == CharState.alive)
        {
            this.GetComponent<Animator>().SetTrigger("DEATH");
            behaviour = NPCBehaviour.Static;
            Debug.Log("I AM DEAD");
            agent.isStopped = true;
            this.gameObject.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
        CheckPlayerPosition();
    }
    public void CheckPlayerPosition()
    {
        Vector3 player_position = gameManager.instance.player.transform.position;
        if(Vector3.Distance(player_position, this.transform.position)<= 10)
        {
            agent.destination = player_position;
            this.m_vigilance_state = VigilanceState.Attacking;

        }
    }
}
