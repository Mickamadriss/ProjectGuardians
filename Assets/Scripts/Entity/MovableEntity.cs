using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public abstract class MovableEntity : Entity
{
    [SerializeField] protected float speed;
    [SerializeField] protected CharacterController m_CharacterController = null;

    private void TeleportTo(Transform position) {
        //transform.position = position.position;
    }
}
