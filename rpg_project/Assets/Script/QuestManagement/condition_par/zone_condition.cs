using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_condition : MonoBehaviour
{
    public GameObject support;
    public Vector3 destination_position;
    public float radius;
    private SphereCollider destination_zone;

    public void BuildSphere()
    {
        destination_zone = support.AddComponent<SphereCollider>();
    }
}
