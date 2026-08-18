using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirections = new List<Vector2>();
    
    // Start is called before the first frame update
    void Start()
    {
        availableDirections.Clear();   
        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.down);
    }

    // Update is called once per frame
    void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.12f, 0, direction, 0.28f, obstacleLayer);
        if(hit.collider == null){
            availableDirections.Add(direction);
        }
    }
}
