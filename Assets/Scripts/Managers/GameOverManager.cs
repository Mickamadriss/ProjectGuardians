using SDD.Events;
using STUDENT_NAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : Manager<GameManager>
{
    [SerializeField] private Text m_Waves;
    [SerializeField] private Text m_WavesRecord;
    int wave = 0;

    protected override void Awake()
    {
        base.Awake();
        m_WavesRecord.text = PlayerPrefs.GetInt("waveRecord").ToString();
    }

    protected override IEnumerator InitCoroutine()
    {
        yield break;
    }

    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<WaveChanged>(waveChanged);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<WaveChanged>(waveChanged);
    }

    private void waveChanged(WaveChanged e)
    {
        m_Waves.text = e.eWave.ToString();
        wave = e.eWave; // car si on est mort on a pas fini la wave
        int waveRecord = PlayerPrefs.GetInt("waveRecord");
        if(waveRecord < wave)
        {
            PlayerPrefs.SetInt("waveRecord", wave);
            waveRecord = wave;
        }
        m_WavesRecord.text = waveRecord.ToString();
    }
}
