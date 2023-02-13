using SDD.Events;
using STUDENT_NAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : Manager<CityManager>
{
    [SerializeField] private int life;

    protected override IEnumerator InitCoroutine()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        EventManager.Instance.Raise(new CityLifeChanged() { eLife = life });
    }

    #region Events' subscription
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<CityAttacked>(cityAttacked);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.AddListener<CityAttacked>(cityAttacked);
    }

    #endregion

    #region Event callback

    private void cityAttacked(CityAttacked e)
    {
        life -= e.eDamage;
        EventManager.Instance.Raise(new CityLifeChanged() { eLife = life });
    }

    #endregion
}