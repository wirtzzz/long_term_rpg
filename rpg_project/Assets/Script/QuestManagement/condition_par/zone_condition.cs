using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_condition : MonoBehaviour
{
    public GameObject support;
    public Vector3 destination_position;
    public float radius;
    private SphereCollider destination_zone;
    private void OnEnable()
    {
        zone_reached.is_entered += ZoneReached;
    }
    private void OnDisable()
    {
        zone_reached.is_entered -= ZoneReached;
    }
    private void ZoneReached()
    {
        //next zone
    }
}
