using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npc_management : MonoBehaviour
{
    public GameObject storypoints_parent;
    public Vector3 cur_destination;
    private story_point[] destinations;
    private int cur_dest_index = 0;
    private GameObject cur_point;
    public NavMeshAgent agent;
    void Start()
    {
        destinations = storypoints_parent.GetComponentsInChildren<story_point>(true);
        NextStoryPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.transform.position, agent.destination) <= 1.0f)
        {
            cur_point.SetActive(false);
            NextStoryPoint();
        }
    }
    private void NextStoryPoint()
    {
        destinations[cur_dest_index].gameObject.SetActive(true);
        cur_destination = destinations[cur_dest_index].transform.position;
        cur_point = destinations[cur_dest_index].gameObject;

        agent.destination = cur_destination;
        if (cur_dest_index == destinations.Length - 1)
            cur_dest_index = 0;
        else
            cur_dest_index++;
    }
}
