using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(RectTransform))]
public class ItemDrop : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] private ItemStack m_ItemStack = null;

    private RectTransform m_RectTransform = null;
    private AbstractItemStackDisplay m_Display = null;
    private RectTransform m_DisplayRect = null;
    #region Properties
    public ItemStack ItemStack { get => m_ItemStack; set => m_ItemStack = value; }
    #endregion
    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    public void Collect(Inventory inventory)
    {
        if (inventory == null) return;
        if (inventory.AddItemStack(m_ItemStack))
        {
            Destroy(gameObject);
            OnPointerExit(null);
            PlayerManager.Save();
            return;
        }
        if (!m_Display) return;
        m_Display.Display(ItemStack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_Display)
        {
            Destroy(m_Display.gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Display = Instantiate(ItemStack.Item.ItemStackDisplay, this.transform.root);
        m_DisplayRect = m_Display.GetComponent<RectTransform>();
        m_Display.transform.position = new Vector3(-5000, -5000);
        StartCoroutine(m_DisplayRect.PositionNextTo(this.GetComponent<RectTransform>()));
        m_Display.Display(ItemStack);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory inventory = ObjectsManager.FindObjectByIdentifier<Inventory>("core.inventories.inventory");
        Collect(inventory);
    }
}
