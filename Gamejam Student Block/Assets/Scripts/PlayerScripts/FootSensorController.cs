using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSensorController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public bool groundChecker(LayerMask ground)
    {
        Vector2 center = gameObject.transform.position;
        Vector2 size = gameObject.GetComponent<BoxCollider2D> ().size;
        Collider2D groundoverlappingCollider = Physics2D.OverlapBox (center, size, 0f, ground);

        if (groundoverlappingCollider == null){
            return false;
        }
        else
        {
            return true;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
