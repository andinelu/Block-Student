using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [SerializeField] private string horizontalAxis;
    [SerializeField] private string verticalAxis;
    private bool jump;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        jump = false;

        GetComponent<PlayerControllerV2>().Move(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis) > 0 );

		
	}
}
