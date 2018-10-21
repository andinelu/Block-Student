using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndstateScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        /*if (GameObject.Find("PlayerM")==null && GameObject.Find("PlayerM2")==null )
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }*/ //Put in the PlayerControllerV2 Death check instead, with an || not &&

    }
}
