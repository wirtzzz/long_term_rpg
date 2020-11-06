using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect_condition : MonoBehaviour
{
    public List<Object> objects;
    public delegate void collect_cond_complete();
    public static event collect_cond_complete CollectCondComplete;
    private int pick_counter = 0;
    private int obj_counter = 0;

    private void Start()
    {
        foreach (Object item in objects)
        {
            obj_counter += item.obj_num;
        }
    }
    private void OnEnable()
    {
        Object.in_inventory += ObjectPicked;
    }
    private void OnDisable()
    {
        Object.in_inventory -= ObjectPicked;
    }
    private void ObjectPicked(GameObject obj_to_pick)
    {
        pick_counter++;
        if (pick_counter == obj_counter)
            CollectCondComplete();
    }
}
