using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float velocityIncrements;

    private bool hasJumped;
    private bool isGrounded;

    [SerializeField] private LayerMask groundMask;


    void Awake()
    {
        this.rb2d = GetComponent<Rigidbody2D> ();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void FixedUpdate()
    {
        
    }
}
