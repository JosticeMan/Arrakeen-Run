using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinEnemyWeapon : MonoBehaviour {

    public GameObject[] weapons; //The weapons that the player can choose from
    public bool playerSpotted; //Whether or not a player has been detected 

    public string enemyTag; //The tag of the enemy to this player
    public LayerMask enemyMask; //The enemy mask of the player
    public bool gettingChased; //Whether or not the player is getting chased
    public Transform spawn; //The location the gun will spawn
    public Vector3 offset; //The offset of the gun when running/jumping

    private bool isMelee; //Whether or not the player is using a melee weapon
    private GameObject spawnedWeapon; //The weapon that has been spawned and equipped
    private Rigidbody2D rigidBod; //The rigidbody of the enemy
    private JustinEnemyMovement controller; //The controller of the enemy
    private bool movedUp; //Whether or not the weapon is currently elevated

	// Use this for initialization
	void Start () {
        playerSpotted = false;
        gettingChased = false;
        movedUp = false;
        rigidBod = GetComponent<Rigidbody2D>();
        controller = GetComponent<JustinEnemyMovement>();
        GameObject weapon = getRandomWeapon();
        spawnedWeapon = Instantiate(weapon, spawn);

        isMelee = (spawnedWeapon.GetComponent<JustinEnemyGun>() == null);
        if (!isMelee)
        {
            GetComponent<JustinEnemyMovement>().usingGun = true;
            spawnedWeapon.GetComponent<JustinEnemyGun>().enemyTag = enemyTag;
            spawnedWeapon.GetComponent<JustinEnemyGun>().enemyMask = enemyMask;
        }
        else
        {
            //Means the weapon is a type of melee
            spawnedWeapon.GetComponent<JustinEnemyMelee>().enemyTag = enemyTag;
        }
	}
	
    //Returns a random weapon from the given array of weapons
    GameObject getRandomWeapon()
    {
        int index = Random.Range(0, weapons.Length);
        return weapons[index];
    }

    //Attacks when a player is spotted or getting chased
	// Update is called once per frame
	void Update () {

        if(spawnedWeapon != null)
        {
            handleWeaponAdjustment();
        }

        if(playerSpotted || gettingChased)
        {
            if (isMelee)
            {
                spawnedWeapon.GetComponent<JustinEnemyMelee>().attack();
            }
            else
            {
                spawnedWeapon.GetComponent<JustinEnemyGun>().attack();
            }
        }
    }

    //Handles the adjustment of the weapon
    void handleWeaponAdjustment()
    {
        Vector3 currentPos = spawnedWeapon.transform.position;
        bool moveUp = rigidBod.velocity.x != 0f || !(controller.isGrounded);
        if (moveUp && !movedUp)
        {
            adjustUp(currentPos);
        }
        else
        {
            if (movedUp && !moveUp)
            {
                adjustDown(currentPos);
            }
        }
    }

    //Move the weapon up
    void adjustUp(Vector3 currentPos)
    {
        if (controller.facingRight)
        {
            spawnedWeapon.transform.position = new Vector3(currentPos.x + offset.x, currentPos.y + offset.y, currentPos.z);
        }
        else
        {
            spawnedWeapon.transform.position = new Vector3(currentPos.x - offset.x, currentPos.y + offset.y, currentPos.z);
        }
        movedUp = true;
    }

    //Move the weapon down
    void adjustDown(Vector3 currentPos)
    {
        if (controller.facingRight)
        {
            spawnedWeapon.transform.position = new Vector3(currentPos.x - offset.x, currentPos.y - offset.y, currentPos.z);
        }
        else
        {
            spawnedWeapon.transform.position = new Vector3(currentPos.x + offset.x, currentPos.y - offset.y, currentPos.z);
        }
        movedUp = false;
    }

}
