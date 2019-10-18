using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventuresUnknown.Utils.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UICanvas : MonoBehaviour
    {
        [SerializeField] private Vector3 m_HidePosition = new Vector3(0,0,0);


        private RectTransform m_RectTransform = null;

        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
        }
        public void Show()
        {
            //gameObject.SetActive(true);
            m_RectTransform.localPosition = new Vector3(0, 0, 0);
        }
        public void Hide()
        {
            m_RectTransform.localPosition = m_HidePosition;
            //gameObject.SetActive(false);
        }
    }
}
