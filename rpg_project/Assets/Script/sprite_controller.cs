using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite_controller : MonoBehaviour
{
    //PUBLIC

    public SwordMan attacker;
    public enum State
    {
        waiting,
        attack
    }
    public State cur_state;
    public Character enemy_prototype;
    public Animator enemy_an;
    public string current_trig = "to_idle";
    public float radius;
    public float speed;
    public bool moving_towards = false;
    public float dist_value;
    public GameObject player;
    //PRIVATE
    private Vector3 last_position;
    private Vector3 target_position;
    private Vector3 move = new Vector3();
    private Vector3 cur_position;
    private Character.Dir cur_direction;
    //UNITY METHODS
    private void Start()
    {
        cur_position = player.transform.position;
        //StartCoroutine(WaitForPlayer());
    }
    void Update()
    {
        last_position = cur_position;
        cur_position = player.transform.position;
        //        this.transform.position = player.transform.position;
        //TeleportToPlayerZone();

    }
    //METHODS
    public void TeleportToPlayerZone()
    {
        move.x = cur_position.x * 50 - last_position.x * 50;
        move.y = cur_position.y * 50 - last_position.y * 50;

        Debug.Log(last_position);
        //teleport to zone
        if (move.x != 0)
        {
            if (move.x >= 0)
            {
                target_position = new Vector3(player.transform.position.x + 1, player.transform.position.y);
            }
            else
                target_position = new Vector3(player.transform.position.x - 1, player.transform.position.y);
        }
        else if (move.y != 0)
        {
            if (move.y >= 0)
            {
                target_position = new Vector3(player.transform.position.x, player.transform.position.y + 1);
            }
            else
                target_position = new Vector3(player.transform.position.x, player.transform.position.y - 1);
        }
        else
            target_position = new Vector3(player.transform.position.x - 1, player.transform.position.y);

        this.transform.position = target_position;
    }
    public void MoveToPlayer()
    {
        StartCoroutine(MoveTo());
    }
    IEnumerator WaitForPlayer()
    {
        while (cur_state == State.waiting)

        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, 1f, this.transform.right, radius, LayerMask.GetMask("Player"));
            foreach (RaycastHit2D hit in hits)
            {
                MoveToPlayer();
            }
            yield return null;
        }
        MoveToPlayer();
    }
    IEnumerator MoveTo()
    {
        float step = speed / 1500;
        Vector3 dist = new Vector3(0.0f, 0.0f, 0.0f);
        while (transform.position != player.transform.position - dist)

        {
            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < Mathf.Abs(player.transform.position.y - this.transform.position.y))
            {
                if (player.transform.position.y - this.transform.position.y > 0)
                {
                    dist = new Vector3(0f, dist_value, 0f);
                    enemy_an.ResetTrigger(current_trig);
                    enemy_an.SetTrigger("back_walk");
                    current_trig = "back_walk";

                    //enemy_prototype.m_character_orientation = Character.Dir.back;

                    //cur_direction = Character.Dir.back;

                }
                else
                {
                    dist = new Vector3(0f, -dist_value, 0f);
                    enemy_an.ResetTrigger(current_trig);
                    enemy_an.SetTrigger("front_walk");
                    current_trig = "front_walk";

                    //enemy_prototype.m_character_orientation = Character.Dir.front;

                    //cur_direction = Character.Dir.front;

                }
            }
            else
            {
                if (player.transform.position.x - this.transform.position.x > 0)
                {
                    dist = new Vector3(dist_value, 0f, 0f);
                    enemy_an.ResetTrigger(current_trig);
                    enemy_an.SetTrigger("right_walk");
                    current_trig = "right_walk";

                    //cur_direction = Character.Dir.right;

                }
                else
                {
                    dist = new Vector3(-dist_value, 0f, 0f);
                    enemy_an.ResetTrigger(current_trig);
                    enemy_an.SetTrigger("left_walk");
                    current_trig = "left_walk";

                    //cur_direction = Character.Dir.left;

                }
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position - dist, step);
            yield return null;
        }
        cur_state = State.attack;
        Attack();
    }

    private void Attack()
    {
        if (cur_state == State.attack)
        {
            enemy_an.ResetTrigger(current_trig);
            switch (cur_direction)
            {
                case Character.Dir.back:
                    enemy_an.SetTrigger("atck_to_back");
                    break;
                case Character.Dir.front:
                    enemy_an.SetTrigger("atck_to_front");
                    break;
                case Character.Dir.right:
                    enemy_an.SetTrigger("atck_to_right");
                    break;
                case Character.Dir.left:
                    enemy_an.SetTrigger("atck_to_left");
                    break;
                default:
                    break;
            }
            cur_state = State.waiting;
            WaitForPlayer();
        }
        else
        {
            Debug.LogError("Not in attack mode");
        }
    }

    //<<<<<<< Updated upstream
    //=======
    //    private void AttackPlayer(Character.Dir dir)
    //    {
    //        attacker.ActivateBox(dir, true);
    //        StartCoroutine(wait_hitbox(dir));
    //    }
    //    private IEnumerator wait_hitbox(Character.Dir collider_dir)
    //    {
    //        yield return new WaitForFixedUpdate();
    //        Debug.Log(collider_dir);
    //        attacker.ActivateBox(collider_dir, false);
    //        cur_state = State.waiting;
    //    }
    //    public void CharacterAnimationEnd(string info)
    //    {
    //        StartCoroutine(wait_hitbox(GetDirFromString(info)));
    //    }
    //    private Character.Dir GetDirFromString(string dir) {
    //        Character.Dir temp = Character.Dir.no_dir;
    //        switch (dir)
    //        {
    //            case "left":
    //                temp = Character.Dir.left;
    //                break;
    //            case "right":
    //                temp = Character.Dir.right;
    //                break;
    //            case "front":
    //                temp = Character.Dir.front;
    //                break;
    //            case "back":
    //                temp = Character.Dir.back;
    //                break;
    //            default:
    //                Debug.LogError("Ah");
    //                break;
    //        }
    //        return temp;
    //    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        enemy_prototype.TakeDamages(10);
    //        Debug.Log(enemy_prototype.m_pv);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy_prototype.TakeDamages(10);
            Debug.Log(enemy_prototype.m_pv);
        }
    }

}