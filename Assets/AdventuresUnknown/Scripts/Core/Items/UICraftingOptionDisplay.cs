using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.Objects.Items.Actions;
using AdventuresUnknownSDK.Core.UI.Items;
using Assets.AdventuresUnknown.Scripts.Core.Logic.Hangar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.AdventuresUnknown.Scripts.Core.Items
{
    public class UICraftingOptionDisplay : AbstractItemStackDisplay
    {

        [SerializeField] private UICraftingActionDisplay m_CraftingActionDisplayPrefab = null;
        [SerializeField] private Transform m_PrefabParent = null;

        #region Properties

        #endregion

        #region Methods
        public override bool Display(ItemStack itemStack)
        {
            CraftingAction[] craftingActions = itemStack.GetCraftingActions();

            ClearParent();

            if (craftingActions == null) return true;


            foreach(CraftingAction craftingAction in craftingActions)
            {
                UICraftingActionDisplay instance = Instantiate(m_CraftingActionDisplayPrefab, m_PrefabParent);
                instance.Display(craftingAction,itemStack);
            }

            return true;
        }

        private void ClearParent()
        {
            foreach(Transform t in m_PrefabParent)
            {
                Destroy(t.gameObject);
            }
        }
        #endregion
    }
}
