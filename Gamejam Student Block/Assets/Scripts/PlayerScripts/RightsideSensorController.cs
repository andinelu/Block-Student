using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightsideSensorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public bool RightSideChecker()
    {
        Vector2 center = gameObject.transform.position;
        Vector2 size = gameObject.GetComponent<BoxCollider2D> ().size;
        Collider2D firstOverlappingCollider = Physics2D.OverlapBox (center, size, 0f);
        if (firstOverlappingCollider == null)
            return false;
        if (firstOverlappingCollider.tag == "Block")
        {
            Debug.Log ("BLOCKED RIGHT SIDE!");
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
