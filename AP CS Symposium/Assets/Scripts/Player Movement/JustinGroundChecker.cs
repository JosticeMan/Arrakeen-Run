using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinGroundChecker : MonoBehaviour {

    public LayerMask groundLayer; //The layer that all the ground objects should be in
    public float groundCheckRadius; //The radius to check around the player for the ground

    private JustinPlayerController player; //The controller responsible for moving the player

	// Use this for initialization
	void Start () {

        player = GetComponentInParent<JustinPlayerController>(); //Retrieves the current controller that is making the player move

	}

    void Update()
    {
        player.isGrounded = Physics2D.OverlapCircle(gameObject.transform.position, groundCheckRadius, groundLayer);  
    }

}
