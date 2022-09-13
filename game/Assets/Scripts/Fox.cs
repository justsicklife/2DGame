using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    public float speed = 1f;

    Rigidbody2D rb;
    Animator animator;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    const float groundCheckRadius = 0.2f;
    float horizontalValue;
    float runSpeedModifier = 2f;
    bool isRunning = false;
    bool facingRight = true;
    public bool isGrounded = false;

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
    }

    private void FixedUpdate() {
        Move(horizontalValue);
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

    void Move(float dir) {
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
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheckCollider.position,groundCheckRadius);
    }
}
