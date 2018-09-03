using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinGun : MonoBehaviour {

    public float attackSpeed = 1f; //The firing rate of the gun
    public float damage = 10f; //Damage the gun does per bullet

    public GameObject bullet; //The ammo that the gun uses

    private GameObject parent; //The object that the spawned bullets become a child of
    private Transform gunTransform; //The transform of the gun
    private JustinPlayerController player; //The main body of the player
    private JustinPlayerControllerNN player1; //The main body of the player
    private float timePassed; //The amount of time that passed since the last attack
	public AudioSource audio; //The shooting sound effect

	// Use this for initialization
	void Start () {
        parent = GameObject.Find("Bullets");
        gunTransform = GetComponent<Transform>();
        player = GetComponentInParent<JustinPlayerController>();
        if(player == null)
        {
            player1 = GetComponentInParent<JustinPlayerControllerNN>();
        }
        timePassed = attackSpeed;
	}
	
    //Determines whether or not the player can attack
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
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

    //Handles the spawning of the bullet that will damage the player that it hits
    public void attack()
    {
        //Spawn bullet
        Vector3 position = new Vector3(gunTransform.position.x + .25f, gunTransform.position.y + .025f, gunTransform.position.z);
        if(player != null)
        {
            if (!player.facingRight)
            {
                position = new Vector3(gunTransform.position.x - .25f, gunTransform.position.y + .025f, gunTransform.position.z);
            }
        } else
        {
            if (!player1.facingRight)
            {
                position = new Vector3(gunTransform.position.x - .25f, gunTransform.position.y + .025f, gunTransform.position.z);
            }
        }
        GameObject b = (GameObject) Instantiate(bullet, position, gunTransform.rotation);
        JustinBullet bul = b.GetComponent<JustinBullet>();
        if(player != null)
        {
            bul.facingRight = player.facingRight;
        } else
        {
            bul.facingRight = player1.facingRight;
        }
        bul.damage = damage;
        b.transform.parent = parent.transform;
    }
}
