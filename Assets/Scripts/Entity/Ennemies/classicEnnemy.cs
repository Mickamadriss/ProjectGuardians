using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class classicEnnemy : AIEntity, IEnnemy
{
    private void Update()
    {
        m_CharacterController.Move(transform.forward * speed * Time.fixedDeltaTime);
    }
}
