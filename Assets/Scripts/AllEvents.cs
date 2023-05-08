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
    public int eLife { get; set; }
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
}

public class WaveChanged : SDD.Events.Event
{
    public int eWave { get; set; }
}
#endregion

#region player Events

public class PlayerLifeChanged : SDD.Events.Event
{
    public int eLife { get; set; }
}

#endregion
