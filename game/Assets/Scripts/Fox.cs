using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    [SerializeField] float jumpPower = 300f;


    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    const float groundCheckRadius = 0.2f;
    float horizontalValue;
    float runSpeedModifier = 2f;
    bool isRunning;
    bool facingRight = true;
    public bool isGrounded;
    bool jump;

    void Awake() {    
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void Start() {
        
    }
    
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
    
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            isRunning = true;
        } 
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            isRunning = false;
        }

        if(Input.GetButtonDown("Jump"))
            jump = true;
        else if(Input.GetButtonUp("Jump"))
            jump = false;
    }

    private void FixedUpdate() {
        Move(horizontalValue,jump);
        GroundCheck();
    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders  = Physics2D.OverlapCircleAll(
            groundCheckCollider.position,
            groundCheckRadius,
            groundLayer);
        if (colliders.Length > 0) 
            isGrounded = true;
    }

    void Move(float dir,bool jumpFlag) 
    {
        if(isGrounded && jumpFlag) 
        {
            isGrounded = false;
            jumpFlag = false;
            rb.AddForce(new Vector2(0f,jumpPower));
        }

        #region Move & Run

        float xVal = dir * speed * 100 * Time.deltaTime;
        
        if(isRunning) {
            xVal *= runSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;

        if(facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
            facingRight= false;
        }
        else if(!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1,1,1);
            facingRight = true;
        }

        animator.SetFloat("xVelocity",Mathf.Abs(rb.velocity.x));
        #endregion
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheckCollider.position,groundCheckRadius);
    }
}
