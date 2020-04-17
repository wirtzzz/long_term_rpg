using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Career
{
    public GameObject CharacterGO;
    public float speed;
    public Animator animator;
    public bool on_attack=false;
    public SwordMan player_atck;
    public Character player_char;
    //HITBOXES
    private void Start()
    {
        player_char.m_character_orientation = Character.Dir.front;
        player_atck.ActivateBox(Character.Dir.no_dir, false);
    }
    // Update is called once per frame

    void Update()
    {
        //MOVEMENT
        if (Input.GetKey(KeyCode.Z))
        {
            animator.SetTrigger("back_walking_start");
            CharacterGO.transform.Translate(Vector3.up * speed /100);
            player_char.m_character_orientation = Character.Dir.back;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetTrigger("front_walking_start");
            CharacterGO.transform.Translate(Vector3.down * speed /100);
            player_char.m_character_orientation = Character.Dir.front;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            animator.SetTrigger("left_walking_start");
            CharacterGO.transform.Translate(Vector3.left * speed /100);
            player_char.m_character_orientation = Character.Dir.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("right_walking_start");
            CharacterGO.transform.Translate(Vector3.right * speed /100);
            player_char.m_character_orientation = Character.Dir.right;
        }
        else
            animator.SetTrigger("stop");


        //ATTACK
        if (Input.GetMouseButtonDown(0)&&!on_attack)
        {
            on_attack = true;
            switch (player_char.m_character_orientation)
            {
                case Character.Dir.back:
                    animator.SetTrigger("attack_back");
                    //CharacterGO.GetComponent<Career>().Attack()
                    break;
                case Character.Dir.front:
                    animator.SetTrigger("attack_front");
                    break;
                case Character.Dir.right:
                    animator.SetTrigger("attack_right");
                    break;
                case Character.Dir.left:
                    animator.SetTrigger("attack_left");
                    break;
                default:
                    break;
            }
            player_atck.ActivateBox(player_char.m_character_orientation, true);
            StartCoroutine(wait_hitbox(player_char.m_character_orientation));
        }
    }

    //BOX ACTIVATOR


    //COROUTINE
    private IEnumerator wait_hitbox(Character.Dir collider_dir)
    {
        yield return new WaitForFixedUpdate();
        player_atck.ActivateBox(collider_dir, false);
        on_attack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            player_char.TakeDamages(10);
        }
    }
}
