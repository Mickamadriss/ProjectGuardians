using SDD.Events;
using STUDENT_NAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class WaveManger : Manager<WaveManger>, IEventHandler
{
    //liste de AIEnnemy 
    [SerializeField] List<AIEnnemy> m_Ennemies;
    public GameObject player;
    //Gameobject city
    [SerializeField] GameObject m_City;
    private float LastSpawnTime = 0;
    private float SpawnDelay = 5;
    private int ennemyCount = 0;
    private bool m_IsPlaying = false;
    private List<AIEnnemy> spawnEnnemies = new List<AIEnnemy>();

    protected override void Awake()
    {
        base.Awake();
        SubscribeEvents();
    }

    private void Update()
    {
        if (m_IsPlaying)
        {
            //toute les 10 secondes fait spawn un ennemy à une position aléatoire
            if (Time.time - LastSpawnTime > SpawnDelay)
            {
                int random = Random.Range(0, m_Ennemies.Count);
                //todo FAIRE SPAWN LES ENNEMIES DIRECT SUR LE SOL POUR EVITER UN BUG LIE AU NAVMESH
                Vector3 pos = new Vector3(Random.Range(-100, 100) + m_City.transform.position.x, 20, Random.Range(-100, 100) + m_City.transform.position.z);
                //set la direction de l'ennemy vers la ville
                m_Ennemies[random].player = player.transform;
                m_Ennemies[random].city = m_City.transform;
                spawnEnnemies.Add(Instantiate(m_Ennemies[random], pos, Quaternion.identity));
                ennemyCount++;
                LastSpawnTime = Time.time;
                EventManager.Instance.Raise(new EnnemyCountChanged() { eNumberEnnemy = ennemyCount });
            }
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnsubscribeEvents();
    }

    protected override IEnumerator InitCoroutine()
    {
        yield break;
    }

    #region Events' subscription
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<EnnemyKilled>(ennemyKilled);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<EnnemyKilled>(ennemyKilled);
    }
    #endregion

    #region Event callback

    protected override void GameOver(GameOverEvent e)
    {
        foreach (AIEnnemy aIEntity in spawnEnnemies)
        {
            Destroy(aIEntity.gameObject);
        }
        m_IsPlaying = false;
    }

    private void ennemyKilled(EnnemyKilled e)
    {
        ennemyCount--;
        spawnEnnemies.Remove(e.eEntity);
        EventManager.Instance.Raise(new EnnemyCountChanged() { eNumberEnnemy = ennemyCount });
    }

    protected override void GamePlay(GamePlayEvent e)
    {
        m_IsPlaying = true;
        LastSpawnTime = Time.time;
    }

    protected override void GamePause(GamePauseEvent e)
    {
        m_IsPlaying = false;
    }

    protected override void GameResume(GameResumeEvent e)
    {
        m_IsPlaying = true;
    }

    #endregion
}
