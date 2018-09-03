using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinMeleeAttack : MonoBehaviour {

    public float attackSpeed = 1f; //The attack speed of the melee weapon
    public float damage = 10f; //Damage of the melee weapon

    private bool touchingEnemy; //Whether or not the melee weapon is touching an enemy
    private GameObject collision; //The current object the melee weapon is touching
    private float timePassed; //The amount of time that passed since the last attack
	public AudioSource audio; //The attack sound effect

    void Start()
    {
        touchingEnemy = false;
        timePassed = attackSpeed;
    }

    //Determines whether or not the player can attack
    void Update()
    {
        timePassed += Time.deltaTime;
        if(Input.GetMouseButtonDown(0))
        {
            if (timePassed >= attackSpeed)
            {
                attack();
                if(audio != null)
                {
                    audio.Play();
                }
                timePassed = 0;
            }
        }
    }

    //Handles the detection of the melee weapon touching an enemy
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            this.collision = collision.gameObject;
            touchingEnemy = true;
        }
    }

    //Handles the detection of when the melee weapon is no longer touching the enemy
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            touchingEnemy = false;
        }
    }

    //Handles the damage aspect of the weapon by damaging the other player
    public void attack()
    {
        GetComponent<Animator>().Play("Attack");
        if (touchingEnemy)
        {
            if (collision == null)
            {
                touchingEnemy = false;
            }
            else
            {
				JustinPlayerStats jPS = collision.GetComponent<JustinPlayerStats> ();
				if (jPS == null) {
					//Damage enemy code here
					collision.GetComponent<JustinPlayerStatsNN> ().updateHealth (-damage);
				} else {
					//Damage enemy code here
					jPS.updateHealth (-damage);
				}
                //Debug.Log("Enemy was hit! " + collision.GetComponent<JustinPlayerStats>().health);
            }
        }
        GetComponent<Animator>().SetBool("Attacking", false);
    }

}
