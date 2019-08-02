using UnityEngine;
using UnityEngine.UI;

public class UIHotkeyDisplay : MonoBehaviour
{

    [SerializeField] private Image m_Image = null;
    [SerializeField] private UIHotkeyPopup m_Source = null;
    private HotkeyDisplayData m_HotkeyDisplayData = null;
    //could be abstract
    public void Display(HotkeyDisplayData hotkeyDisplayData)
    {
        m_HotkeyDisplayData = hotkeyDisplayData;
        if (m_Image)
        {
            m_Image.sprite = hotkeyDisplayData.ActiveGem.Sprite;
        }
    }

    public void Choose()
    {
        m_Source.SelectHotKeyDisplayData(m_HotkeyDisplayData);
    }

}