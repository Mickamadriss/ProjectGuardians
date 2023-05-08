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
        [SerializeField] Text m_CityLife;
        [SerializeField] Text m_Waves;
        [SerializeField] Text m_EnnemyRemaining;
        [SerializeField] Text m_PlayerLife;
        [SerializeField] GameObject m_InteractionHUD;
        [SerializeField] TextMeshProUGUI m_InteractionPromt;

        #endregion

        #region Events' subscription
        public override void SubscribeEvents()
        {
            base.SubscribeEvents();
            EventManager.Instance.AddListener<CityLifeChanged>(cityLifeChanged);
            EventManager.Instance.AddListener<WaveChanged>(waveChanged);
            EventManager.Instance.AddListener<EnnemyCountChanged>(ennemyCountChanged);
            EventManager.Instance.AddListener<PlayerLifeChanged>(playerLifeChanged);
            EventManager.Instance.AddListener<DrawInteractionHud>(drawInteractionHUD);
            EventManager.Instance.AddListener<EraseInteractionHud>(eraseInteractionHUD);
        }

        public override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            EventManager.Instance.RemoveListener<CityLifeChanged>(cityLifeChanged);
            EventManager.Instance.RemoveListener<WaveChanged>(waveChanged);
            EventManager.Instance.RemoveListener<EnnemyCountChanged>(ennemyCountChanged);
            EventManager.Instance.RemoveListener<PlayerLifeChanged>(playerLifeChanged);
            EventManager.Instance.RemoveListener<DrawInteractionHud>(drawInteractionHUD);
            EventManager.Instance.RemoveListener<EraseInteractionHud>(eraseInteractionHUD);
        }

        #endregion

        #region Event callback
        private void cityLifeChanged(CityLifeChanged e)
        {
            m_CityLife.text = e.eLife.ToString();
        }

        private void waveChanged(WaveChanged e)
        {
            m_Waves.text = e.eWave.ToString();
        }

        private void ennemyCountChanged(EnnemyCountChanged e)
        {
            m_EnnemyRemaining.text = e.eNumberEnnemy.ToString();
        }


        private void playerLifeChanged(PlayerLifeChanged e)
        {
            m_PlayerLife.text = e.eLife.ToString();
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