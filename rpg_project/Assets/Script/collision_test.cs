using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_test : MonoBehaviour
{
    public BoxCollider BC1;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("BC1");
    }
}
