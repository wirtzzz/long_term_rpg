using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masterer : Career
{
    //PARAMETERS
    protected int m_mana;
    private CircleCollider2D m_range;
    private List<Character> enemies_in_range = new List<Character>();


    //CONSTRUCTOR
    public Masterer(int mana, CircleCollider2D range)
    {
        this.m_range = range;
        this.m_mana = mana;
        this.m_career_name = "Masterer";
    }

    //UNITY METHOD
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enemies_in_range.Add(collision.gameObject.GetComponent<Character>());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            enemies_in_range.Remove(collision.gameObject.GetComponent<Character>());
        }
    }

    //METHODS
    protected bool IsEnemyInRange(Character target)
    {
        bool in_range = false;
        foreach (var item in enemies_in_range)
        {
            if (target == item)
                in_range = true;
        }
        return in_range;
    }
}