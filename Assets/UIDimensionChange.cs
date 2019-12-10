using AdventuresUnknownSDK.Core.Utils.UnityEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class UIDimensionChange : MonoBehaviour
{
    [SerializeField] private FloatEvent m_OnWidthChangeEvent = new FloatEvent();
    [SerializeField] private FloatEvent m_OnHeightChangeEvent = new FloatEvent();
    private RectTransform m_RectTransform = null;
    #region Properties
    public FloatEvent OnWidthChangeEvent { get => m_OnWidthChangeEvent; set => m_OnWidthChangeEvent = value; }
    public FloatEvent OnHeightChangeEvent { get => m_OnHeightChangeEvent; set => m_OnHeightChangeEvent = value; }
    public RectTransform RectTransform {
        get
        {
            if (m_RectTransform == null)
            {
                m_RectTransform = GetComponent<RectTransform>();
            }
            return m_RectTransform;
        }
    }
    #endregion
    #region Methods
    private void Start()
    {
        OnWidthChangeEvent.Invoke(RectTransform.rect.width);
        OnHeightChangeEvent.Invoke(RectTransform.rect.height);
    }
    void Update()
    {
        if (RectTransform.hasChanged)
        {
            OnWidthChangeEvent.Invoke(RectTransform.rect.width);
            OnHeightChangeEvent.Invoke(RectTransform.rect.height);
        }
    }
    #endregion
}
