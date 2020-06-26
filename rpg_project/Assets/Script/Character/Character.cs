﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //PARAMETERS
    public string m_character_name;
    private Race m_race;
    public int m_pv;
    public int m_resistance;
    public Career m_career;
    public enum Dir
    {
        back, front, right, left, no_dir
    };
    public Dir m_character_orientation;
    public enum CharacterSide
    {
        ally,
        neutral,
        enemy
    };
    public CharacterSide m_side;
    public bool is_pnj;

    //GET SET


    //CONSTRUCTOR
    public Character(Sprite chrcter_sprite, string name, Race race, Career career, CharacterSide ch_side, bool pnj_state, int pv)
    {
        this.m_character_name = name;
        this.m_race = race;
        this.m_career = career;
        this.m_side = ch_side;
        this.is_pnj = pnj_state;
        this.m_pv = pv;
    }

    //METHODS
    public void TakeDamages(int dmg)
    {
        this.m_pv -= dmg;
        if (this.m_pv <= 0)
        {
            Destroy(this.gameObject);
        }
    }

  
}