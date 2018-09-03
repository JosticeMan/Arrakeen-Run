using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinPlatformGenerator : MonoBehaviour {

    //public GameObject thePlatform; //The main platform to be generated
    public Transform generationPoint; //The point ahead of the platform to check

    public GameObject startingPlatform; //The platform the player starts on
    public float minDistanceBetween; //Min distance between each platform
    public float maxDistanceBetween; //Max distance between each platform

    public float spawnChance; //The chance the generator will spawn an object
    public JustinObjectPool[] poolers; //The object pooler
	public int[] poolerChances; //The chances of each object being spawned in the main pooler
    public JustinObjectGenerator[] objectGenerator; //The generator that places the object on the platform

    public float minHeight; //The min height that the object could be spawned at
    public float maxHeight;  //The max height that the object could be spawned at

    private float previousWidth; //The previous width of the platform
    private float platformWidth; //The width of the platform

	// Use this for initialization
	void Start () {

        //platformWidth = thePlatform.GetComponents<EdgeCollider2D>()[0].points[0].x * -2f;
        if(startingPlatform != null)
        {
            platformWidth = getWidth(startingPlatform);
            transform.position = new Vector3(transform.position.x + (platformWidth / 2f), transform.position.y, transform.position.z);
        }
    }

    //Returns the width of the obj based on the first edge collider
    float getWidth(GameObject obj)
    {
        float sum = 0f;
        Vector2[] points = obj.GetComponents<EdgeCollider2D>()[0].points;
        float xScale = obj.transform.localScale.x;
        for (int i = 0; i < points.Length; i++)
        {
            if(points[i].x < 0)
            {
                sum -= (points[i].x * xScale); 
            } else
            {
                sum += (points[i].x * xScale);
            }
        }
        return sum;
    }

    //Returns whether or not the random number is less than or equal to the given num
    bool chance(float num)
    {
        return (Random.Range(0, 100)) < num;
    }

    //Returns a random pooler
    JustinObjectPool getRandomPooler(JustinObjectPool[] pooler)
    {
		int theNumber = Random.Range (0, 100);
		int index = pooler.Length - 1;
		for (int i = 0; i < poolerChances.Length; i++) {
			if (poolerChances[i] > theNumber) {
				index = i;
			}
		}
		return pooler [index];
    }

    //Handles the spawning of the objects on the platform
    void spawnObject(float randomHeight, float distanceBetween, Vector3 originalPos)
    {
        Vector3 spawner = new Vector3(originalPos.x + distanceBetween, originalPos.y + randomHeight, originalPos.z);
        if (objectGenerator.Length != 0)
        {
            foreach(JustinObjectGenerator o in objectGenerator)
            {
                o.spawnObjects(spawner, platformWidth, 0);
            }
        }
    }

    //Spawns the platform at the next position
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x && poolers.Length > 0)
        {
            //GameObject spawnedPlatform = Instantiate(thePlatform, transform.position, transform.rotation);
            //spawnedPlatform.transform.parent = spawnInto.transform;

            GameObject nextPlatform = getRandomPooler(poolers).GetPooledObject();
            platformWidth = getWidth(nextPlatform);
            float randomHeight = Random.Range(minHeight, maxHeight);
            bool spawn = chance(spawnChance);
            float distanceBetween = Random.Range(minDistanceBetween, maxDistanceBetween);
            Vector3 originalPos = transform.position;
            transform.position = new Vector3(transform.position.x + (platformWidth / 2f) + distanceBetween, transform.position.y + randomHeight, transform.position.z);
            if (spawn)
            {
                nextPlatform.transform.position = transform.position;
                nextPlatform.SetActive(true);
				JustinPlatformSpawn checkSpawn = nextPlatform.GetComponent<JustinPlatformSpawn> ();
				if (checkSpawn != null && checkSpawn.spawnObjectsOnPlatform) {
					spawnObject(randomHeight, distanceBetween, originalPos);
				}
            }
            transform.position = new Vector3(transform.position.x + (platformWidth / 2f) + distanceBetween, transform.position.y - randomHeight, transform.position.z);
        }
    }

}
