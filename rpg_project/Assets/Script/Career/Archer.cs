using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Career
{
    //PARAMETERS
    protected int m_arr_num;
    private CircleCollider2D m_range;
    private List<Character> enemies_in_range = new List<Character>();

    //CONSTRUCTOR
    public Archer(int arr_num, CircleCollider2D range)
    {
        this.m_arr_num = arr_num;
        this.m_range = range;
        this.m_career_name = "Archer";
    }
    //UNITY METHODS
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