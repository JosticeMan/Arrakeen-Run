using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinEnemyMelee : MonoBehaviour {

    public float attackSpeed = 1f; //The attack speed at which the player can attack 
    public float damage = 10f; //Damage of the melee weapon

    public string enemyTag; //The enemy of the weapon

    private bool touchingEnemy; //Whether or not the sword is touching the enemy
    private GameObject collision; //The object the sword collided with
    private float timePassed; //The amount of time that passed since the last attack
    private bool canAttack; //Whether or not the player can attack
	public AudioSource audio; //The attack sound effect

    void Start()
    {
        touchingEnemy = false;
        canAttack = true;
        timePassed = attackSpeed;
    }

    //Determines whether or not the player can attack
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= attackSpeed)
        {
            canAttack = true;
            timePassed = 0;
        }
    }

    //Detects for collision with an enemy and then triggers the code with it
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            this.collision = collision.gameObject;
            touchingEnemy = true;
            GetComponentInParent<JustinEnemyMovement>().stopMoving = true;
        }
    }

    //Sets touching enemy to false if the sword is no longer touching the enemy
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == enemyTag)
        {
            touchingEnemy = false;
        }
    }

    //Handles the attack code and deals damage to the other player
    public void attack()
    {
        if(canAttack)
        {
            if(audio != null)
            {
                audio.Play();
            }
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
                    //Debug.Log("Justin was hit! " + collision.GetComponent<JustinPlayerStats>().health);
                }
            }
            GetComponent<Animator>().SetBool("Attacking", false);
            canAttack = false;
        }
    }
}
