using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIGemPanelController : MonoBehaviour
{
    [SerializeField] private RectTransform m_Parent = null;
    [SerializeField] private RectTransform m_TransformTo = null;
    [SerializeField] private RectTransform m_Child = null;


    public void OnToggle(bool v)
    {
        if (v)
        {
            m_Child.SetParent(m_TransformTo);
            m_Child.localScale = new Vector3(1, 1, 1);
            m_Child.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            m_Child.SetParent(m_Parent);
            float scaleX = m_Parent.sizeDelta.x / m_Child.sizeDelta.x;
            float scaleY = m_Parent.sizeDelta.y / m_Child.sizeDelta.y;
            m_Child.localScale = new Vector3(scaleX, scaleY, 1);
            m_Child.localPosition = new Vector3(0, 0, 0);
        }
    }
}
