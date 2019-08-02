using AdventuresUnknown.Utils;
using AdventuresUnknownSDK.Core.Managers;
using AdventuresUnknownSDK.Core.Objects.Datas;
using AdventuresUnknownSDK.Core.UI.Interfaces;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdventuresUnknown
{
    public class UISaveFileDisplay : MonoBehaviour, IPointerClickHandler,IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        [SerializeField] private IGameText m_NameText = null;
        [SerializeField] private IGameText m_LevelText = null;
        [SerializeField] private IGameText m_PlayedText = null;

        private FileObject m_FileObject = null;

        private GameObject m_DragObject = null;

        public FileObject FileObject { get => m_FileObject; }

        public void Display(FileObject fileObject)
        {
            m_FileObject = fileObject;
            ContextData cd = fileObject.Get("core.datas.context") as ContextData;
            if (!cd) return;

            if (m_NameText)
            {
                m_NameText.SetText(cd.ShipName);
            }
            if (m_LevelText)
            {
                m_LevelText.SetText(cd.Level.ToString());
            }
            if (m_PlayedText)
            {
                TimeSpan timePlayed = TimeSpan.FromSeconds(cd.PlayTime);
                m_PlayedText.SetText(string.Format("{0}:{1:00}:{2:00}",
                    (int)timePlayed.TotalHours, timePlayed.Minutes, timePlayed.Seconds));
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == 2)
            {
                m_FileObject.Load();
                SceneManager.LoadScene("core.scenes.hangar");
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            eventData.useDragThreshold = false;
            m_DragObject = Instantiate(this.gameObject,this.gameObject.transform.root);
            m_DragObject.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Canvas c = m_DragObject.transform.root.GetComponent<Canvas>();
            if (c == null) return;
            Vector3 pos = c.worldCamera.ScreenToWorldPoint(eventData.position);
            pos.z = -9.7f;
            m_DragObject.transform.position = pos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (m_DragObject)
            {
                Destroy(m_DragObject);
            }
            if (eventData.used)
            {
                Destroy(gameObject);
            }
        }
    }
}