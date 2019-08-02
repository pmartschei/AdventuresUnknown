using AdventuresUnknownSDK.Core.Objects.GameModes;
using UnityEngine;
using UnityEngine.UI;

namespace AdventuresUnknown.Core.Logic
{
    public class UIGameModeDisplay : MonoBehaviour
    {
        [SerializeField] private TMPro.TMP_Text m_GameModeNameText = null;
        [SerializeField] private Image m_IconImage = null;

        public virtual void Display(GameMode gameMode)
        {
            if (m_GameModeNameText)
                m_GameModeNameText.text = gameMode.GameModeName;
            if (m_IconImage)
                m_IconImage.sprite = gameMode.Icon;
        }
    }
}