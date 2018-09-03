using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinIgnoreParentScale : MonoBehaviour {

    private GameObject parent; //Parent object
    private Vector3 originalScale; //Original scale

	// Use this for initialization
	void Start () {
        parent = transform.parent.gameObject;
        originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if(parent.transform.localScale.x == -1)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
	}
}
