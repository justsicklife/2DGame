using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{

    public float speed = 1f;

    Rigidbody2D rb;
    float horizontalValue;

    void Awake() {    
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start() {
        
    }
    
    void Update()
    {
        horizontalValue = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate() {
        Move(horizontalValue);
    }

    void Move(float dir) {
        float xVal = dir * speed * 100 * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(xVal,rb.velocity.y);
        rb.velocity = targetVelocity;
    }
}
