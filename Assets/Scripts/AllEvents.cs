using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameOverEvent : SDD.Events.Event
{
}
public class GameVictoryEvent : SDD.Events.Event
{
}

public class GameStatisticsChangedEvent : SDD.Events.Event
{
	public float eBestScore { get; set; }
	public float eScore { get; set; }
	public int eNLives { get; set; }
}
#endregion

#region MenuManager Events
public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
public class CreditsButtonClickedEvent : SDD.Events.Event { }

public class QuitButtonCreditsClickedEvent : SDD.Events.Event { }

public class QuitButtonClickedEvent : SDD.Events.Event
{ }
#endregion

#region City Events

public class CityAttacked : SDD.Events.Event
{
    public int eDamage { get; set; }
}

public class CityLifeChanged : SDD.Events.Event
{
    public float eLife { get; set; }
}

public class DrawInteractionHud : SDD.Events.Event
{
    public string prompt { get; set; }
}

public class EraseInteractionHud : SDD.Events.Event
{
}

#endregion

#region WaveManager Events

public class EnnemyCountChanged : SDD.Events.Event
{
    public int eNumberEnnemy { get; set; }
}

public class EnnemyKilled : SDD.Events.Event
{
    public AIEnnemy eEntity { get; set; }
    public bool ePlayerKill { get; set; }
}

public class WaveChanged : SDD.Events.Event
{
    public int eWave { get; set; }
}

public class TimeNextWaveChanged : SDD.Events.Event
{
    public float eTime { get; set; }
}

#endregion

#region player Events

public class PlayerLifeChanged : SDD.Events.Event
{
    public float eLife { get; set; }
}

public class PlayerExpChanged : SDD.Events.Event
{
    public float eExp { get; set; }
}

public class PlayerGoldChanged : SDD.Events.Event
{
    public float eGold { get; set; }
}

public class PlayerGoldUpdate : SDD.Events.Event
{
    public PlayerGoldUpdate(int gold)
    {
        Gold = gold;
    }

    // Positive or negative value that will be + to player balance
    public int Gold { get; }
}

public class PlayerHealUpdate : SDD.Events.Event
{
    public PlayerHealUpdate(int health)
    {
        Health = health;
    }
    
    public int Health { get; }
}

#endregion
