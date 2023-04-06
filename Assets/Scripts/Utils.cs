using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static bool IsFromOtherSide(GameObject gameObject, GameObject other)
    {
        if (gameObject.GetComponentInParent<IEnnemy>() == null)
            return other.GetComponentInParent<IEnnemy>() != null;
        else
            return other.GetComponentInParent<IEnnemy>() == null;
    }
}