using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMan : Career
{
    //PARAMETERS
    public BoxCollider2D front_swdman, back_swdman, left_swdman, right_swdman;

    //CONSTRUCTORS
    public SwordMan(int strength)
    {
        this.m_strength = strength;
        this.m_career_name = "SwordMan";
    }
    public void ActivateBox(Character.Dir collider_dir, bool is_active)
    {
        switch (collider_dir)
        {
            case Character.Dir.back:
                back_swdman.enabled = is_active;
                break;
            case Character.Dir.front:
                Debug.Log("lol");
                front_swdman.enabled = is_active;
                break;
            case Character.Dir.right:
                right_swdman.enabled = is_active;
                break;
            case Character.Dir.left:
                left_swdman.enabled = is_active;
                break;
            case Character.Dir.no_dir:
                back_swdman.enabled = is_active;
                front_swdman.enabled = is_active;
                right_swdman.enabled = is_active;
                left_swdman.enabled = is_active;
                break;
            default:
                break;
        }
        StartCoroutine(wait_hitbox(collider_dir));
    }

    private IEnumerator wait_hitbox(Character.Dir collider_dir)
    {
        yield return new WaitForFixedUpdate();
        ActivateBox(collider_dir, false);
    }
}