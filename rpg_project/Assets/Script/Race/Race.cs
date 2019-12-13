using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Race : MonoBehaviour
{
    //PROTECTED
    protected Sprite m_sprite;
    protected string m_race_name;
    protected Vector2 m_position;
    protected GameObject m_object;
    protected CharacterController controller;
    //METHOD
    protected void Move(Vector2 point_to_move)
    {
        
    }
    protected abstract void SayName();
}