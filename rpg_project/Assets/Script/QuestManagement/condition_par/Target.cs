using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    public int kill_number, cur_kill;
    public delegate void tgt_killed(GameObject tgt_object);
    public static event tgt_killed is_killed;
    //to do : créer event calls

    private void OnEnemyKilled()
    {
        is_killed(target);
    }
}