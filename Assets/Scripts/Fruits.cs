using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : Pellet
{

    // Start is called before the first frame update
    protected override void Eat(){
        GameManager.Instance.FruitEat(this);
    }
}
