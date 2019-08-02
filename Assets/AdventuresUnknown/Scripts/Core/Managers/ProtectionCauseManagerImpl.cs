using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions;
using AdventuresUnknownSDK.Core.Objects.Mods.Actions.ActionObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Managers
{
    public class ProtectionCauseManagerImpl : ProtectionCauseManager
    {
        [SerializeField] private ProtectionCause m_Block = null;

        #region Properties
        protected override ProtectionCause BlockImpl { get => m_Block; set => m_Block = value; }
        #endregion

        #region Methods

        #endregion
    }
}
