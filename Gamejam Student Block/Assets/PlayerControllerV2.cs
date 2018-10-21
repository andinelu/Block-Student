using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerControllerV2 : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [Range(0, 15f)] [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Transform m_LeftWallCheck;
    [SerializeField] private Transform m_RightWallCheck;

    const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded = false;            // Whether or not the player is grounded.
    const float k_sideRadius = .1f;
    private bool m_touchingWall = false;            
    const float k_CeilingRadius = .1f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        m_touchingWall = false;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++) //This is mostly a double failsafe over just using layers cleanly
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }

        //WALLCHECK, way excessiive - quick CP hack
        Collider2D[] collidersLSide = Physics2D.OverlapCircleAll(m_LeftWallCheck.position, k_sideRadius, m_WhatIsGround);
        Collider2D[] collidersRSide = Physics2D.OverlapCircleAll(m_RightWallCheck.position, k_sideRadius, m_WhatIsGround);
        for (int i = 0; i < collidersLSide.Length; i++) //This is mostly a double failsafe over just using layers cleanly
        {
            if (collidersLSide[i].gameObject != gameObject && collidersLSide[i].gameObject.tag == "Block")
            {
                m_touchingWall = true;
            }
        }
        for (int i = 0; i < collidersRSide.Length; i++) //This is mostly a double failsafe over just using layers cleanly
        {
            if (collidersRSide[i].gameObject != gameObject && collidersRSide[i].gameObject.tag == "Block")
            {
                m_touchingWall = true;
            }
        }

        if (m_Grounded)
        {
            Collider2D[] collidersAbove = Physics2D.OverlapCircleAll(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
            for (int i = 0; i < collidersAbove.Length; i++) //This is mostly a double failsafe over just using layers cleanly
            {
                if (collidersAbove[i].gameObject != gameObject)
                {
                    if (GameObject.Find("PlayerM") == null || GameObject.Find("PlayerM2") == null)
                    {
                        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                    }

                    GameObject.Destroy(gameObject);

                }
            }

        }
    }


    public void Move(float move, bool jump)
    {
        /* If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }*/

        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * maxSpeed, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        
        // If the player should jump...
        if ( (m_Grounded && jump) || (m_touchingWall && jump) )
        {
            // Add a vertical force to the player.

            if (m_Rigidbody2D.velocity.y <= 0)
            {
                m_Grounded = false;
                m_touchingWall = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce), ForceMode2D.Impulse );
            }
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}