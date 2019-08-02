using AdventuresUnknownSDK.Core.Entities;
using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.AdventuresUnknown.Core.Managers
{
    public class CooldownManagerImpl : CooldownManager
    {
        //could be made efficient if we just store cooldowns different
        //atm: cd1 has 1 sec
        //     cd2 has 2 sec
        //new: cd1 has 1 sec
        //     cd2 has 1 sec
        //only redurce the cd of cd1
        [SerializeField] private List<TimerObject> m_Cooldowns = new List<TimerObject>();
        //could be better
        private void Update()
        {
            if (Time.deltaTime <= 0.0f) return;
            for(int i = 0; i < m_Cooldowns.Count; i++)
            {
                m_Cooldowns[i].Update(Time.deltaTime);
                if (m_Cooldowns[i].IsFinished())
                {
                    m_Cooldowns.RemoveAt(i);
                    i--;
                }
            }
        }
        protected override void AddCooldownImpl(object source, float duration)
        {
            if (HasCooldownImpl(source)) return;
            m_Cooldowns.Add(new TimerObject(source, duration));
        }
        //could be better
        protected override bool HasCooldownImpl(object source)
        {
            foreach(TimerObject cooldown in m_Cooldowns)
            {
                if (cooldown.Source == source) return true;
            }
            return false;
        }

        protected override float GetCooldownImpl(object source)
        {
            foreach (TimerObject cooldown in m_Cooldowns)
            {
                if (cooldown.Source == source) return cooldown.Duration;
            }
            return 0.0f;
        }
    }
}