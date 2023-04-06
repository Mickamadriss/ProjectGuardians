using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BoyMouvementBehaviour : MouvementBehaviour
{
    public override bool isTarget(GameObject g)
    {
        return aggro == null && g.GetComponent<ICity>() != null;
    }
}
