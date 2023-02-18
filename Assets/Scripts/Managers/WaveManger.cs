using SDD.Events;
using STUDENT_NAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManger : Manager<WaveManger>
{
    //liste de AIEnnemy 
    [SerializeField] List<AIEntity> m_Ennemies;
    //Gameobject city
    [SerializeField] GameObject m_City;
    private float LastSpawnTime = 0;
    private float SpawnDelay = 5;

    protected override void Awake()
    {
        base.Awake();
        SubscribeEvents();
    }

    private void Update()
    {
        //toute les 10 secondes fait spawn un ennemy à une position aléatoire
        if (Time.time - LastSpawnTime > SpawnDelay)
        {
            int random = Random.Range(0, m_Ennemies.Count);
            Vector3 pos = new Vector3(Random.Range(-100, 100), 1, Random.Range(-100, 100));
            //set la direction de l'ennemy vers la ville
            m_Ennemies[random].destination = m_City.transform;
            Instantiate(m_Ennemies[random], pos, Quaternion.identity);
            LastSpawnTime = Time.time;
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
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
    }
    #endregion
}
