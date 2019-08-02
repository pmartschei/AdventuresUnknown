using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items;
using AdventuresUnknownSDK.Core.Objects.Localization;
using Assets.AdventuresUnknown.Core.Managers;
using System;
using UnityEngine;
namespace AdventuresUnknown.Core.Managers
{
    public class StartUpManager : MonoBehaviour
    {
        [SerializeField] private ObjectsManagerImpl m_ObjectsManager = null;
        [SerializeField] private LocalizationsManagerImpl m_LocalizationsManager = null;
        [SerializeField] private PlayerManagerImpl m_PlayerManager = null;
        [SerializeField] private ModifierManagerImpl m_ModifierManager = null;
        // Use this for initialization
        void OnEnable()
        {
            if (m_ObjectsManager)
                m_ObjectsManager.PreInit();
            if (m_LocalizationsManager)
                m_LocalizationsManager.Init();
            FixLocalizationLanguages();
            if (m_ObjectsManager)
                m_ObjectsManager.Init();
            if (m_PlayerManager)
                m_PlayerManager.Init();
            if (m_ModifierManager)
                m_ModifierManager.Init();
        }

        private void FixLocalizationLanguages()
        {
            LocalizationLanguage[] localizationLanguages = ObjectsManager.GetAllObjects<LocalizationLanguage>();
            if (localizationLanguages == null) return;
            foreach(LocalizationLanguage localizationLanguage in localizationLanguages)
            {
                localizationLanguage.ForceUpdate();
            }
        }
    }
}