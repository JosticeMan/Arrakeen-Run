using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinEnemyGroundChecker : MonoBehaviour {

    public LayerMask groundLayer; //The layer that is considered the ground
    public float groundCheckRadius; //The radius to check for the ground

    private JustinEnemyMovement enemy; //The main body of the player

	// Use this for initialization
	void Start () {
        enemy = GetComponentInParent<JustinEnemyMovement>();
	}

    //Updates whether or not the circle collider is colliding with a ground layer object
    void Update()
    {
        enemy.isGrounded = Physics2D.OverlapCircle(gameObject.transform.position, groundCheckRadius, groundLayer);
    }

}
