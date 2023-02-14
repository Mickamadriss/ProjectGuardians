using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classicEnnemy : AIEntity, IEnnemy
{
    

    private void Update()
    {
        Move(transform.forward);
    }
}
