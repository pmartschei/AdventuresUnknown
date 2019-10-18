using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AdventuresUnknown.Core.Visuals
{
    public abstract class VisualChanger<T1> : MonoBehaviour
    {
        [SerializeField] private bool m_UpdateEachFrame = false;
        [SerializeField] private T1 m_HostileT1 = default(T1);
        [SerializeField] private T1 m_FriendlyT1 = default(T1);

        private bool m_IsFriendly = false;

        #region Properties
        public T1 HostileT1 { get => m_HostileT1; set => m_HostileT1 = value; }
        public T1 FriendlyT1 { get => m_FriendlyT1; set => m_FriendlyT1 = value; }
        #endregion

        private void Start()
        {
            bool friendly = PlayerManager.SpaceShip.gameObject.layer == this.gameObject.layer;
            m_IsFriendly = friendly;
            OnVisual(m_IsFriendly);
        }
        void Update()
        {
            bool friendly = PlayerManager.SpaceShip.gameObject.layer == this.gameObject.layer;
            if (friendly != m_IsFriendly || m_UpdateEachFrame)
            {
                m_IsFriendly = friendly;
                OnVisual(m_IsFriendly);
            }
        }
        public virtual void OnVisual(bool friendly)
        {
            if (friendly)
            {
                OnVisualFriendly();
            }
            else
            {
                OnVisualHostile();
            }
        }

        public virtual void OnVisualFriendly()
        {

        }
        public virtual void OnVisualHostile()
        {

        }
    }
    public abstract class VisualChanger<T1,T2> : VisualChanger<T1>
    {
        [SerializeField] private T2 m_HostileT2 = default(T2);
        [SerializeField] private T2 m_FriendlyT2 = default(T2);
        #region Properties
        public T2 HostileT2 { get => m_HostileT2; set => m_HostileT2 = value; }
        public T2 FriendlyT2 { get => m_FriendlyT2; set => m_FriendlyT2 = value; }
        #endregion
    }
    public abstract class VisualChanger<T1, T2, T3> : VisualChanger<T1,T2>
    {
        [SerializeField] private T3 m_HostileT3 = default(T3);
        [SerializeField] private T3 m_FriendlyT3 = default(T3);
        #region Properties
        public T3 HostileT3 { get => m_HostileT3; set => m_HostileT3 = value; }
        public T3 FriendlyT3 { get => m_FriendlyT3; set => m_FriendlyT3 = value; }
        #endregion
    }
    public abstract class VisualChanger<T1,T2,T3,T4> : VisualChanger<T1,T2,T3>
    {
        [SerializeField] private T4 m_HostileT4 = default(T4);
        [SerializeField] private T4 m_FriendlyT4 = default(T4);
        #region Properties
        public T4 HostileT4 { get => m_HostileT4; set => m_HostileT4 = value; }
        public T4 FriendlyT4 { get => m_FriendlyT4; set => m_FriendlyT4 = value; }
        #endregion
    }
}
