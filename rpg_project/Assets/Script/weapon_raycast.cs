using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_raycast : MonoBehaviour
{
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, transform.position, 1000f);
        Debug.DrawRay(transform.position, transform.position * 1000f, Color.red);
    }
}