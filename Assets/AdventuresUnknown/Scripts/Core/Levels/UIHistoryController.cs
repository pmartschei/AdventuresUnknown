using AdventuresUnknownSDK.Core.Objects.Levels;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventuresUnknown.Scripts.Core.Levels
{
    public class UIHistoryController : MonoBehaviour
    {
        [SerializeField] private UIHistoryDisplay m_HistoryDisplayPrefab = null;
        [SerializeField] private JourneyDataIdentifier m_JourneyData = null;

        #region Properties

        #endregion

        #region Methods
        private void OnEnable()
        {
            this.transform.Clear();
            if (m_JourneyData.ConsistencyCheck())
            {
                m_JourneyData.Object.OnCompletedLevelAdd.AddListener(OnCompletedLevelAdd);
                LoadAlreadyCompletedLevels();
            }
        }
        private void OnDisable()
        {
            if (m_JourneyData.Object)
                m_JourneyData.Object.OnCompletedLevelAdd.RemoveListener(OnCompletedLevelAdd);
        }

        private void LoadAlreadyCompletedLevels()
        {
            List<CompletedLevel> completedLevels = m_JourneyData.Object.CompletedLevels;

            foreach(CompletedLevel completedLevel in completedLevels)
            {
                OnCompletedLevelAdd(completedLevel);
            }
        }

        private void OnCompletedLevelAdd(CompletedLevel completedLevel)
        {
            UIHistoryDisplay historyDisplay = Instantiate(m_HistoryDisplayPrefab, this.transform);
            historyDisplay.Display(completedLevel);
        }
        #endregion
    }
}
