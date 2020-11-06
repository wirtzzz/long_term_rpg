using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_condition : MonoBehaviour
{
    public List<zone> destinations;
    private int n_zone = 0;
    private int zone_number;
    public delegate void zone_cond_complete();
    public static event zone_cond_complete ZoneCondComplete; 
    private void Start()
    {
        zone_number = destinations.Count;
    }
    //public GameObject support;
    //public Vector3 destination_position;
    //public float radius;
    //private SphereCollider destination_zone;
    private void OnEnable()
    {
        zone.is_entered += ZoneReached;
    }
    private void OnDisable()
    {
        zone.is_entered -= ZoneReached;
    }
    private void ZoneReached()
    {
        n_zone++;
        if (n_zone == zone_number)
            ZoneCondComplete();
    }
}
