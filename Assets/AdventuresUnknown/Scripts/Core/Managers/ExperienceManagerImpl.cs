using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Experience;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AdventuresUnknown.Scripts.Core.Managers
{
    public class ExperienceManagerImpl : ExperienceManager
    {

        [SerializeField] private ExperienceController m_ExperienceController = null;
        public override int MaxLevelImpl => m_ExperienceController.MaxLevel;

        public override int GetExperienceForLevelImpl(int level)
        {
            return m_ExperienceController.GetExperienceForLevel(level);
        }
    }
}
