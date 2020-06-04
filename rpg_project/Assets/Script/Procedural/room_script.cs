using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_script : MonoBehaviour
{
    public GameObject player_start_position;
    public doorway[] doorWays;
    public MeshCollider mesh_collider;
    public enum RoomType{
        start,
        end,
        corridor,
        arena
    };
    public RoomType m_room_type;
    public Bounds room_bounds {
        get {return mesh_collider.bounds;}
    }
}
