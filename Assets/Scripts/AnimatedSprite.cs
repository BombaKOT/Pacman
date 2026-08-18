using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationFrameTime;
    public bool loop;
    private SpriteRenderer _spriteRenderer;
    private int animationFrame;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Animation", animationFrameTime, animationFrameTime);
    }

    void Awake(){
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Animation(){
        if(!_spriteRenderer.enabled){
            return;
        } 
        animationFrame++;
        if(animationFrame >= sprites.Length && loop){
            animationFrame = 0;
        } 
        if(animationFrame >= 0 && animationFrame < sprites.Length){
            _spriteRenderer.sprite = sprites[animationFrame];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Restart(){
        animationFrame = -1;
        Animation();
    }

    void OnEnable()
    {
        _spriteRenderer.enabled = true;
    }

    void OnDisable()
    {
        _spriteRenderer.enabled = false;
    }
}
