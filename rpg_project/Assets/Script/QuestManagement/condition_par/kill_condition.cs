using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_condition : MonoBehaviour
{
    public List<Target> targets;
    public delegate void kill_cond_complete();
    public static event kill_cond_complete KillCondComplete;
    private int kill_counter = 0;
    private int tgt_counter;
    private void Start()
    {
        tgt_counter = targets.Count;
    }
    private void OnEnable()
    {
        Target.is_killed += TargetKilled;
    }
    private void OnDisable()
    {
        Target.is_killed -= TargetKilled;
    }
    private void TargetKilled(GameObject tgt)
    {
        kill_counter++;
        if (kill_counter == tgt_counter)
            KillCondComplete();
    }
}
