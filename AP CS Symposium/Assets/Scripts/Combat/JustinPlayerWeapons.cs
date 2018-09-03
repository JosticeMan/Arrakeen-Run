using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinPlayerWeapons : MonoBehaviour
{
    public bool[] weaponEquipped; //Determines whether a weapon is equipped or not
    public GameObject[] weapons; //The selection of weapons that the player can choose from and equip

    public GameObject currentWeapon; //The current weapon that is equipped 
    public Vector3 offset; //Weapon offset when running/jumping

    private Rigidbody2D rigidBod; //The rigidbody of the player
    private JustinPlayerController controller; //The controller of the player
    private bool movedUp; //Whether or not the gun is elevated

    // Use this for initialization
    void Start()
    {
        movedUp = false;
        rigidBod = GetComponent<Rigidbody2D>();
        controller = GetComponent<JustinPlayerController>();
    }

    //Iterates through each weapon and checks if it is equipped
    //If the weapon is equipped, then make it render and enable it
    // Update is called once per frame
    void Update()
    {
        if(currentWeapon != null)
        {
            handleWeaponAdjustment();
        }
 
        for(int i = 0; i < weapons.Length; i++)
        {
            bool enabled = false;
            if(weaponEquipped[i])
            {
                enabled = true;
            }
            weapons[i].SetActive(enabled);
        }
    }

    //Handles the adjustment of the weapon
    void handleWeaponAdjustment()
    {
        Vector3 currentPos = currentWeapon.transform.position;
        bool moveUp = rigidBod.velocity.x != 0f || !(controller.isGrounded);
        if (moveUp && !movedUp)
        {
            adjustUp(currentPos);
        }
        else
        {
            if(movedUp && !moveUp)
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
            currentWeapon.transform.position = new Vector3(currentPos.x + offset.x, currentPos.y + offset.y, currentPos.z);
        }
        else
        {
            currentWeapon.transform.position = new Vector3(currentPos.x - offset.x, currentPos.y + offset.y, currentPos.z);
        }
        movedUp = true;
    }

    //Move the weapon down
    void adjustDown(Vector3 currentPos)
    {
        if (controller.facingRight)
        {
            currentWeapon.transform.position = new Vector3(currentPos.x - offset.x, currentPos.y - offset.y, currentPos.z);
        }
        else
        {
            currentWeapon.transform.position = new Vector3(currentPos.x + offset.x, currentPos.y - offset.y, currentPos.z);
        }
        movedUp = false;
    }

    //Iterates through the boolean array and makes all weapons marked as not equipped
    public void clear()
    {
        for (int i = 0; i < weaponEquipped.Length; i++)
        {
            weaponEquipped[i] = false;
        }
        currentWeapon = null;
    }

    //Method handles the equpping of a weapon
    public void equipWeapon(int indexNumber)
    {
        if (currentWeapon != null && movedUp)
        {
            adjustDown(currentWeapon.transform.position);
        }
        clear();
        weaponEquipped[indexNumber] = true;
        currentWeapon = weapons[indexNumber];
    }

}
