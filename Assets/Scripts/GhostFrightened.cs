using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;
    private bool _eaten;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Enable(float duration)
    {
        base.Enable(duration);
        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;
        Invoke("Flash", duration * 0.675f);
    }

    void Flash(){
        if(!_eaten){
            blue.enabled = false;
            white.enabled = true; 
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    public override void Disable()
    {
        base.Disable();
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Eaten(){
        _eaten = true;
        ghost.SetPosition(ghost.home.inside.position);
        ghost.home.Enable(duration);
        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    void OnEnable()
    {
        blue.GetComponent<AnimatedSprite>().Restart();
        _eaten = false;   
        ghost.movement.speedMultiplier = 0.75f;   
    }

    void OnDisable()
    {
        _eaten = false;
        ghost.movement.speedMultiplier = 1;   
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       
        Node node = collision.GetComponent<Node>();
        if(node == null || !enabled || _eaten){
            return;
        }
        Vector2 direction = Vector2.zero;
        float maxDistance = float.MinValue;
        foreach(Vector2 availableDirection in node.availableDirections){
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y) * 0.24f;
            float distance = Vector3.Distance(ghost.target.position, newPosition);
            if(distance > maxDistance){
                maxDistance = distance;
                direction = availableDirection;
            }
        }
        ghost.movement.SetDirection(direction);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("pacman")){
            if(enabled){
                Eaten();
            }
        }
    }
}
