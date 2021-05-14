﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.AI;

public class npc_management : MonoBehaviour
{
    public enum NPCBehaviour
    {
        Movable,
        Static
    }
    public int y = 0;
    public TextAsset text_asset;
    public NPCBehaviour behaviour;
    public GameObject storypoints_parent;
    public Vector3 cur_destination;
    protected story_point[] destinations;
    protected int cur_dest_index = 0;
    protected GameObject cur_point;
    protected Outline outline_script;
    public NavMeshAgent agent;
    void Start()
    {
        outline_script = GetComponent<Outline>();
        outline_script.enabled = false;
        if (behaviour == NPCBehaviour.Movable)
        {
            if (storypoints_parent != null)
            {
                Debug.Log("spp exists");
                destinations = storypoints_parent.GetComponentsInChildren<story_point>(true);
                NextStoryPoint();
            }
        }
    }
    // Update is called once per frame
    protected void NextStoryPoint()
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
    protected void OnMouseOver()
    {
        if (behaviour == NPCBehaviour.Static)
            outline_script.enabled = true;
    }
    protected void OnMouseExit()
    {
        outline_script.enabled = false;
    }
    protected IEnumerator check_distance()
    {
        while (Vector3.Distance(agent.transform.position, agent.destination) > 1.0f)
        {
            yield return null;
        }
        cur_point.SetActive(false);
        NextStoryPoint();
    }
    protected void OnMouseDown()
    {
        if (behaviour == NPCBehaviour.Static)
        {
            gameManager.instance.inkManager.ink_json_asset = text_asset;
            gameManager.instance.inkManager.StartStory();
        }
    }
}