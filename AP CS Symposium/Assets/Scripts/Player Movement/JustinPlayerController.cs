using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Class manages the movement of the player
 * 
 * Justin Yau
 * */
public class JustinPlayerController : NetworkBehaviour {

    public float playerVelocity; //Variable responsible for walking speed
    public bool keepMoving; //To determine whether or not the player gets horizontal control of player
    public float jumpForce; //Variable responsible for jumping

    [SyncVar(hook = "OnScaleUpdate")]
    public bool facingRight; //Variable responsible for tracking whether or not the player is facing right

    public float maxSpeed; //Variable responsible for limiting the highest speed the player can go

    public bool isGrounded; //This boolean tracks whether or not the player is in the air from jumping
    public float groundY; //The lowest ground level for the player

    private GameObject mainGame; //The main game
    private GameObject gameOver; //The gameover menu

    private Rigidbody2D thePlayer; //The actual body of the player
    private Animator anim; //The animator responsible for animating the player will be stored here
    private JustinPlayerWeapons weaponManager; //Weapon manager will be stored here
    private Vector3 previousPosition; //The previous position of the character will be stored here
    private bool canDoubleJump = true; //Whether or not the player can double jump
    private SpriteRenderer rend; //The sprite renderer of the player

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
		if (!isLocalPlayer) {
            Color rC = rend.material.color;
            rend.material.color = new Color(rC.r, rC.g, rC.b, 0.5f);
			return;
		}

        thePlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weaponManager = GetComponent<JustinPlayerWeapons>();

        facingRight = true;
        JustinReference r = GameObject.Find("ReferencePoint").GetComponent<JustinReference>();
        mainGame = r.mainMenu;
        gameOver = r.gameOverNetwork;
	}

    // Update is called once per frame
    void Update () {

        if (isServer)
        {
            if ((transform.localScale.x == 1 && !facingRight))
            {
                facingRight = true;
            }
            if((transform.localScale.x == -1 && facingRight))
            {
                facingRight = false;
            }
        }
        else
        {
            CmdUpdateScale(facingRight);
        }

        if (!isLocalPlayer) {
            return;
		}

        float horizontalAxis = Input.GetAxis("Horizontal");

        if(keepMoving)
        {
            horizontalAxis = 1f;
        }

        anim.SetBool("Grounded", isGrounded); //Passes the grounded boolean into the animator for states
        anim.SetFloat("Speed", Mathf.Abs(horizontalAxis)); //Passes the speed value into the animator for states

        if(isGrounded && !canDoubleJump)
        {
            canDoubleJump = true;
        }

        if(horizontalAxis < -0.1f) //Makes the player face in the right direction 
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1); //Flip image properly to face right direction
        }
        if(horizontalAxis > 0.1f)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            weaponManager.clear();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponManager.equipWeapon(0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponManager.equipWeapon(1);
        }

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && (isGrounded || canDoubleJump)) //If the space key is pressed
        {
            thePlayer.velocity = new Vector2(thePlayer.velocity.x, jumpForce);
            if(!isGrounded && canDoubleJump)
            {
                canDoubleJump = false;
            }
        }

        if(transform.position.y < groundY)
        {
            gameOver.SetActive(true);
            mainGame.SetActive(false);
            Destroy(transform.gameObject);
        }

    }

    void FixedUpdate()
    {
		if (!isLocalPlayer) {
			return;
		}

        float horizontalMovement = Input.GetAxis("Horizontal"); //We only need to move left and right in this function

        Vector2 movement = new Vector2(horizontalMovement * playerVelocity, thePlayer.velocity.y + .01f);

        if(keepMoving)
        {
            movement.x = playerVelocity;
        }

        thePlayer.velocity = movement;

        if (thePlayer.velocity.x > maxSpeed) //Prevents the player from going over the maximum speed
        {
            thePlayer.velocity = new Vector2(maxSpeed, thePlayer.velocity.y + .01f);
        }
        if (thePlayer.velocity.x < -maxSpeed) //Prevents the player from going over the maximum speed in the other direction
        {
            thePlayer.velocity = new Vector2(-maxSpeed, thePlayer.velocity.y + .01f);
        }

    }

    void OnScaleUpdate(bool fRight)
    {
        facingRight = fRight;
        if(facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    [Command]
    public void CmdUpdateScale(bool nFRight)
    {
        facingRight = nFRight;
    }

}
