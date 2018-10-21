using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurriculumController : MonoBehaviour {

    public bool taken;

	// Use this for initialization
	void Start () {
        this.taken = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            string playerid = collision.gameObject.name;
            this.taken = true;
            if (playerid.Contains ("1"))
            {
                int playernum = 1;
                GameObject.Find ("Player 1").GetComponentInChildren<PlayerController>().IncrementCurriculumn (playernum);
            }
            if(playerid.Contains("2"))
            {
                int playernum = 2;
                GameObject.Find ("Player 2").GetComponentInChildren<PlayerController> ().IncrementCurriculumn (playernum);
            }
            GameObject.Destroy (gameObject);
        }
    }
}
