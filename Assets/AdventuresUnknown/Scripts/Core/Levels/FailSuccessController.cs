﻿using AdventuresUnknownSDK.Core.Managers;
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
            JourneyData jd = ObjectsManager.FindObjectByIdentifier<JourneyData>("core.datas.journey");

            if (!jd) return;

            jd.Difficulty += 1;
        }

        private void OnFail()
        {
            PlayerManager.PlayerWallet.AddValue("core.currencies.gold", PlayerManager.PlayerWallet.GetValue("core.currencies.gold") / 10 * 8);
        }

        private void OnDisable()
        {
            LevelManager.OnFail.RemoveListener(OnFail);
            LevelManager.OnSuccess.RemoveListener(OnSuccess);
        }

        
    }
}