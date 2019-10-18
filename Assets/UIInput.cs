using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInput : MonoBehaviour
{
    [SerializeField] private KeyCode m_KeyCode = KeyCode.None;
    [SerializeField] private UnityEvent m_OnInput = new UnityEvent();

    public KeyCode KeyCode { get => m_KeyCode; set => m_KeyCode = value; }
    public UnityEvent OnInput { get => m_OnInput; set => m_OnInput = value; }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_KeyCode))
        {
            OnInput.Invoke();
        }
    }
}
