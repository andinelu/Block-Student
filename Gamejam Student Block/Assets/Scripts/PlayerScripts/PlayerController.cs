using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Rigidbody2D rb2d;
    [SerializeField] private float speed;
    [SerializeField] private float maxHorizontalSpeed;
    [SerializeField] private float velocityIncrements;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private string[] playerControls;
    [SerializeField] private float jumpWaitTime;
    [SerializeField] private int curriculum;
    [SerializeField] private int curriculumGoal;

    private bool rightSideObstacle;
    private bool leftSideObstacle;
    private bool hasJumped;
    private bool isRoofed;
    private bool isGrounded;
    private float lastJump;

    [SerializeField] public LayerMask groundMask;


    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        this.rb2d = GetComponent<Rigidbody2D> ();
        this.isGrounded = false;
        this.isRoofed = false;
        this.hasJumped = false;
        this.leftSideObstacle = false;
        this.rightSideObstacle = false;
    }
	
    public void IncrementCurriculumn (int playerid)
    {
        if(curriculum + 1 >= curriculumGoal)
        {
            Debug.Log ("YOU WIN!!! You may mock your opponent know!");
        }
        else
        {
            this.curriculum += 1;
            GameObject.Find ("Curriculum" + playerid).GetComponent<Text> ().text = "Player " + playerid + "'s score :" + this.curriculum;
        }
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

    // Calculates horizontal speed. Applies to walking (a,d), and left/right arrow. 

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


    // Calculates jump speed. 
    void CalculateVerticalSpeed (Vector2 newVelocity)
    {
        if (this.isGrounded || this.leftSideObstacle || this.rightSideObstacle)
        {
            this.lastJump = this.jumpWaitTime + 0.5f ;
        }
        if (this.jumpWaitTime < this.lastJump) //optional if statement
        {
            if (Input.GetAxis (this.playerControls[1]) != 0 && rb2d.velocity.y <= 10 )
            {
                if (this.isGrounded == true)
                {
                    this.lastJump = Time.fixedDeltaTime;
                    newVelocity.y = 0;
                    rb2d.velocity = newVelocity;
                    rb2d.AddForce (Vector2.up * m_JumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    // validates state of player
    void validateState()
    {
        if (this.isRoofed && this.isGrounded)
        {
            this.Die ();
        }

        if (this.isGrounded)
        {
            this.hasJumped = false;
        }
    }

    // kills the player
    void Die()
    {
        GameObject.Destroy (gameObject);
    }

    // Inbuilt physics calculation method. Too complicated to understand.
    private void FixedUpdate()
    {

        // Check "sensors" at side. Boolean variables = if something is close to that side. 
        this.isRoofed = gameObject.GetComponentInChildren<HeadSensorController> ().roofChecker (this.groundMask);
        // Debug.Log (this.isRoofed);
        this.leftSideObstacle = gameObject.GetComponentInChildren<LeftsideSensorController> ().collided;
        //Debug.Log (this.leftSideObstacle);
        this.rightSideObstacle = gameObject.GetComponentInChildren<RightsideSensorController> ().RightSideChecker ();
        this.isGrounded = gameObject.GetComponentInChildren<FootSensorController> ().groundChecker (this.groundMask);
        //Debug.Log (gameObject.name + ": ground is: " + this.isGrounded);

        // Validation of state of player  - kills the player in worst case. 
        validateState ();

        //  Calculate movement.
        Vector2 newVelocity = CalculateHorizontalSpeed ();
        CalculateVerticalSpeed (newVelocity);

    }
}
