using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.AI;

public class lvl_builder : MonoBehaviour
{
    //PUBLIC /!\ (╯°□°）╯︵ ┻━┻)
    public room_script start_room_prefab, end_room_prefab;
    public List<room_script> room_prefabs = new List<room_script>();
    public Vector2 i_r = new Vector2(5, 5);
    public float overlap;
    public GameObject player;
    public GameObject enemy;
    public Camera m_camera;
    public NavMeshSurface m_surface;

    //PRIVATE ┬─┬ ノ( ゜-゜ノ)
    private room_script start_room;
    private LayerMask room_layer_mask;
    private end_room end_room;
    private List<room_script> placed_room = new List<room_script>();
    private List<doorway> available_doorway = new List<doorway>();

    private void Start()
    {
        room_layer_mask = LayerMask.GetMask("room");
        StartCoroutine("GenerateLevel");
    }

    private void PlaceStartRoom()
    {
        GameObject temp_start_room = Instantiate(start_room_prefab).gameObject;

        start_room = temp_start_room.GetComponent<room_script>();
        start_room.transform.parent = this.transform;

        AddDoorwaysToList(start_room, ref available_doorway);

        start_room.transform.rotation = Quaternion.identity;
        start_room.transform.position = Vector3.zero;
        placed_room.Add(start_room);

        GameObject temp_player_object = Instantiate(player, start_room.player_start_position.transform.position, start_room.player_start_position.transform.rotation);
        m_camera.gameObject.transform.position = temp_player_object.transform.position;
        m_camera.GetComponent<camera_follow>().tgt = temp_player_object.transform;
        temp_player_object.GetComponent<ThirdPersonMovement>().m_camera = this.m_camera;
    }
    private void PlaceRoom()
    {
        room_script cur_room = Instantiate(room_prefabs[UnityEngine.Random.Range(0, room_prefabs.Count)]);

        cur_room.transform.parent = this.transform;

        List<doorway> all_available_doorways = new List<doorway>(available_doorway);
        List<doorway> cur_room_doorway = new List<doorway>();
        AddDoorwaysToList(cur_room, ref cur_room_doorway);

        AddDoorwaysToList(cur_room, ref available_doorway);

        bool room_placed = false;

        int doorway_counter = 0;

        foreach (doorway cur_available_doorway in all_available_doorways)
        {
            foreach (doorway cur_doorway in cur_room_doorway)
            {
                PositionRoomAtDoorway(ref cur_room, cur_doorway, cur_available_doorway);
                if (CheckRoomOverlap(cur_room))
                {
                    doorway_counter++;
                    continue;
                }
                room_placed = true;
                placed_room.Add(cur_room);

                cur_doorway.gameObject.SetActive(false);
                available_doorway.Remove(cur_doorway);

                cur_available_doorway.gameObject.SetActive(false);
                available_doorway.Remove(cur_available_doorway);

                break;
            }
            if (room_placed)
            {
                break;
            }
            else
            {
                Destroy(cur_room.gameObject);
                ResetLevelGenerator();
                break;
            }
        }
    }
    private void PlaceRoom(bool end_room)
    {
        room_script cur_room = Instantiate(end_room_prefab);

        cur_room.transform.parent = this.transform;


        List<doorway> all_available_doorways = new List<doorway>(available_doorway);
        List<doorway> cur_room_doorway = new List<doorway>();
        AddDoorwaysToList(cur_room, ref cur_room_doorway);

        AddDoorwaysToList(cur_room, ref available_doorway);

        bool room_placed = false;

        int doorway_counter = 0;

        foreach (doorway cur_available_doorway in all_available_doorways)
        {
            foreach (doorway cur_doorway in cur_room_doorway)
            {
                PositionRoomAtDoorway(ref cur_room, cur_doorway, cur_available_doorway);
                if (CheckRoomOverlap(cur_room))
                {
                    doorway_counter++;
                    continue;
                }
                room_placed = true;
                placed_room.Add(cur_room);

                cur_doorway.gameObject.SetActive(false);
                available_doorway.Remove(cur_doorway);

                cur_available_doorway.gameObject.SetActive(false);
                available_doorway.Remove(cur_available_doorway);

                break;
            }
            if (room_placed)
            {
                GameObject temp_enemy = Instantiate(enemy, cur_room.enemy_spawner.transform.position, cur_room.enemy_spawner.transform.rotation);
                break;
            }
            else
            {
                Destroy(cur_room.gameObject);
                ResetLevelGenerator();
                break;
            }
        }
    }
    private void ResetLevelGenerator()
    {
        StopAllCoroutines();
        StopCoroutine("GenerateLevel");

        if (start_room)
        {
            Destroy(start_room.gameObject);
        }
        if (end_room)
        {
            Destroy(end_room.gameObject);
        }
        foreach (room_script rs in placed_room)
        {
            Destroy(rs.gameObject);
        }
        placed_room.Clear();
        available_doorway.Clear();
        StartCoroutine("GenerateLevel");
    }
    private void AddDoorwaysToList(room_script r_s, ref List<doorway> list)
    {
        foreach (doorway dw in r_s.doorWays)
        {
            int r = UnityEngine.Random.Range(0, list.Count);
            list.Insert(r, dw);
        }
    }
    private void PositionRoomAtDoorway(ref room_script room, doorway room_doorway, doorway tgt_dw)
    {
        room.transform.rotation = Quaternion.identity;
        room.transform.position = Vector3.zero;
        //rotate room :)
        Vector3 tgt_dw_euler = tgt_dw.transform.eulerAngles;
        Vector3 room_doorway_euler = room_doorway.transform.eulerAngles;
        float delta_angle = Mathf.DeltaAngle(room_doorway_euler.y, tgt_dw_euler.y);
        Quaternion cur_room_tgt_rotation = Quaternion.AngleAxis(delta_angle, Vector3.up);
        room.transform.rotation = cur_room_tgt_rotation * Quaternion.Euler(0f, 180f, 0f);

        Vector3 room_pos_offset = room_doorway.transform.position - room.transform.position;
        room.transform.position = tgt_dw.transform.position - room_pos_offset;
    }
    private bool CheckRoomOverlap(room_script rs)
    {
        Bounds bounds = rs.room_bounds;
        bounds.center = rs.transform.position;
        bounds.Expand(overlap);

        Collider[] colliders = Physics.OverlapBox(bounds.center, bounds.size / 2, rs.transform.rotation, room_layer_mask);
        if (colliders.Length > 0)
        {
            foreach (Collider c in colliders)
            {
                if (c.transform.parent.gameObject.Equals(rs.gameObject))
                {
                    continue;
                }
                else
                    return true;
            }
        }
        return false;
    }

    //COROUTINEEE
    private IEnumerator GenerateLevel()
    {
        WaitForSeconds start_up = new WaitForSeconds(1);
        WaitForFixedUpdate interval = new WaitForFixedUpdate();

        yield return start_up;
        PlaceStartRoom();
        yield return interval;

        int iteration = UnityEngine.Random.Range((int)i_r.x, (int)i_r.y);
        Debug.Log("iteration " + iteration);
        for (int i = 0; i < iteration; i++)
        {
            PlaceRoom();
            yield return new WaitForSeconds(0.01f);
        }
        PlaceRoom(true);
        yield return interval;

        m_surface.BuildNavMesh();
    }
}
