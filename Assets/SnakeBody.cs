using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    [SerializeField] private Transform m_PrevBodyTransform = null;
    [SerializeField] private float m_Distance = 0.5f;
    private void Start()
    {
        if (m_PrevBodyTransform == null) return;
        this.transform.SetParent(m_PrevBodyTransform.parent);
    }
    private void Update()
    {
        if (m_PrevBodyTransform == null) return;
        float distance = Vector3.Distance(this.transform.position, m_PrevBodyTransform.transform.position);
        if (distance == 0) return;
        this.transform.rotation = Quaternion.LookRotation(m_PrevBodyTransform.transform.position - this.transform.position);
        this.transform.position = Vector3.Lerp(this.transform.position, m_PrevBodyTransform.transform.position, 1 - m_Distance / distance);
    }
}
