using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Movement))]

public class Ghost : MonoBehaviour
{
    public Movement movement;
    public Transform target;
    public GhostChase chase;
    public GhostFrightened frightened;
    public GhostHome home;
    public GhostScatter scatter;
    public GhostBehavior initialBehavior;
    public int points;

    // Start is called before the first frame update
    
    void Awake()
    {
        movement = GetComponent<Movement>();  
        home = GetComponent<GhostHome>();  
        chase = GetComponent<GhostChase>();  
        frightened = GetComponent<GhostFrightened>();  
        scatter = GetComponent<GhostScatter>();  
        
    }

    public void ResetState(){
        gameObject.SetActive(true);
        movement.ResetState();
        frightened.Disable();
        chase.Disable();
        scatter.Enable();
        if(home != initialBehavior){
            home.Disable();
        }
        if(initialBehavior != null){
            initialBehavior.Enable();
        }
    }

    void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector3 position){
        position.z = transform.position.z;
        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("pacman")){
            if(frightened.enabled){
                GameManager.Instance.GhostEaten(this);
            }
            else{
                GameManager.Instance.PacmanEaten();
            }
        }
    }
}
