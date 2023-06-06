using SDD.Events;
using STUDENT_NAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityManager : Manager<CityManager>
{
    [SerializeField] private float MaxLife;
    private float life;

    protected override IEnumerator InitCoroutine()
    {
        throw new System.NotImplementedException();
    }

    #region Events' subscription
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<CityAttacked>(cityAttacked);
        EventManager.Instance.AddListener<GamePlayEvent>(gameStarted);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<CityAttacked>(cityAttacked);
        EventManager.Instance.RemoveListener<GamePlayEvent>(gameStarted);

    }

    #endregion

    #region Event callback

    private void cityAttacked(CityAttacked e)
    {
        life -= e.eDamage;
        if(life <= 0)
        {
            life = 0;
            EventManager.Instance.Raise(new GameOverEvent());
        }
        EventManager.Instance.Raise(new CityLifeChanged() { eLife = life / MaxLife * 100 });
    }

    private void gameStarted(GamePlayEvent e)
    {
        life = MaxLife;
        EventManager.Instance.Raise(new CityLifeChanged() { eLife = life / MaxLife * 100 });
    }

    #endregion
}