using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite_controller : MonoBehaviour
{
    public int loc_strength;
    public SwordMan attacker;
    public enum State
    {
        waiting,
        attack
    }
    public State cur_state;
    public Character enemy_prototype;
    public Animator enemy_an;
    public string current_trig="to_idle";
    public float speed;
    public bool moving_towards = false;
    private Vector3 last_position;
    public float dist_value;
    private Vector3 cur_position;
    public GameObject player;
    private Vector3 target_position;
    private Vector3 move = new Vector3();
    public float radius;
    // Update is called once per frame
    private void Start()
    {
        cur_position = player.transform.position;
        StartCoroutine(WaitForPlayer());
        attacker.m_strength = loc_strength;
        attacker.ActivateBox(Character.Dir.no_dir, false);
    }
    void Update()
    {
        last_position = cur_position;
        cur_position = player.transform.position;
        //        this.transform.position = player.transform.position;
        //TeleportToPlayerZone();

    }

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
        while (cur_state==State.waiting)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, 1f, this.transform.right, radius, LayerMask.GetMask("Player"));
            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);
                cur_state = State.attack;
                MoveToPlayer();
            }
            yield return null;
        }
    }
    IEnumerator MoveTo()
    {
        float step = speed / 1500;
        Vector3 dist=new Vector3(0.0f,0.0f,0.0f);
        while (transform.position != player.transform.position-dist)
        {
            if (Mathf.Abs(player.transform.position.x - this.transform.position.x) < Mathf.Abs(player.transform.position.y - this.transform.position.y))
            {
                if (player.transform.position.y - this.transform.position.y > 0)
                {
                    dist = new Vector3(0f, dist_value, 0f);
                    enemy_an.ResetTrigger(current_trig);
                    enemy_an.SetTrigger("back_walk");
                    current_trig = "back_walk";
                    enemy_prototype.m_character_orientation = Character.Dir.back;
                }
                else
                {
                    dist = new Vector3(0f, -dist_value, 0f);
                    enemy_an.ResetTrigger(current_trig);
                    enemy_an.SetTrigger("front_walk");
                    current_trig = "front_walk";
                    enemy_prototype.m_character_orientation = Character.Dir.front;
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
                    enemy_prototype.m_character_orientation = Character.Dir.right;
                }
                else
                {
                    dist = new Vector3(-dist_value, 0f, 0f);
                    enemy_an.ResetTrigger(current_trig);
                    enemy_an.SetTrigger("left_walk");
                    current_trig = "left_walk";
                    enemy_prototype.m_character_orientation = Character.Dir.left;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position-dist, step);
            yield return 0;
        }
        enemy_an.ResetTrigger(current_trig);
        enemy_an.SetTrigger("to_idle");
        current_trig = "to_idle";
        AttackPlayer(enemy_prototype.m_character_orientation);
        cur_state = State.waiting;
        Debug.Log("arrivé");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            enemy_prototype.TakeDamages(10);
            Debug.Log(enemy_prototype.m_pv);
        }
    }
    private void AttackPlayer(Character.Dir dir)
    {
        attacker.ActivateBox(dir, true);
        Debug.Log(dir);
    }
    private IEnumerator wait_hitbox(Character.Dir collider_dir)
    {
        yield return new WaitForFixedUpdate();
        attacker.ActivateBox(collider_dir, false);
        cur_state = State.waiting;
    }
}