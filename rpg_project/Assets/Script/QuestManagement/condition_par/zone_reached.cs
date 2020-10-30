using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_reached : MonoBehaviour
{
    private SphereCollider collider_box;
    private GameObject player;
    public delegate void ZoneEntered();
    public static event ZoneEntered is_entered;
    public zone_reached(Vector3 position, float m_radius, GameObject player)
    {
        this.collider_box = this.gameObject.AddComponent<SphereCollider>();
        collider_box.center = position;
        collider_box.radius = m_radius;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
            is_entered();
    }
}
