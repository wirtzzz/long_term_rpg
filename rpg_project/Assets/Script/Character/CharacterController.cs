using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject CharacterGO;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        //MOVEMENT
        if (Input.GetKey(KeyCode.Z))
        {
            CharacterGO.transform.Translate(Vector3.up / speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            CharacterGO.transform.Translate(Vector3.down / speed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            CharacterGO.transform.Translate(Vector3.left / speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            CharacterGO.transform.Translate(Vector3.right / speed);
        }

        //ATTACK
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("aaaah");
        }
    }
}
