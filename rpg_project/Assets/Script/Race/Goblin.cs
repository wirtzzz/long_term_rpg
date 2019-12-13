using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Race {
    //CONSTRUCTOR
    public Goblin(int life_points, int strength, string name = "Goblin")
    {
        this.m_race_name = name;
    }
    //METHOD
    protected override void SayName()
    {
        if (name == "Goblin") { Debug.Log("I am a Goblin and im going to kill you !"); }
        else
            Debug.Log("I am " + m_race_name + ", a goblin, and im going to kill you !");
    }
}