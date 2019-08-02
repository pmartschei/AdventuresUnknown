using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Items.Actions;
using AdventuresUnknownSDK.Core.Utils.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.AdventuresUnknown.Scripts.Core.Managers
{
    public class CraftingActionManagerImpl : CraftingActionManager
    {
        [SerializeField] private CraftingActionCatalogIdentifier m_DefaultCraftingActionCatalog = null;

        #region Properties

        #endregion

        #region Methods
        protected override CraftingAction[] GetDefaultCraftingActionsImpl(string itemTypeIdentifier)
        {
            if (m_DefaultCraftingActionCatalog.ConsistencyCheck())
            {
                return m_DefaultCraftingActionCatalog.Object.CraftingActions;
            }
            return null;
        }
        #endregion
    }
}
