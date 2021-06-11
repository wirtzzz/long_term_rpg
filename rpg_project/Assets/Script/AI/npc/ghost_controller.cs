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
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("non");
        if (hit.gameObject.tag == "weapon")
        {
            Debug.Log("I AM DEAD");
        }
    }

}
