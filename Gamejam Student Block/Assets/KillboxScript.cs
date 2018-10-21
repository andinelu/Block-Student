using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillboxScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Destroy(other.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate () {

        
		
	}
}
