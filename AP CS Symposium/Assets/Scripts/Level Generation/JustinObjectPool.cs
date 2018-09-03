using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinObjectPool : MonoBehaviour {

    public GameObject pooledObject; //The object that we will be pooling

    public int poolCount; //The amount of objects the pool will contain 

    List<GameObject> pooledObjects; //All the objects that have been pooled

    private GameObject spawnInto; //Object that the platforms will become a child of

    // Use this for initialization
    void Start () {

        spawnInto = GameObject.Find("SpawnedMapObjects");
        pooledObjects = new List<GameObject>();

        for(int i = 0; i < poolCount; i++)
        {
            newPoolObject();
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
    //Returns a random object from the list
    GameObject getRandomObject()
    {
        return pooledObject[Random.Range(0, pooledObject.Length)];
    }
    */

    //Creates a new pool object and sets the appropriate parameters 
    GameObject newPoolObject()
    {
        GameObject pooled = Instantiate(pooledObject);
        pooled.transform.parent = spawnInto.transform;
        pooled.SetActive(false);
        pooledObjects.Add(pooled);
        return pooled;
    }

    //Allows the retrieval of the next unused pooled object 
    //If there is none, create a new object and return that
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if(pooledObjects[i] != null && !pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return newPoolObject();

    }
}
