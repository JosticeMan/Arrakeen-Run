using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinEnemyGun : MonoBehaviour {

    public float attackSpeed = 1f; //The firing rate of the gun
    public float damage = 10f; //Damage the gun does per bullet

    public GameObject bullet; //The ammo that the gun uses

    public string enemyTag; //The enemy tag for the gun 
    public LayerMask enemyMask; //The enemy mask of the gun

    private GameObject parent; //The object to put spawned bullets as a child of
    private Transform gunTransform; //The transform information about the gun
    private JustinEnemyMovement player; //The main motor of the enemy body
    private float timePassed; //The amount of time that passed since the last attack
    private bool canAttack; //Whether or not the player can attack
	public AudioSource audio; //The shooting sound effect

    // Use this for initialization
    void Start()
    {
        gunTransform = GetComponent<Transform>();
        parent = GameObject.Find("Bullets");
        player = GetComponentInParent<JustinEnemyMovement>();
        timePassed = attackSpeed;
        canAttack = true;
    }
    
    //Determines whether or not the player can attack again
    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(!canAttack)
        {
            if (timePassed >= attackSpeed)
            {
                canAttack = true;
                timePassed = 0;
            }
        }
    }

    //Handles the attack part of the gun. Spawns a bullet and sets the appropriate properties.
    public void attack()
    {
        if(canAttack)
        {
            if(audio != null)
            {
                audio.Play();
            }
            //Spawn bullet
            Vector3 position = new Vector3(gunTransform.position.x + .05f, gunTransform.position.y + .0125f, gunTransform.position.z);
            if (!player.facingRight)
            {
                position = new Vector3(gunTransform.position.x - .05f, gunTransform.position.y + .0125f, gunTransform.position.z);
            }
            GameObject b = (GameObject)Instantiate(bullet, position, gunTransform.rotation);
            JustinBullet bul = b.GetComponent<JustinBullet>();
            bul.damage = damage;
            bul.facingRight = player.facingRight;
            bul.enemyTag = enemyTag;
            b.transform.parent = parent.transform;
            canAttack = false;
        }
    }
}
