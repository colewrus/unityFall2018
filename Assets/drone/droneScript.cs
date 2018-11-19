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
    public float bulletSpeed;
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
        bull.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        bull.transform.position = transform.position + transform.forward * 2;
        bull.GetComponent<bulletScript>().target = target.transform;
        bull.GetComponent<bulletScript>().speed = bulletSpeed;
        bull.SetActive(true);
        /*
        Rigidbody rb = bull.GetComponent<Rigidbody>();
        
        bull.SetActive(true);
        Vector3 tempTarg = target.transform.position + new Vector3(Random.Range(0, 0.2f), Random.Range(0, 02f), Random.Range(0, 0.2f));
        Debug.Log("Target: " + tempTarg);
        rb.AddForce(tempTarg);
        */    
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
