using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public GameObject obj;
    public Sprite item;
    public int obj_num;
    private int picked_num=0;
    public delegate void obj_picked(GameObject obj_to_pick);
    public static event obj_picked in_inventory;

    private void OnObjectPicked()
    {
        if (picked_num <= obj_num)
        {
            picked_num++;
            in_inventory(obj);
        }
    }
}
