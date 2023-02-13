using SDD.Events;
using STUDENT_NAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : Manager<CityManager>
{
    [SerializeField] private int life;
    [SerializeField] private Text lifeText;

    protected override IEnumerator InitCoroutine()
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        lifeText.text = life.ToString();
    }

    #region Events' subscription
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<CityLifeChanged>(changeLife);
        EventManager.Instance.AddListener<CityAttacked>(cityAttacked);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<CityLifeChanged>(changeLife);
        EventManager.Instance.AddListener<CityAttacked>(cityAttacked);
    }

    #endregion

    #region Event callback

    private void cityAttacked(CityAttacked e)
    {
        life -= e.eLife;
        lifeText.text = life.ToString();
    }

    private void changeLife(CityLifeChanged e)
    {
        life -= e.eLife;
        lifeText.text = life.ToString();
    }

    #endregion
}