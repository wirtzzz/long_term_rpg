using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Career
{
    public GameObject CharacterGO;
    public float speed;
    public Animator animator;
    public bool on_attack = false;
    public SwordMan player_atck;
    public Character player_char;
    //Sprite
    public GameObject CharacterSprite;
    public SwordMan m_swordMan;
    //Hitboxes
    //4 positions, start on right, go on counter clock from right to left (right, front, left, back)
    //public Vector3[] Hitboxes;
    //public GameObject Hitbox;
    //Collider Management
    private string current_trig = "idle";
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
            CharacterGO.transform.Translate(Vector3.up * speed / 100);
            player_char.m_character_orientation = Character.Dir.back;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetTrigger("front_walking_start");
            CharacterGO.transform.Translate(Vector3.down * speed / 100);
            player_char.m_character_orientation = Character.Dir.front;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            animator.SetTrigger("left_walking_start");
            CharacterGO.transform.Translate(Vector3.left * speed / 100);
            player_char.m_character_orientation = Character.Dir.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetTrigger("right_walking_start");
            CharacterGO.transform.Translate(Vector3.right * speed / 100);
            player_char.m_character_orientation = Character.Dir.right;
        }

        //ATTACK
        if (Input.GetMouseButton(0) && !on_attack)
        {
            //Go to middle state "To_Attack"
            animator.SetTrigger("Attacking");
            on_attack = true;
            m_swordMan.ActivateBox(player_char.m_character_orientation, true);
            switch (player_char.m_character_orientation)
            {
                case Character.Dir.back:
                    animator.SetTrigger("attack_back");
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
            on_attack = false;
            //LaunchTrigger("StopAttack");
        }
    }

    //Control animation triggers
    private void LaunchTrigger(string trigger)
    {
        animator.ResetTrigger(current_trig);
        animator.SetTrigger(trigger);
        current_trig = trigger;
    }

    //COROUTINE
    private IEnumerator wait_hitbox(Character.Dir collider_dir)
    {
        yield return new WaitForFixedUpdate();
        player_atck.ActivateBox(collider_dir, false);
        on_attack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            if (on_attack)
            {
                Debug.Log("Collision with " + collision.gameObject.name);
                collision.gameObject.GetComponent<Character>().TakeDamages(10);
                on_attack = false;
            }

        }
    }
}
