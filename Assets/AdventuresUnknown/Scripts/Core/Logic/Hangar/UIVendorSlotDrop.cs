using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.UI.Items.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventuresUnknown.Scripts.Core.Logic.Hangar
{
    public class UIVendorSlotDrop : MonoBehaviour,IDropHandler
    {
        [SerializeField] IInventorySlot m_IInventorySlot = null;

        public void OnDrop(PointerEventData eventData)
        {
            IDragItemStack iDragItemStack = eventData.pointerDrag.GetComponent<IDragItemStack>();
            if (iDragItemStack == null) return;

            if (m_IInventorySlot != null &&
                m_IInventorySlot.Inventory != null)
            {

                ItemStack itemStack = iDragItemStack.IInventorySlot.Inventory.Items[iDragItemStack.IInventorySlot.Slot].Copy();
                if (itemStack.IsEmpty) return;
                iDragItemStack.IInventorySlot.Inventory.RemoveItemStack(iDragItemStack.IInventorySlot.Slot);

                PlayerManager.PlayerWallet.AddValue(itemStack.Item.CurrencyValue.Currency.Identifier, itemStack.Item.CurrencyValue.Value);
                
                m_IInventorySlot.Inventory.RemoveItemStack(m_IInventorySlot.Inventory.Size-1);
                m_IInventorySlot.Inventory.AddItemStack(itemStack);
                eventData.Use();
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
