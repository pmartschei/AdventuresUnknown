using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour
{
    [SerializeField] private GameObject m_ShowWhenOn = null;
    [SerializeField] private GameObject m_ShowWhenOff = null;

    public GameObject ShowWhenOn { get => m_ShowWhenOn; set => m_ShowWhenOn = value; }
    public GameObject ShowWhenOff { get => m_ShowWhenOff; set => m_ShowWhenOff = value; }

    public virtual void OnSwitch(bool v)
    {
        m_ShowWhenOn.SetActive(v);
        m_ShowWhenOff.SetActive(!v);
    }
}
