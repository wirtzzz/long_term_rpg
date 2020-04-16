using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Career : MonoBehaviour
{
    //PARAMETERS
    protected string m_career_name;
    protected int m_strength;

    //METHODS
    public virtual void Attack(Character target)
    {
        int damage = this.m_strength - target.m_resistance;
        target.m_pv -= damage;
    }
}