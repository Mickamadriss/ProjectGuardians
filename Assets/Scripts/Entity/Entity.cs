using Newtonsoft.Json.Bson;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] protected int life;
    [SerializeField] protected int maxLife;
    public bool m_IsPlaying = true;

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

    public virtual Side getSide()
    {
        return Side.Neutral;
    }
    
    public virtual void TakeDamage(int damage)
    {
        life -= damage;
        Debug.Log(gameObject + " // HP = "+life+"/"+maxLife);
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void heal(int n)
    {
        life += n;
        if (life > maxLife) life = maxLife;
    }
}
