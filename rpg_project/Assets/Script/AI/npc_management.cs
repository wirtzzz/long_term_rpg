using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npc_management : MonoBehaviour
{
    public enum NPCBehaviour
    {
        Movable,
        Static
    }
    public NPCBehaviour behaviour;
    public GameObject storypoints_parent;
    public Vector3 cur_destination;
    private story_point[] destinations;
    private int cur_dest_index = 0;
    private GameObject cur_point;
    private Outline outline_script;
    public NavMeshAgent agent;
    void Start()
    {
        outline_script = GetComponent<Outline>();
        outline_script.enabled = false;
        if(behaviour == NPCBehaviour.Movable)
        {
            if(storypoints_parent != null)
            {
                destinations = storypoints_parent.GetComponentsInChildren<story_point>(true);
                NextStoryPoint();
            }
        }
    }

    // Update is called once per frame
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
        StartCoroutine(check_distance());
    }
    private void OnMouseOver()
    {
        outline_script.enabled = true;
    }
    private void OnMouseExit()
    {
        outline_script.enabled = false;
    }
    private IEnumerator check_distance()
    {
        while (Vector3.Distance(agent.transform.position, agent.destination) > 1.0f)
        {
            yield return null;
        }
        cur_point.SetActive(false);
        NextStoryPoint();
    }
}
