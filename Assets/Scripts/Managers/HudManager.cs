﻿namespace STUDENT_NAME
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;
	using SDD.Events;

	public class HudManager : Manager<HudManager>
	{

        //[Header("HudManager")]
        #region Labels & Values
		
        [Header("Labels & Values")]
        [SerializeField] Text m_CityLife;
        [SerializeField] Text m_Waves;
        [SerializeField] Text m_EnnemyRemaining;

        #endregion

        #region Events' subscription
        public override void SubscribeEvents()
        {
            base.SubscribeEvents();
            EventManager.Instance.AddListener<CityLifeChanged>(cityLifeChanged);
            EventManager.Instance.AddListener<WaveChanged>(waveChanged);
            EventManager.Instance.AddListener<EnnemyCountChanged>(ennemyCountChanged);
        }

        public override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            EventManager.Instance.RemoveListener<CityLifeChanged>(cityLifeChanged);
            EventManager.Instance.RemoveListener<WaveChanged>(waveChanged);
            EventManager.Instance.RemoveListener<EnnemyCountChanged>(ennemyCountChanged);
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

        #endregion

        #region Manager implementation
        protected override IEnumerator InitCoroutine()
		{
			yield break;
		}
		#endregion
	}
}