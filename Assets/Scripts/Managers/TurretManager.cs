
using System.Collections;
using System.Collections.Generic;
using SDD.Events;
using UnityEngine;
using Event = SDD.Events.Event;

public class TurretManager: Manager<TurretManager>
{
    private List<GameObject> turrets = new List<GameObject>();

    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<RegisterTurret>(RegisterTurret);
        EventManager.Instance.AddListener<GameOverEvent>(GameOverEvent);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<RegisterTurret>(RegisterTurret);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOverEvent);

    }

    private void RegisterTurret(RegisterTurret e)
    {
        turrets.Add(e.Turret);
    }

    private void GameOverEvent(GameOverEvent e)
    {
        foreach (var turret in turrets)
        {
            Destroy(turret);
        }
        turrets.Clear();
    }

    protected override IEnumerator InitCoroutine()
    {
        yield break;
    }
    
    
}
