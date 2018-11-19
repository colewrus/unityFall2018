using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float speed;
    public Transform target;
    [Tooltip("How long the bullet exists before it disappears")]
    public float lifetime;
    Rigidbody rb;


	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        if(target != null)
        {
            rb = GetComponent<Rigidbody>();
            transform.LookAt(target);
            rb.AddForce(transform.forward * speed);
        }


    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator DelayDeactivate()
    {
        yield return new WaitForSeconds(lifetime);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.gameObject.SetActive(false);
    }
}
