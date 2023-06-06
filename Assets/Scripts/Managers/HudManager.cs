namespace STUDENT_NAME
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using SDD.Events;
    using System;
    using TMPro;

    public class HudManager : Manager<HudManager>
	{

        //[Header("HudManager")]
        #region Labels & Values
		
        [Header("Labels & Values")]
        [SerializeField] Text m_Waves;
        [SerializeField] Text m_EnnemyRemaining;
        [SerializeField] GameObject m_InteractionHUD;
        [SerializeField] TextMeshProUGUI m_InteractionPromt;
        [SerializeField] Slider m_TimeWave;
        [SerializeField] GameObject m_UITimeWave;
        [SerializeField] Slider m_PlayerHealth;
        [SerializeField] Slider m_CityHealth;
        [SerializeField] Slider m_PlayerExp;

        #endregion

        #region Events' subscription
        public override void SubscribeEvents()
        {
            base.SubscribeEvents();
            EventManager.Instance.AddListener<CityLifeChanged>(cityLifeChanged);
            EventManager.Instance.AddListener<WaveChanged>(waveChanged);
            EventManager.Instance.AddListener<TimeNextWaveChanged>(timeWaveChanged);
            EventManager.Instance.AddListener<EnnemyCountChanged>(ennemyCountChanged);
            EventManager.Instance.AddListener<PlayerLifeChanged>(playerLifeChanged);
            EventManager.Instance.AddListener<DrawInteractionHud>(drawInteractionHUD);
            EventManager.Instance.AddListener<EraseInteractionHud>(eraseInteractionHUD);
            EventManager.Instance.AddListener<PlayerExpChanged>(playerExpChanged);
        }

        public override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            EventManager.Instance.RemoveListener<CityLifeChanged>(cityLifeChanged);
            EventManager.Instance.RemoveListener<WaveChanged>(waveChanged);
            EventManager.Instance.RemoveListener<TimeNextWaveChanged>(timeWaveChanged);
            EventManager.Instance.RemoveListener<EnnemyCountChanged>(ennemyCountChanged);
            EventManager.Instance.RemoveListener<PlayerLifeChanged>(playerLifeChanged);
            EventManager.Instance.RemoveListener<DrawInteractionHud>(drawInteractionHUD);
            EventManager.Instance.RemoveListener<EraseInteractionHud>(eraseInteractionHUD);
            EventManager.Instance.RemoveListener<PlayerExpChanged>(playerExpChanged);
        }

        #endregion

        #region Event callback
        private void cityLifeChanged(CityLifeChanged e)
        {
            m_CityHealth.value = e.eLife;
        }

        private void waveChanged(WaveChanged e)
        {
            m_Waves.text = e.eWave.ToString();
            m_UITimeWave.gameObject.SetActive(false);
        }

        private void timeWaveChanged(TimeNextWaveChanged e)
        {
            m_TimeWave.value = e.eTime;
            m_UITimeWave.gameObject.SetActive(true);
        }

        private void ennemyCountChanged(EnnemyCountChanged e)
        {
            m_EnnemyRemaining.text = e.eNumberEnnemy.ToString();
        }


        private void playerLifeChanged(PlayerLifeChanged e)
        {
            m_PlayerHealth.value = e.eLife;
        }

        private void playerExpChanged(PlayerExpChanged e)
        {
            m_PlayerExp.value = e.eExp;
        }

        private void drawInteractionHUD(DrawInteractionHud e)
        {
            m_InteractionPromt.text = e.prompt;
            m_InteractionHUD.SetActive(true);
        }

        private void eraseInteractionHUD(EraseInteractionHud e)
        {
            m_InteractionHUD.SetActive(false);
        }

        #endregion

        #region Manager implementation
        protected override IEnumerator InitCoroutine()
		{
			yield break;
		}
		#endregion
	}
}