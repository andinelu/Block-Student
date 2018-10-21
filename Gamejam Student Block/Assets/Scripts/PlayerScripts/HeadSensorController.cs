using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSensorController : MonoBehaviour {

    BoxCollider2D bc2d;

    void Awake()
    {
        this.bc2d = GetComponent<BoxCollider2D> ();
    }

    // Use this for initialization
    void Start () {
		
	}

    public bool roofChecker(LayerMask ground)
    {
        Vector2 center = gameObject.transform.position;
        Vector2 size = gameObject.GetComponent<BoxCollider2D> ().size;
        Collider2D groundoverlappingCollider = Physics2D.OverlapBox (center, size, 0f, ground);

        if (groundoverlappingCollider == null)
        {
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
