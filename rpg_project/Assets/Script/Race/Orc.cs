using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Race
{
    //CONSTRUCTOR
    public Orc(int life_points, int strength)
    {
        this.m_race_name = "Orc";
    }
    //METHOD
    protected override void SayName()
    {
        Debug.Log("I am an " + m_race_name + "and i am going to kill you !");
    }    
}
