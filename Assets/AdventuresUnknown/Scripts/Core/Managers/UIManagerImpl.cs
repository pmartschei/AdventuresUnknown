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
        [SerializeField] private Transform m_HealthBarTransform = null;
        [SerializeField] private Transform m_ItemDropTransform = null;
        [SerializeField] private Transform m_EntityTransform = null;
        [SerializeField] private Transform m_AttacksTransform = null;

        #region Properties
        public override Transform OverlayImpl => m_OverlayTransform;
        public override AbstractItemStackDisplay MissingItemDisplayImpl => m_MissingItemDisplay;

        public override Transform HealthBarsTransformImpl { get => m_HealthBarTransform; set => m_HealthBarTransform = value; }
        public override Transform ItemDropsTransformImpl { get => m_ItemDropTransform; set => m_ItemDropTransform = value; }
        public override Transform EntityTransformImpl { get => m_EntityTransform; set => m_EntityTransform = value; }
        public override Transform AttacksTransformImpl { get => m_AttacksTransform; set => m_AttacksTransform = value; }
        #endregion

        #region Methods
        #endregion
    }
}
