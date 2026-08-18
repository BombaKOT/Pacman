using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite right;
    public Sprite left;
    public Sprite down;
    private SpriteRenderer _spriteRenderer;
    private Movement _movement;
    // Start is called before the first frame update
    void Awake()
    {
        _movement = GetComponentInParent<Movement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_movement.direction == Vector2.up){
            _spriteRenderer.sprite = up;
        }
        if(_movement.direction == Vector2.right){
            _spriteRenderer.sprite = right;
        }
        if(_movement.direction == Vector2.left){
            _spriteRenderer.sprite = left;
        }
        if(_movement.direction == Vector2.down){
            _spriteRenderer.sprite = down;
        }
        
    }
}
