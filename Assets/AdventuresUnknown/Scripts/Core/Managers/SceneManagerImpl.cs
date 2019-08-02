using AdventuresUnknownSDK.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;
using CoreSceneManager = AdventuresUnknownSDK.Core.Managers.SceneManager;
using AdventuresUnknownSDK.Core.Attributes;
using UnityEngine;
using AdventuresUnknownSDK.Core.Log;

namespace Assets.AdventuresUnknown.Core.Managers
{
    public class SceneManagerImpl : CoreSceneManager
    {
        [ReadOnly]
        [SerializeField] private string m_CurrentSceneName = "";
        #region Properties

        #endregion

        #region Methods
        private void Awake()
        {
            for(int i = 0; i < UnitySceneManager.sceneCount; i++)
            {
                Scene scene = UnitySceneManager.GetSceneAt(i);
                if (scene == gameObject.scene) continue;
                m_CurrentSceneName = scene.name;
            }
        }
        protected override void LoadSceneImpl(string sceneName)
        {
            if (!IsValidSceneImpl(sceneName)) return;
            if (!m_CurrentSceneName.Equals(string.Empty))
            {
                GameConsole.LogFormat("Switching scenes from {0} to {1}", m_CurrentSceneName, sceneName);
                UnitySceneManager.UnloadSceneAsync(m_CurrentSceneName);
            }
            UnitySceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            m_CurrentSceneName = sceneName;
        }

        protected override bool IsValidSceneImpl(string sceneName)
        {
            return Application.CanStreamedLevelBeLoaded(sceneName);
        }
        #endregion
    }
}
