using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float velocityIncrements;
    [SerializeField] private float m_JumpForce = 400f;

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
        if (rb2d.velocity.x < -0.01)
        {
            GetComponent<SpriteRenderer> ().flipX = true;
        }
        if (rb2d.velocity.x > 0.01)
        {
            GetComponent<SpriteRenderer> ().flipX = false;
        }
    }

    void Die()
    {

    }

    private void FixedUpdate()
    {
        bool grounded = this.isGrounded;
        this.isGrounded = false;

    }
}
