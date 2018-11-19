using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour {

    [Tooltip("Add a prefab of the object you want to spawn here")]
    public GameObject SpawnObject;
    [Tooltip("Do you want this to keep spawning an object")]
    public bool ContinueSpawn;
    [Tooltip("Delay before spawning an object(s)")]
    public float spawnTimer;
    [Tooltip("Check this box if you want the spawner to work when a player walks through the box colliders")]
    public bool contactEnabled;
    [Tooltip("Check this box to have a spawner location that waits a certain amount of time before starting")]
    public bool timerEnabled;
    [Tooltip("If you want to just spawn a certain number of objects, provide a number here")]
    public int amountLimited;
    [Tooltip("Check this box if you only want this to spawn once")]
    public bool singleSpawn;

    int amountCount;
    int childCount;

	// Use this for initialization
	void Start () {
        amountCount = 0;
        childCount = transform.childCount;
        Debug.Log(childCount);
		if(!contactEnabled && timerEnabled)
        {
            if (singleSpawn)
            {
                Invoke("SpawnFunction", spawnTimer);
            }
            else
            {
                InvokeRepeating("SpawnFunction", spawnTimer, spawnTimer);
            }
            
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player" && contactEnabled)
        {
            if (!ContinueSpawn)
            {
                SpawnFunction();
            }
            else
            {
                InvokeRepeating("SpawnFunction", spawnTimer, spawnTimer);
            }
            
        }
    }



    void SpawnFunction()
    {
      
        if(amountLimited > 0)
        {
            if (amountCount < amountLimited)
            {         
                GameObject obj = (GameObject)Instantiate(SpawnObject, transform.GetChild(0).transform.position, Quaternion.identity);
                amountCount++;
                return;         
            }
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(SpawnObject, transform.GetChild(0).transform.position, Quaternion.identity);
        }
    }

}
