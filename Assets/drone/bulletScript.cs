using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour {

    public float speed;
    public Transform target;
    Rigidbody rb;


	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = target.position.normalized * speed;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
