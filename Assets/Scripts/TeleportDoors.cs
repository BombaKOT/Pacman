using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TeleportDoors : MonoBehaviour
{
    public Transform connection;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other){
        Vector3 position = connection.position;
        position.z = other.transform.position.z;
        other.transform.position = position;
    }


}
