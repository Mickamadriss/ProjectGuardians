using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public abstract class AIEntity : MovableEntity, IEnnemy, IEventHandler
{
    [SerializeField] public Transform destination;

    private void OnDestroy()
    {
        EventManager.Instance.Raise(new EnnemyKilled() { });
    }

    #region event subscription
    public void SubscribeEvents()
    {
        throw new System.NotImplementedException();
    }

    public void UnsubscribeEvents()
    {
        throw new System.NotImplementedException();
    }

    #endregion

}
