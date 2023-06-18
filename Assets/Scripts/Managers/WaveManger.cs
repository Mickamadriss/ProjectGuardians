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
    private float StartWave = 0;
    private int ennemyCount = 0;
    private bool m_IsPlaying = false;
    
    [SerializeField] LayerMask ground;

    //wave Description
    private int WaveNumber = 0;
    [SerializeField] private float WaveDelay = 10;
    [SerializeField] private float TimeForceNextWave = 80;
    [SerializeField] private int numberEnnemiesToSpawn = 5;
    private int distancePack = 15;
    private bool forceNextWave = false;

    protected override void Awake()
    {
        base.Awake();
        SubscribeEvents();
    }

    private void Update()
    {
        if (m_IsPlaying)
        {
            if(ennemyCount <= 0 || forceNextWave)
            {
                float timeSinceLastWave = Time.time - LastEndWave;
                EventManager.Instance.Raise(new TimeNextWaveChanged() { eTime = (WaveDelay-timeSinceLastWave)/WaveDelay * 100 });
                if (timeSinceLastWave > WaveDelay || forceNextWave) //si le temps d'attente est fini
                {
                    forceNextWave = false;
                    if (SfxManager.Instance) SfxManager.Instance.PlaySfx3D(Constants.WAVE_START, gameObject.transform.position);
                    WaveNumber++;
                    TimeForceNextWave++;
                    StartWave = Time.time;
                    EventManager.Instance.Raise(new WaveChanged() { eWave = WaveNumber });

                    //zone de spawn
                    int posx;
                    int posz;
                    do
                    {
                        posx = UnityEngine.Random.Range(-150, 150);
                        posz = UnityEngine.Random.Range(-150, 150);
                    } while ((posx > 100 || posx < -100) && (posz > 100 || posz < -100));
                    Vector3 pos = new(posx + m_City.transform.position.x, 20,posz + m_City.transform.position.z);

                    for (int j = 0; j < (numberEnnemiesToSpawn + WaveNumber); j++)
                    {
                        //flou de spawn
                        Vector3 posSpawn = new(UnityEngine.Random.Range(-distancePack, distancePack) + pos.x, 20, UnityEngine.Random.Range(-distancePack, distancePack) + pos.z);
                        int random = UnityEngine.Random.Range(0, m_Ennemies.Count);

                        SpawnEnnemy(posSpawn, random);
                    }
                }
            }
            else
            {
                float timeSinceWaveStart = Time.time - StartWave;
                EventManager.Instance.Raise(new TimeNextWaveChanged() { eTime = (TimeForceNextWave - timeSinceWaveStart) / TimeForceNextWave * 100 });
                if (timeSinceWaveStart > TimeForceNextWave)
                {
                    forceNextWave = true;
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
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(mainMenuHasBeenCliked);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<EnnemyKilled>(ennemyKilled);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(mainMenuHasBeenCliked);
    }
    #endregion

    #region Event callback

    private void mainMenuHasBeenCliked(MainMenuButtonClickedEvent e)
    {
        Reset();
    }

    protected override void GameOver(GameOverEvent e)
    {
        EventManager.Instance.Raise(new WaveChanged() { eWave = WaveNumber-1}); //car si on est mort on a pas fini la wave
        Reset();
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
            AIEnnemy ennemy = Instantiate(m_Ennemies[idEnnemy], results[0].point, Quaternion.identity);
            spawnEnnemies.Add(ennemy);
            ennemy.Boost(WaveNumber);
            ennemyCount++;
            LastEndWave = Time.time;
            EventManager.Instance.Raise(new EnnemyCountChanged() { eNumberEnnemy = ennemyCount });
        }
    }

    private void Reset()
    {
        foreach (AIEnnemy aIEntity in spawnEnnemies)
        {
            Destroy(aIEntity.gameObject);
            WaveNumber = 0;
            ennemyCount = 0;
            EventManager.Instance.Raise(new EnnemyCountChanged() { eNumberEnnemy = ennemyCount });
        }
        spawnEnnemies.Clear();
        m_IsPlaying = false;
    }
}
