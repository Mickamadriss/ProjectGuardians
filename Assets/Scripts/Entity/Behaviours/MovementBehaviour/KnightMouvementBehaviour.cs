using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class KnightMouvementBehaviour : MouvementBehaviour
{
    public override bool isTarget(GameObject g)
    {
        return (aggro == null || (aggro != null && aggro.GetComponent<ICity>() != null)) && g.GetComponentInParent<IAlly>() != null;
    }
}
