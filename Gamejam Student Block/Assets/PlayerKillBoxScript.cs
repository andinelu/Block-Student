using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillBoxScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Destroy(other.gameObject);

        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
