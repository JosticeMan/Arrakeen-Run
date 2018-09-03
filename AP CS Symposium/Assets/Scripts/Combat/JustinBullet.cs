using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinBullet : MonoBehaviour {

    public float damage; //Damage of the bullet
    public float speed; //Speed of the bullet 
    public float despawnTime; //The time it takes for the bullet to despawn after not hitting anything

    public bool facingRight; //Which direction the bullet is facing
    public string enemyTag; //The enemy mask for the bullet
    //public float radius; //The radius of the bullet

    private Rigidbody2D bulletBody; //The rigid body of the bullet to allow us to control movement
    private Vector2 speedVector; //Calculated speed vector for the bullet
    private float timePassed; //The amount of time that passed since the bullet spawned
    private float lastCollision; //The time since the bullet last hit something

	// Use this for initialization
	void Start () {

        lastCollision = 0f;
        bulletBody = GetComponent<Rigidbody2D>();
        speedVector = new Vector2(speed, 0);
        if(!facingRight)
        {
            speedVector = new Vector2(-speed, 0);
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z); 
        }

	}

    /*
    //This method is for when the bullet collides with the ground or an enemy. The bullet will despawn if it does collide with any of these two types.
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedWith = collision.gameObject;
        if (collidedWith.tag == "Ground") 
        {
            Destroy(gameObject);
        }
        else if (collidedWith.tag == enemyTag && timePassed > lastCollision + 0.0001f)
        {
            //Damage enemy code here
            collidedWith.GetComponent<JustinPlayerStats>().updateHealth(-damage);
            //Debug.Log("Enemy was hit! " + collidedWith.GetComponent<JustinPlayerStats>().health);
            Destroy(gameObject);
        }
        lastCollision = timePassed;
        //Maybe play explosion animation here
    }
    */

    //This method makes the bullet move and determine whether it is time to destroy the bullet or not
    // Update is called once per frame
    void Update () {

        SpawnRay(); 

        timePassed += Time.deltaTime;
        bulletBody.velocity = speedVector;

        if(despawnTime < timePassed)
        {
            Destroy(gameObject);
        }
	}

    //Spawns a raycast in the bullet to detect for collisions
    void SpawnRay()
    {
        Vector2 direction = Vector2.right;

        if (!facingRight)
        {
            direction = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, bulletBody.velocity.x);
        if(hit.collider != null && timePassed > lastCollision + 0.00001f)
        {
            if(hit.transform.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }
            else if(hit.transform.gameObject.tag == enemyTag)
            {
				JustinPlayerStats jPS = hit.transform.gameObject.GetComponent<JustinPlayerStats> ();
				if (jPS == null) {
					//Damage enemy code here
					hit.transform.gameObject.GetComponent<JustinPlayerStatsNN> ().updateHealth (-damage);
				} else {
					//Damage enemy code here
					jPS.updateHealth (-damage);
				}
                //Debug.Log("Enemy was hit! " + collidedWith.GetComponent<JustinPlayerStats>().health);
                Destroy(gameObject);
            }
            lastCollision = timePassed;
        }
    }

    /*
    void spawnRayCircle()
    {
        Collider2D collided = Physics2D.OverlapCircle(gameObject.transform.position, radius, enemyMask);
        Collider2D groundCheck = Physics2D.OverlapCircle(gameObject.transform.position, radius, LayerMask.NameToLayer("Ground"));
        if(groundCheck != null)
        {
            Destroy(gameObject);
        }
        if(collided != null)
        {
            if(collided.GetComponent<JustinPlayerStats>() != null)
            {
                //Damage enemy code here
                collided.GetComponent<JustinPlayerStats>().updateHealth(-damage);
                //Debug.Log("Enemy was hit! " + collidedWith.GetComponent<JustinPlayerStats>().health);
                Destroy(gameObject);
            }
        }
    }
    */

    /*
    void spawnRayCast()
    {
        Vector2 direction = Vector2.right;

        if (!facingRight)
        {
            direction = Vector2.left;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
        if (hit.collider != null && timePassed > lastCollision + 0.0001f)
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }
            else if (hit.transform.gameObject.tag == enemyTag)
            {
                //Damage enemy code here
                hit.transform.gameObject.GetComponent<JustinPlayerStats>().updateHealth(-damage);
                //Debug.Log("Enemy was hit! " + collidedWith.GetComponent<JustinPlayerStats>().health);
                Destroy(gameObject);
            }
            lastCollision = timePassed;
        }
    }
    */

}
