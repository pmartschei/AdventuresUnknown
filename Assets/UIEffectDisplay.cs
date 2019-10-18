using AdventuresUnknownSDK.Core.Objects.Effects;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectDisplay : MonoBehaviour
{

    [SerializeField] private Image m_Sprite = null;
    [SerializeField] private GameObject m_TimerObject = null;
    [SerializeField] private IGameText m_TimerText = null;


    public void Display(EffectContext effectContext)
    {
        if (effectContext == null) return;

        if (m_Sprite)
        {
            m_Sprite.sprite = effectContext.Effect.Icon;
        }
        m_TimerObject.SetActive(!effectContext.IsInfinityDuration);
        if (!effectContext.IsInfinityDuration)
        {
            m_TimerText.SetText(effectContext.Duration);
        }
    }
}
