using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSensorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

    public bool groundChecker ()
    {
        Vector2 center = gameObject.transform.position;
        Vector2 size = gameObject.GetComponent<BoxCollider2D> ().size;
        Collider2D firstOverlappingCollider = Physics2D.OverlapBox (center, size, 0f);
        if (firstOverlappingCollider == null)
            return false;
        if (firstOverlappingCollider.tag == "Block")
        {
            return true;
        }
        return true;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
