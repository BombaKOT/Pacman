using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed;
    public float speedMultiplier = 1;
    public Vector2 initialDirection;
    public Vector2 direction;
    public Vector2 nextDirection;
    public Vector3 startPosition;
    public Rigidbody2D rb; 
    public LayerMask obstacleLayer;
    // Start is called before the first frame update
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }
    void Start()
    {
        ResetState();
    }

    public void ResetState(){
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startPosition;
        enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector2 position = rb.position;
        Vector2 translation = speedMultiplier * speed * direction * Time.fixedDeltaTime;
        rb.MovePosition(translation + position);
    }

    public void SetDirection(Vector2 direction, bool forced = false){
        if(!Occupied(direction) || forced){
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else{
            nextDirection = direction;
        }
       
    }

    public bool Occupied(Vector2 direction){
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.16f, 0f, direction, 0.36f, obstacleLayer);
        return hit.collider != null;
    }

    void Update()
    {
        if(nextDirection != Vector2.zero){
            SetDirection(nextDirection);
        }
    }
}
