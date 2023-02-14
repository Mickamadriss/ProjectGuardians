using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicWeaponBehviour : MonoBehaviour, WeaponBehaviour
{
    private bool inRange;
    
    public bool isInRange()
    {
        return inRange;
    }

    public void useAttack()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEnnemy>() == null)
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IEnnemy>() == null)
        {
            inRange = false;
        }
    }
}
