using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftBoxScript : MonoBehaviour {
    private List<Collider2D> CList = new List<Collider2D>();

    [SerializeField]


	// Use this for initialization
	void Start () {


		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Block")
        {
            CList.Add(other);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Block")
        {
            CList.Remove(other);
        }

    }

    // Update is called once per frame
    void FixedUpdate () {

        if (CList.Count >=3) {

            GameObject.Find("KillBoxBot").transform.position = new Vector2(GameObject.Find("KillBoxBot").transform.position.x, GameObject.Find("KillBoxBot").transform.position.y+1);

        }
		
	}
}
