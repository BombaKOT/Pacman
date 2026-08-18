using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : MonoBehaviour
{
    public int points;
    // Start is called before the first frame update
    
    protected virtual void Eat(){
        GameManager.Instance.PelletEaten(this);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("pacman")){
            Eat();
        }
    }

    
}
