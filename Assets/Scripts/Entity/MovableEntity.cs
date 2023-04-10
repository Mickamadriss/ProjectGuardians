using SDD.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;
using UnityEngine.Analytics;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public abstract class MovableEntity : Entity
{
    [SerializeField] protected float speed;
    [SerializeField] protected float m_RotationSpeed = 100f;
    [SerializeField] protected CharacterController m_CharacterController = null;

    public void Move(UnityEngine.Vector3 mouvement)
    {
        if (!m_IsPlaying) return;
        m_CharacterController.Move(mouvement * speed * Time.fixedDeltaTime);
    }

    public void Rotate(UnityEngine.Vector3 rotation)
    {
        if (!m_IsPlaying) return;
        transform.Rotate(rotation * m_RotationSpeed * Time.fixedDeltaTime);
    }

    public void RotateTo(Transform destination)
    {
        if (!m_IsPlaying) return;
        // Calculer la direction vers le mur
        Vector3 direction = destination.position - transform.position;
        direction.y = 0f;
        
        // Calculer l'angle de rotation n�cessaire pour faire face au mur
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Calculer la rotation � appliquer � l'objet
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

        // Appliquer la rotation � l'objet
        Rotate(newRotation.eulerAngles - transform.eulerAngles);
    }

    public void RotateAwayFrom(Transform destination)
    {
        if (!m_IsPlaying) return;
        // Calculer la direction oppos�e � la destination
        Vector3 direction = transform.position - destination.position;
        direction.y = 0f;

        // Calculer l'angle de rotation n�cessaire pour faire face � la direction oppos�e
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Calculer la rotation � appliquer � l'objet
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

        // Appliquer la rotation � l'objet
        Rotate(newRotation.eulerAngles - transform.eulerAngles);
    }
}
