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
    private bool m_IsPlaying = true;

    private void Awake()
    {
        SubscribeEvents();
    }

    void OnDestroy()
    {
        UnsubscribeEvents();
    }

    #region Event's subscription

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlaying);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameResumeEvent>(GameResume);
        EventManager.Instance.AddListener<GameOverEvent>(GameIsOver);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlaying);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
        EventManager.Instance.RemoveListener<GameResumeEvent>(GameResume);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameIsOver);
    }

    #endregion

    #region Event's callback

    void GamePlaying(GamePlayEvent e)
    {
        m_IsPlaying = true;
    }

    void GameResume(GameResumeEvent e)
    {
        m_IsPlaying = true;
    }

    void GamePause(GamePauseEvent e)
    {
        m_IsPlaying = false;
    }

    void GameIsOver(GameOverEvent e)
    {
        m_IsPlaying = false;
    }

    #endregion

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
        
        // Calculer l'angle de rotation nécessaire pour faire face au mur
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Calculer la rotation à appliquer à l'objet
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

        // Appliquer la rotation à l'objet
        Rotate(newRotation.eulerAngles - transform.eulerAngles);
    }

    public void RotateAwayFrom(Transform destination)
    {
        if (!m_IsPlaying) return;
        // Calculer la direction opposée à la destination
        Vector3 direction = transform.position - destination.position;
        direction.y = 0f;

        // Calculer l'angle de rotation nécessaire pour faire face à la direction opposée
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Calculer la rotation à appliquer à l'objet
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_RotationSpeed * Time.deltaTime);

        // Appliquer la rotation à l'objet
        Rotate(newRotation.eulerAngles - transform.eulerAngles);
    }
}
