using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost;
    public float duration;
    // Start is called before the first frame update
    void Awake()
    {
        ghost = GetComponent<Ghost>();
    }

    // Update is called once per frame
    public void Enable()
    {
        Enable(duration);
    }

    public virtual void Enable(float duration){
        enabled = true;
        CancelInvoke();
        Invoke("Disable", duration);
    }

    public virtual void Disable(){
        enabled = false;
        CancelInvoke();
    }
}
