using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    public delegate void tgt_killed(GameObject tgt_object);
    public static event tgt_killed is_killed;
    public int number_to_kill;
    private int killed = 0;
    //to do : créer event calls

    private void OnEnemyKilled()
    {
        if (killed <= number_to_kill)
        {
            killed++;
            is_killed(target);
        }
    }
}