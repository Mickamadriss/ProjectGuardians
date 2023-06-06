using SDD.Events;
using STUDENT_NAME;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveManger : Manager<WaveManger>, IEventHandler
{
    //liste de AIEnnemy 
    [SerializeField] List<AIEnnemy> m_Ennemies;
    private List<AIEnnemy> spawnEnnemies = new List<AIEnnemy>();

    public GameObject player;

    //Gameobject city
    [SerializeField] GameObject m_City;

    //parameters
    private float LastEndWave = 0;
    private int ennemyCount = 0;
    private bool m_IsPlaying = false;
    
    [SerializeField] LayerMask ground;

    //wave Description
    private int WaveNumber = 0;
    [SerializeField] private float WaveDelay = 10;
    [SerializeField] private int numberEnnemiesToSpawn = 5;

    protected override void Awake()
    {
        base.Awake();
        SubscribeEvents();
    }

    private void Update()
    {
        if (m_IsPlaying)
        {
            if(ennemyCount == 0)
            {
                float timeSinceLastWave = Time.time - LastEndWave;
                EventManager.Instance.Raise(new TimeNextWaveChanged() { eTime = (WaveDelay-timeSinceLastWave)/WaveDelay * 100 });
                if (timeSinceLastWave > WaveDelay) //si le temps d'attente est fini
                {
                    WaveNumber++;
                    EventManager.Instance.Raise(new WaveChanged() { eWave = WaveNumber });

                    //zone de spawn
                    Vector3 pos = new Vector3(UnityEngine.Random.Range(-100, 100) + m_City.transform.position.x, 20, UnityEngine.Random.Range(-100, 100) + m_City.transform.position.z);
                    for (int j = 0; j < (numberEnnemiesToSpawn + WaveNumber); j++)
                    {
                        //flou de spawn
                        Vector3 posSpawn = new Vector3(UnityEngine.Random.Range(-5, 5) + pos.x, 20, UnityEngine.Random.Range(-5, 5) + pos.z);
                        int random = UnityEngine.Random.Range(0, m_Ennemies.Count);

                        SpawnEnnemy(posSpawn, random);
                    }
                }
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
            WaveNumber = 0;
            EventManager.Instance.Raise(new WaveChanged() { eWave = WaveNumber });
        }
        m_IsPlaying = false;
    }

    private void ennemyKilled(EnnemyKilled e)
    {
        ennemyCount--;
        spawnEnnemies.Remove(e.eEntity);
        EventManager.Instance.Raise(new EnnemyCountChanged() { eNumberEnnemy = ennemyCount });
        if (ennemyCount == 0)
        {
            LastEndWave = Time.time;
        }
    }

    protected override void GamePlay(GamePlayEvent e)
    {
        m_IsPlaying = true;
        LastEndWave = Time.time;
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

    private void SpawnEnnemy(Vector3 coords, int idEnnemy)
    {
        //Trouve le point sur le sol où faire spawn l'ennemie
        Ray ray = new Ray(coords, Vector3.down);
        RaycastHit[] results = new RaycastHit[3];
        int i = Physics.RaycastNonAlloc(ray, results, 100, ground);
        if (i > 0)
        {
            //set la direction de l'ennemy vers la ville
            m_Ennemies[idEnnemy].player = player.transform;
            m_Ennemies[idEnnemy].city = m_City.transform;
            spawnEnnemies.Add(Instantiate(m_Ennemies[idEnnemy], results[0].point, Quaternion.identity));
            ennemyCount++;
            LastEndWave = Time.time;
            EventManager.Instance.Raise(new EnnemyCountChanged() { eNumberEnnemy = ennemyCount });
        }
    }
}
