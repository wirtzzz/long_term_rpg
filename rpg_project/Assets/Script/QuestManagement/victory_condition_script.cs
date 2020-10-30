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
        kill_condition.KillCondComplete += KillConditionComplete;
        zone_condition.ZoneCondComplete +=  
    }
    private void OnDisable()
    {
        kill_condition.KillCondComplete -= KillConditionComplete;
    }
    private void KillConditionComplete()
    {
        //voila
        Debug.Log("you killed all the enemies!");
    }
    private void ZoneConditionComplete()
    {

    }
}
