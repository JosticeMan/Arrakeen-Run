using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinEnemyMovement : MonoBehaviour {

    public float enemyVelocity; //The speed that the player goes normally
    public float maxSpeed; //The maximum speed that the player can go

    public bool isGrounded; //Boolean to track whether or not the player is touching the ground

    public bool enemySpotted; //Boolean tracking whether or not an enemy was spotted
    public bool facingRight; //Whether or not the player is facing right
    public bool usingGun = false; //Whether or not the player is using a gun instead of melee
    public GameObject playerSpotted; //The object that was spotted by the enemy's visibility detector
    public float runDistance; //The distance that the enemy will run from the player
    public bool stopMoving = false; //Boolean that determines whether or not the player can move

    public float groundY = -0.3f; //The coordinate of the Y value in the case the rigid body glitches through the colliders

    [SerializeField]
    private bool fightTillDeath = false; //Determines whether or not the player stands his ground and fight
    private Vector2 runVelocity; //The velocity that the player runs from another threat 
    private GameObject tempSpotted; //The object that was spotted by the enemy's visibility detector
    private bool keepRunning = false; //Whether or not the player keeps running
    private JustinEnemyWeapon weaponManager; //The manager that handles the weapon that the player has 
    private Rigidbody2D enemyBody; //The body of the player
    private Animator anim; //The animator of the player
    private float passedTime; //The amount of time that has passed
    private float previousFrameZero; //The amount of times the previous frame had zero velocity
    private float stuckTimes; //The amount of times the player got stuck 
    private float rightTimes; //The amount of frames the player was walking right
    private float leftTimes; //The amount of frames the player was walking left
    private float previousTeleTime; //The amount of time that passed since the player got tped up

    // Use this for initialization
    void Start() {
        enemyBody = GetComponent<Rigidbody2D>();
        weaponManager = GetComponent<JustinEnemyWeapon>();
        anim = GetComponent<Animator>();
        facingRight = false;
        if (determineBelowNumber(50f))
        {
            facingRight = true;
        }
        enemySpotted = false;
        previousFrameZero = 0;
        previousTeleTime = 1000000;
    }

    //Randomly generate a number between 0 and 100 and return whether or not it was lower than the target number
    bool determineBelowNumber(float target)
    {
        return Random.Range(0, 100) < target;
    }

    //Determines when the enemy hit a platform edge and makes it turn back if it does
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Edge" && passedTime > 0.00001f) {
            if(keepRunning)
            {
                fightTillDeath = true;
            }
            facingRight = !facingRight;
            passedTime = 0;
        }
    }

    //Rotates the player according to the direction he is facing 
    // Update is called once per frame
    void Update() {

        weaponManager.playerSpotted = enemySpotted;
        passedTime += Time.deltaTime;

        float enemyHorizontal = enemyBody.velocity.x;

        anim.SetBool("Grounded", isGrounded); //Passes the grounded boolean into the animator for states
        anim.SetFloat("Speed", Mathf.Abs(enemyHorizontal)); //Passes the speed value into the animator for states

        if (enemyHorizontal < -0.1f && !keepRunning) //Makes the player face in the right direction 
        {
            transform.localScale = new Vector3(-1, 1, 1); //Flip image properly to face right direction
        }
        if (enemyHorizontal > 0.1f && !keepRunning)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if(transform.position.y < groundY)
        {
            if (previousTeleTime - Time.deltaTime > .1f)
            {
                gameObject.SetActive(false);
            }
            Vector3 t = transform.position;
            transform.position = new Vector3(t.x, groundY + .1f, t.z);
            previousTeleTime = Time.deltaTime;
        }

        if(keepRunning || fightTillDeath)
        {
            if (transform.localScale.x < 0)
            {
                rightTimes = 0;
                leftTimes++;
            }
            else
            {
                leftTimes = 0;
                rightTimes++;
            }

            if (leftTimes > 5)
            {
                facingRight = false;
            }
            else if (rightTimes > 5)
            {
                facingRight = true;
            }
        }
    }

    //Method sets the states up such that the player will start running from the other object
    public void runFromPlayer()
    {
        runVelocity = -enemyBody.velocity;
        keepRunning = true;
        weaponManager.gettingChased = true;
    }

    //Updates the object that was spotted by the detector
    public void updateSpotted()
    {
        tempSpotted = playerSpotted;
    }

    //Makes the player run away from the sighted player
    void handleRun()
    {
        if (tempSpotted == null)
        {
            keepRunning = false;
            weaponManager.gettingChased = false;
        }
        else
        {
            enemyBody.velocity = runVelocity;
            if (Vector3.Distance(transform.position, tempSpotted.transform.position) > runDistance || fightTillDeath)
            {
                facingRight = !facingRight;
                keepRunning = false;
                weaponManager.gettingChased = false;
                tempSpotted = null;
                enemyBody.velocity = runVelocity * -0.00000001f;
            }
        }
    }

    //Handles the movement of the player given he wants to melee attack (Stop when in range of player and continue when he is out of range)
    void handleStopMove()
    {
        if (tempSpotted == null)
        {
            stopMoving = false;
        }
        else
        {
            enemyBody.velocity = new Vector2(0f, 0f);
            if (Vector3.Distance(transform.position, tempSpotted.transform.position) > 2f)
            {
                stopMoving = false;
            }
        }
    }

    //Nudges the player a bit if stuck
    void handleStuck()
    {
        GameObject parent = transform.parent.gameObject;
        Vector3 parentTransform = parent.transform.position;
        if (stuckTimes > 4)
        {
            parent.transform.position = new Vector3(parentTransform.x - .5f, parentTransform.y, parentTransform.z);
        }
        else
        {
            parent.transform.position = new Vector3(parentTransform.x + .5f, parentTransform.y, parentTransform.z);
        }
        previousFrameZero = 0;
        stuckTimes++;
    }

    //Handles the movement of the player given certain states
    void FixedUpdate()
    {

        if ((enemyBody.velocity.x == 0f && (!isGrounded)) && !fightTillDeath && !keepRunning && !stopMoving && !enemySpotted)
        {
            previousFrameZero++;
        } else
        {
            previousFrameZero = 0;
            stuckTimes = 0;
        }

        if (previousFrameZero > 10)
        {
            handleStuck();
        }

        if (keepRunning)
        {
            handleRun();
        }
        else if(fightTillDeath)
        {
            enemyBody.velocity = new Vector2(0f, 0f);
            if(!enemySpotted)
            {
                fightTillDeath = false;
                weaponManager.gettingChased = false;
            }
        }
        else if(stopMoving)
        {
            handleStopMove();
        }
        else
        {
            Vector2 movement = new Vector2(enemyVelocity, 0f);

            if (!facingRight)
            {
                movement = new Vector2(-enemyVelocity, 0f);
            }

            if(!isGrounded)
            {
                movement.x = 0f;
            }

            enemyBody.velocity = movement;

            if (enemyBody.velocity.x > maxSpeed) //Prevents the player from going over the maximum speed
            {
                enemyBody.velocity = new Vector2(maxSpeed, 0f);
            }
            if (enemyBody.velocity.x < -maxSpeed) //Prevents the player from going over the maximum speed in the other direction
            {
                enemyBody.velocity = new Vector2(-maxSpeed, 0f);
            }
        }

    }

}