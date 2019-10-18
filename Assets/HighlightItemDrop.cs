using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Inventories;
using AdventuresUnknownSDK.Core.UI.Items;
using AdventuresUnknownSDK.Core.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightItemDrop : MonoBehaviour
{

    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private Canvas m_Canvas = null;

    private AbstractItemStackDisplay m_Display = null;
    private RectTransform m_DisplayRect = null;
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            DestroyDisplay();
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, m_LayerMask.value))
        {
            ItemDrop itemDrop = raycastHit.collider.GetComponent<ItemDrop>();
            if (itemDrop)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Inventory inventory = ObjectsManager.FindObjectByIdentifier<Inventory>("core.inventories.inventory");
                    if (itemDrop.Collect(inventory))
                    {
                        DestroyDisplay();
                    }
                }
                if (!m_Display)
                {
                    m_Display = Instantiate(itemDrop.ItemStack.Item.ItemStackDisplay, m_Canvas.transform);
                    m_DisplayRect = m_Display.GetComponent<RectTransform>();
                    m_Display.transform.position = new Vector3(-5000, -5000);
                }
                Vector3 pos = Input.mousePosition;
                pos.z = m_Canvas.planeDistance;
                StartCoroutine(m_DisplayRect.PositionNextTo(m_Canvas.worldCamera.ScreenToWorldPoint(pos),RectTransformExtension.PositionAlignment.Top,new Vector2(25,25)));
                m_Display.Display(itemDrop.ItemStack);
            }
            else
            {
                DestroyDisplay();
            }
        }
        else
        {
            DestroyDisplay();
        }
    }

    private void DestroyDisplay()
    {
        if (m_Display)
        {
            Destroy(m_Display.gameObject);
        }
    }
}
