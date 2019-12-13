using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //PARAMETERS
    public Sprite m_sprite;
    public string m_character_name;
    private Race m_race;
    public int m_pv;
    public int m_resistance;
    private Career m_career;
    private BoxCollider2D m_collider;
    public enum CharacterSide
    {
        ally,
        neutral,
        enemy
    };
    public CharacterSide m_side;
    public bool is_pnj;

    //GET SET

    public BoxCollider2D GetCollider
    {
        get { return m_collider; }
    }

    //CONSTRUCTOR
    public Character(Sprite chrcter_sprite, string name, BoxCollider2D collider, Race race, Career career, CharacterSide ch_side, bool pnj_state, int pv)
    {
        this.m_sprite = chrcter_sprite;
        this.m_character_name = name;
        this.m_race = race;
        this.m_career = career;
        this.m_side = ch_side;
        this.is_pnj = pnj_state;
        this.m_pv = pv;
        this.m_collider = collider;
    }
}