using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects;
using AdventuresUnknownSDK.Core.Objects.Localization;
using AdventuresUnknownSDK.Core.Objects.Mods;
using AdventuresUnknownSDK.Core.UI.Items;
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
    public class UIManagerImpl : UIManager
    {

        [SerializeField] private Transform m_OverlayTransform = null;
        [SerializeField] private AbstractItemStackDisplay m_MissingItemDisplay = null;

        #region Properties
        public override Transform OverlayImpl => m_OverlayTransform;
        public override AbstractItemStackDisplay MissingItemDisplayImpl => m_MissingItemDisplay;
        #endregion

        #region Methods
        public void Test()
        {
            Debug.Log("raised");
        }
        #endregion
    }
}
