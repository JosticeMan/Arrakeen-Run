using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinObjectGenerator : MonoBehaviour {

    public JustinObjectPool[] objectPoolers; //The pooler of the objects that can spawn on the platform
    public float distanceBetween; //The distance between each object
    public float distanceToGround; //The distance the vector is from the ground
    public float spawnChance; //The chance for the object to spawn on the platform
    public float maxSpawned; //The max amount of objects that can be spawned on the platform
    public float randomMaxHeight; //The highest you want the obejct to be spawned from the platform

    //Returns a random pooler
    JustinObjectPool getRandomPooler(JustinObjectPool[] pooler)
    {
        return pooler[Random.Range(0, pooler.Length)];
    }

    //Returns whether or not the random number is less than or equal to the given num
    bool chance(float num)
    {
        return (Random.Range(0, 100)) < num;
    }

    //Returns the width of the object
    float getWidth(GameObject obj)
    {
        JustinObjectDimensions dim = obj.GetComponent<JustinObjectDimensions>();
        if (dim != null)
        {
            return dim.objectWidth;
        }
        return 0f;
    }

    //Returns the height of the object
    float getHeight(GameObject obj)
    {
        JustinObjectDimensions dim = obj.GetComponent<JustinObjectDimensions>();
        if(dim != null)
        {
            return dim.objectHeight;
        }
        return 0f;
    }

    //Spawns objects based on chance and randomly 
    public void spawnObjects(Vector3 startingPosition, float distanceToCover, float spawned)
    {
        if(objectPoolers.Length != 0)
        {
            GameObject nextObject = getRandomPooler(objectPoolers).GetPooledObject();
            float spawnCount = spawned;
            if (distanceToCover <= getWidth(nextObject) || spawnCount >= maxSpawned)
            {
                return;
            }
            if (chance(spawnChance))
            {
                float randomHeight = Random.Range(0f, randomMaxHeight);
                Vector3 start = new Vector3(startingPosition.x + (getWidth(nextObject)), startingPosition.y + distanceToGround + (getHeight(nextObject) / 2f) + randomHeight, startingPosition.z);
                nextObject.transform.position = start;
                nextObject.SetActive(true);
                spawnCount++;
            }
            spawnObjects(new Vector3(startingPosition.x + getWidth(nextObject) + distanceBetween, startingPosition.y, startingPosition.z), distanceToCover - getWidth(nextObject) - distanceBetween, spawnCount);
        }
    }

}
