using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class UIGemLineRenderer : MonoBehaviour
{
    [SerializeField] private UILineRenderer m_UILineRenderer = null;
    [SerializeField] private Transform[] m_Transforms = null;

    private readonly int CONNECTION_COUNT = 10;

    private void OnValidate()
    {
        if (m_UILineRenderer)
        {
            m_UILineRenderer.LineList = true;
            Vector2[] points = new Vector2[CONNECTION_COUNT * 2];

            for(int i = 1; i < m_Transforms.Length; i++)
            {

                points[(i - 1) * 2] = m_Transforms[0].localPosition;
                points[(i - 1) * 2 + 1] = m_Transforms[i].localPosition;
            }

            points[10] = m_Transforms[1].localPosition;
            points[11] = m_Transforms[2].localPosition;
            points[12] = m_Transforms[2].localPosition;
            points[13] = m_Transforms[3].localPosition;
            points[14] = m_Transforms[3].localPosition;
            points[15] = m_Transforms[4].localPosition;
            points[16] = m_Transforms[4].localPosition;
            points[17] = m_Transforms[5].localPosition;
            points[18] = m_Transforms[5].localPosition;
            points[19] = m_Transforms[1].localPosition;

            m_UILineRenderer.Points = points;
        }
    }
}
