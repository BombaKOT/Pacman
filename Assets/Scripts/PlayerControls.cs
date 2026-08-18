using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class PlayerControls : MonoBehaviour
{
    private Movement _movement;
    private CircleCollider2D _circleCollider;
    private SpriteRenderer _spriteRenderer;
    public AnimatedSprite deathSequence;
    // Start is called before the first frame update
    void Awake(){
        _movement = GetComponent<Movement>();
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            _movement.SetDirection(Vector2.up);
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            _movement.SetDirection(Vector2.left);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            _movement.SetDirection(Vector2.right);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            _movement.SetDirection(Vector2.down);
        }
        float angle = Mathf.Atan2(_movement.direction.y, _movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);

    }

    public void ResetState(){
        enabled = true;
        _spriteRenderer.enabled = true;
        _circleCollider.enabled = true;
        deathSequence.enabled = false;
        transform.rotation = Quaternion.identity;
        _movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence(){
        enabled = false;
        _spriteRenderer.enabled = false;
        _circleCollider.enabled = false;
        _movement.direction = Vector2.right;
        _movement.enabled = false;
        deathSequence.enabled = true;
        deathSequence.Restart();
    }
}
