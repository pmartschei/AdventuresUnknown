using AdventuresUnknown.Utils;
using AdventuresUnknownSDK.Core.Log;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.Objects.GameModes;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Logic
{
    public class UIPlayMenu : MonoBehaviour
    {
        [SerializeField] private UIGameModeDisplay m_GameModeDisplayPrefab = null;
        [SerializeField] private Transform m_GameModeTransformParent = null;
        [SerializeField] private UISaveFileDisplay m_SaveFileDisplayPrefab = null;
        [SerializeField] private Transform m_SaveFileDisplayParent = null;
        [SerializeField] private ExtensionsToggleGroup m_ExtensionsToggleGroup = null;
        [SerializeField] private Button m_DeleteButton = null;
        [SerializeField] private Button m_PlayButton = null;


        private GameMode m_SelectedGameMode = null;

        private UISaveFileDisplay m_SelectedDisplay = null;

        private void OnEnable()
        {
            GatherGameModes();
            OnToggleGroupToggleChanged(false);
        }
        private void GatherGameModes()
        {
            GameMode[] gameModes = ObjectsManager.GetAllObjects<GameMode>();

            foreach (GameMode gameMode in gameModes)
            {
                AddGameMode(gameMode);
                if (gameModes.Length == 1)
                {
                    ChooseGameMode(gameMode);
                }
            }
        }

        private void ChooseGameMode(GameMode gameMode)
        {
            m_SelectedGameMode = gameMode;
            //unregister all toggles???
            m_SaveFileDisplayParent.Clear();
            FileObject[] saves = PlayerManager.ListSaves(gameMode);
            if (saves == null) return;
            foreach (FileObject save in saves)
            {
                UISaveFileDisplay sfd = Instantiate(m_SaveFileDisplayPrefab, m_SaveFileDisplayParent);
                sfd.Display(save);
                if (m_ExtensionsToggleGroup)
                {
                    //m_ExtensionsToggleGroup.RegisterToggle(sfd.ExtensionsToggle);
                    sfd.ExtensionsToggle.Group = m_ExtensionsToggleGroup;
                }
            }
        }

        public void OnToggleGroupToggleChanged(bool b)
        {
            if (m_ExtensionsToggleGroup.SelectedToggle)
                m_SelectedDisplay = m_ExtensionsToggleGroup.SelectedToggle.GetComponent<UISaveFileDisplay>();
            if (!b) m_SelectedDisplay = null;

            if (m_DeleteButton)
                m_DeleteButton.interactable = b;
            if (m_PlayButton)
                m_PlayButton.interactable = b;
        }

        public void DeleteSelectedDisplay()
        {
            if (m_SelectedDisplay == null) return;
            m_SelectedDisplay.Delete();
            ChooseGameMode(m_SelectedGameMode);
        }
        public void LoadSelectedDisplay()
        {
            if (m_SelectedDisplay == null) return;
            m_SelectedDisplay.Load();
        }
        private void AddGameMode(GameMode gameMode)
        {
            UIGameModeDisplay uiGameModDisplay = Instantiate(m_GameModeDisplayPrefab, m_GameModeTransformParent);
            uiGameModDisplay.Display(gameMode);
        }

        public void CreateNewSaveFile()
        {
            string shipName = "ShipName";
            ContextData cd = ObjectsManager.FindObjectByIdentifier<ContextData>("core.datas.context");
            IPlayerData[] datas = ObjectsManager.GetAllObjects<IPlayerData>();
            foreach (IPlayerData data in datas)
            {
                data.Reset();
            }
            if (cd == null) return;
            cd.GameMode = m_SelectedGameMode.Identifier;
            cd.ShipName = shipName;
            cd.SaveFileName = "test" + PlayerManager.FileExtension;
            PlayerManager.Save();
        }

    }
}