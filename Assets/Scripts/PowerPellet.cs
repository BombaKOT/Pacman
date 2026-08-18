using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration = 8;
    // Start is called before the first frame update
    protected override void Eat()
    {
        GameManager.Instance.PowerPelletEaten(this);
    }

    
}
