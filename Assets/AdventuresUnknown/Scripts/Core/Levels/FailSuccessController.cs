using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventuresUnknown.Core.Levels
{
    public class FailSuccessController : MonoBehaviour
    {
        private void OnEnable()
        {
            LevelManager.OnFail.AddListener(OnFail);
            LevelManager.OnSuccess.AddListener(OnSuccess);
        }

        private void OnSuccess()
        {
            JourneyData journeyData = ObjectsManager.FindObjectByIdentifier<JourneyData>("core.datas.journey");
            VendorData vendorData = ObjectsManager.FindObjectByIdentifier<VendorData>("core.datas.vendor");
            ContextData contextData = ObjectsManager.FindObjectByIdentifier<ContextData>("core.datas.context");

            if (!journeyData) return;

            journeyData.AddCompletedLevel(LevelManager.CurrentLevel);
            journeyData.Difficulty++;
            journeyData.GenerateNextLevels();
            if (vendorData)
            {
                if (contextData)
                    vendorData.PlayerLevel = contextData.Level;
                vendorData.GenerateItems(true);
            }

            PlayerManager.Save();
        }

        private void OnFail()
        {
            PlayerManager.PlayerWallet.AddValue("core.currencies.gold", (int)(PlayerManager.PlayerWallet.GetValue("core.currencies.gold") * 0.8f));
            PlayerManager.Save();
        }

        private void OnDisable()
        {
            LevelManager.OnFail.RemoveListener(OnFail);
            LevelManager.OnSuccess.RemoveListener(OnSuccess);
        }

        
    }
}