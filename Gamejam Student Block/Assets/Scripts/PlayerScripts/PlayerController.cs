using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float velocityIncrements;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private string[] playerControls;

    private bool hasJumped;
    private bool isRoofed;
    private bool isGrounded;

    [SerializeField] public LayerMask groundMask;


    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        this.rb2d = GetComponent<Rigidbody2D> ();
        this.isGrounded = true;
        this.hasJumped = false;
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


    Vector2 CalculateHorizontalSpeed()
    {
        float horizontalAxis = Input.GetAxis (playerControls[0]);
        Vector2 newVelocity = rb2d.velocity + (horizontalAxis * velocityIncrements * Vector2.right * Time.fixedDeltaTime);
        if (horizontalAxis == 0)
        {
            newVelocity.x *= .90f * (1 - Time.fixedDeltaTime);
        }

        if (newVelocity.x < 0 && horizontalAxis > 0)
        {
            newVelocity.x *= .8f * (1 - Time.fixedDeltaTime);
        }
        if (newVelocity.x > 0 && horizontalAxis < 0)
        {
            newVelocity.x *= .8f * (1 - Time.fixedDeltaTime);
        }
        newVelocity.x = Mathf.Clamp (newVelocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);

        rb2d.velocity = newVelocity;
        return newVelocity;
    }

    void CalculateVerticalSpeed (Vector2 newVelocity)
    {
        if (Input.GetAxis (this.playerControls[1]) != 0 && this.isGrounded && rb2d.velocity.y <= 0)
        {
            if(this.hasJumped == false)
            {
                newVelocity.y = 0;
                rb2d.velocity = newVelocity;
                rb2d.AddForce (Vector2.up * m_JumpForce, ForceMode2D.Impulse);
                this.hasJumped = true;
            }
        }
    }

    void Jump()
    {
        if(gameObject.GetComponentInChildren<FootSensorController> ().groundChecker () == true)
        {
        }
    }


    void Die()
    {

    }

    private void FixedUpdate()
    {
        this.isGrounded = gameObject.GetComponentInChildren<FootSensorController> ().groundChecker ();
        if (this.isGrounded)
        {
            this.hasJumped = false;
        }
        this.isRoofed = gameObject.GetComponentInChildren<HeadSensorController> ().roofChecker ();
        Vector2 newVelocity = CalculateHorizontalSpeed ();
        CalculateVerticalSpeed (newVelocity);

        if(this.isRoofed && this.isGrounded)
        {
            this.Die ();
        }

    }
}
