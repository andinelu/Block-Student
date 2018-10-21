using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftsideSensorController : MonoBehaviour {

    public bool collided = false;

    [SerializeField] private LayerMask ground;
	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == this.ground)
        {
            this.collided = true;
        }
        else
        {
            this.collided = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == this.ground)
        {
            this.collided = true;
        }
        else
        {
            this.collided = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.collided = false;
    }

    public bool LeftSideChecker(LayerMask ground)
    {

        Vector2 center = gameObject.transform.position;
        Vector2 size = gameObject.GetComponent<BoxCollider2D> ().size;
        //Collider2D groundoverlappingCollider = Physics2D.OverlapBox (center, size, 0f, ground);
        Collider2D[] all = Physics2D.OverlapBoxAll (center, size, 0f);

        foreach(Collider2D coll in all)
        {
            if(coll.gameObject != gameObject && coll.name != "HeadSensor")
            {
                Debug.Log (coll.name);
                if (coll.name == "Tilemap")
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
