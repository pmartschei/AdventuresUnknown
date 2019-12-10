using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Objects.Localization;
using AdventuresUnknownSDK.Core.Objects.Mods;
using AdventuresUnknownSDK.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.AdventuresUnknown.Core.Managers
{
    public class GameSettingsManagerImpl : GameSettingsManager
    {
        [SerializeField] private ModTypeFormatter m_DefaultModTypeFormatter = null;
        [SerializeField] private UnityEvent m_LanguageChangeEvent = new UnityEvent();

        private LocalizationLanguage m_CurrentLanguage = null;


        #region Properties
        protected override LocalizationLanguage LanguageImpl {
            get => m_CurrentLanguage;
            set
            {
                if (value == null) return;
                if ((m_CurrentLanguage == null) ||  !m_CurrentLanguage.Equals(value))
                {
                    m_CurrentLanguage = value;
                    if (m_LanguageChangeEvent != null)
                        m_LanguageChangeEvent.Invoke();
                    //CoreObject[] assetObjects = Resources.FindObjectsOfTypeAll<CoreObject>();
                    //CoreObject[] sceneObjects = FindObjectsOfType<CoreObject>();
                    //LocalizedMonoBehaviour[] localizedBehaviours = FindObjectsOfType<LocalizedMonoBehaviour>();
                    //foreach (CoreObject assetObject in assetObjects)
                    //    assetObject.OnLanguageChange();
                    //foreach (CoreObject sceneObject in sceneObjects)
                    //    sceneObject.OnLanguageChange();
                    //foreach(LocalizedMonoBehaviour localizedBehaviour in localizedBehaviours)
                    //    localizedBehaviour.OnLanguageChange();
                }
            }
        }
        protected override UnityEvent OnLanguageChangeImpl { get => m_LanguageChangeEvent; }

        protected override ModTypeFormatter DefaultModTypeFormatterImpl => m_DefaultModTypeFormatter;
        #endregion

        #region Methods
        #endregion
    }
}
