using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PaysanMouvementBehaviour : MouvementBehaviour
{
    public override bool isTarget(GameObject g)
    {
        return aggro == null && g.GetComponent<IEnnemy>() == null;
    }
}
