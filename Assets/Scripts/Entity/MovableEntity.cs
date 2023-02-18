using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public abstract class MovableEntity : Entity
{
    [SerializeField] protected float speed;
    [SerializeField] protected float m_RotationSpeed = 100f;
    [SerializeField] protected CharacterController m_CharacterController = null;

    private void TeleportTo(Transform position ) {
        //transform.position = position.position;
    }

    public void Move(UnityEngine.Vector3 mouvement)
    {
        m_CharacterController.Move(mouvement * speed * Time.fixedDeltaTime);
    }

    public void Rotate(UnityEngine.Vector3 rotation)
    {
        transform.Rotate(rotation * m_RotationSpeed * Time.fixedDeltaTime);
    }

    public void RotateTo(Transform destination)
    {
        // Calculer la direction vers le mur
        Vector3 direction = destination.position - transform.position;
        direction.y = 0f;
        
        // Calculer l'angle de rotation nécessaire pour faire face au mur
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Calculer la rotation à appliquer à l'objet
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

        // Appliquer la rotation à l'objet
        Rotate(newRotation.eulerAngles - transform.eulerAngles);
    }
}
