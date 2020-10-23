using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victory_condition_script : MonoBehaviour
{
    public enum cond_type
    {
        enemy_kill,
        zone_reached,
        object_taken,
        interaction
    }

    public List<cond_type> quest_conditions;

    private void OnEnable()
    {
        Target.is_killed += EnemyKilledEvent;
    }
    private void OnDisable()
    {
        Target.is_killed -= EnemyKilledEvent;
    }

    private void EnemyKilledEvent(GameObject tgt_object)
    {
        Debug.Log("enemy KILLED");
    }
}
