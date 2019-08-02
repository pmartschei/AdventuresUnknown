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

namespace AdventuresUnknown.Core.Logic
{
    public class UIPlayMenu : MonoBehaviour
    {
        [SerializeField] private UIGameModeDisplay m_GameModeDisplayPrefab = null;
        [SerializeField] private Transform m_GameModeTransformParent = null;
        [SerializeField] private UISaveFileDisplay m_SaveFileDisplayPrefab = null;
        [SerializeField] private Transform m_SaveFileDisplayParent = null;

        private GameMode m_SelectedGameMode = null;

        private void OnEnable()
        {
            GatherGameModes();

        }
        private void GatherGameModes()
        {
            GameMode[] gameModes = ObjectsManager.GetAllObjects<GameMode>();

            foreach(GameMode gameMode in gameModes)
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
            m_SaveFileDisplayParent.Clear();
            FileObject[] saves = PlayerManager.ListSaves(gameMode);
            if (saves == null) return;
            foreach(FileObject save in saves)
            {
                UISaveFileDisplay sfd = Instantiate(m_SaveFileDisplayPrefab, m_SaveFileDisplayParent);
                sfd.Display(save);
            }
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
            foreach(IPlayerData data in datas)
            {
                data.Reset();
            }
            if (cd == null) return;
            cd.GameMode = m_SelectedGameMode.Identifier;
            cd.ShipName = shipName;
            cd.SaveFileName = "test"+PlayerManager.FileExtension;
            PlayerManager.Save();
        }

    }
}