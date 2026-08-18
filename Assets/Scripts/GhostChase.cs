using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    void OnDisable()
    {
        ghost.scatter.Enable();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if(node == null || !enabled){
            return;
        }
        Vector2 direction = Vector2.zero;
        float minDistance = float.MaxValue;
        foreach(Vector2 availableDirection in node.availableDirections){
            Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y) * 0.24f;
            float distance = Vector3.Distance(ghost.target.position, newPosition);
            if(distance < minDistance){
                minDistance = distance;
                direction = availableDirection;
            }
        }
        ghost.movement.SetDirection(direction);
    }
}
