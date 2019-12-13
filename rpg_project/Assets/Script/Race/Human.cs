using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : Race
{
    string genre;
    public Man(int life_points, int strength, char mf, string name)
    {
        this.m_race_name = name;
        if (mf == 'm')
            this.genre = "Mr";
        else
            this.genre = "Ms";
    }
    //METHOD
    protected override void SayName()
    {
            Debug.Log("Hello, I am " + genre + " " + m_race_name + ", and maybe your ally.");
    }
}