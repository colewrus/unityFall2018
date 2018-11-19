using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneScript : MonoBehaviour {



    [Tooltip("How fast the drone moves")]
    public float speed;
    [Tooltip("Sets the distance a drone will keep from the player")]
    public float engageDistance;
    [Tooltip("Enable to have drone peacefully follow the player")]
    public bool observe;
    public float shotDelay;
    public GameObject bullet;
    public List<GameObject> bullets = new List<GameObject>();

    bool fire;
    GameObject target;
    Vector3 dest;



    // Use this for initialization
    void Start () {
        target = null;
        fire = false;

        for(int i=0; i<6; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }

	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
        {
            TargetAcquired();
        }

        Debug.DrawRay(transform.position, transform.forward * 100, Color.blue);
	}
    
    void TargetAcquired()
    {

        dest = (target.transform.position - transform.position);
        transform.LookAt(target.transform);
       
        if (!fire)
        {
            if (dest.magnitude > engageDistance)
            {
                transform.position += dest.normalized * speed * Time.deltaTime;
            }
            else {
                StartCoroutine("droneFire");
                fire = true;
            }
        }
        else
        {

        }

        
       

    }


    

    public GameObject GetBullets()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.localScale = Vector3.one;
                return bullets[i];
            }
        }
        return null;
    }




    IEnumerator droneFire()
    {
        Debug.Log("fire");
        yield return new WaitForSeconds(shotDelay);
        GameObject bull = GetBullets();
        Rigidbody rb = bull.GetComponent<Rigidbody>();
        bull.transform.position = transform.position + transform.forward * 2;
        bull.SetActive(true);
        Vector3 tempTarg = target.transform.position + new Vector3(Random.Range(0, 0.5f), Random.Range(0, 0.5f), Random.Range(0, 0.5f));
        Debug.Log("Target: " + tempTarg);
        rb.velocity = tempTarg.normalized * 2;
        fire = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            target = null;
        }
    }
}
