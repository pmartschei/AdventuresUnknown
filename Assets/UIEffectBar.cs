using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Effects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectBar : MonoBehaviour
{
    [SerializeField] private UIEffectDisplay m_EffectDisplayPrefab = null;
    [SerializeField] private Transform m_EffectTransform = null;

    private List<UIEffectDisplay> m_EffectDisplays = new List<UIEffectDisplay>();

    void Update()
    {
        EffectContext[] effectContexts = PlayerManager.PlayerController.Entity.Effects;

        if (m_EffectDisplays.Count < effectContexts.Length)
        {
            m_EffectDisplays.Add(Instantiate(m_EffectDisplayPrefab,m_EffectTransform));
        }

        int i = 0;
        for(; i < effectContexts.Length; i++)
        {
            m_EffectDisplays[i].Display(effectContexts[i]);
            m_EffectDisplays[i].gameObject.SetActive(true);
        }

        for (; i < m_EffectDisplays.Count; i++)
        {
            m_EffectDisplays[i].gameObject.SetActive(false);
        }
    }
}
