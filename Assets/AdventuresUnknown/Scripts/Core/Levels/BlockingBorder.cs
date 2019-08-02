using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AdventuresUnknown.Core.Levels
{
    [ExecuteInEditMode]
    public class BlockingBorder : MonoBehaviour
    {

        private Vector3 m_PushingDirection = new Vector3(1, 0, 0);

        private BoxCollider m_Collider;

        public BoxCollider BoxCollider => m_Collider;
        private void Awake()
        {
            m_Collider = GetComponent<BoxCollider>();
        }
        private void OnTriggerStay(Collider other)
        {
            Vector3 pos = m_Collider.ClosestPoint(other.transform.position);
            pos -= m_Collider.transform.position;
            pos += m_Collider.transform.rotation * (m_Collider.size / 2.0f);
            Vector3 pushingDirection = m_Collider.transform.rotation * m_PushingDirection.normalized;
            pos = Vector3.Project(pos, pushingDirection);
            Vector3 movingVector = Vector3.Project(new Vector3(m_Collider.size.x, -m_Collider.size.y), pushingDirection);
            float dot = Vector3.Dot(pos, pushingDirection);
            dot = (1.0f - dot / m_Collider.size.x);
            other.attachedRigidbody.position = other.attachedRigidbody.position + movingVector * dot;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = m_Collider.transform.localToWorldMatrix;
            Gizmos.DrawWireCube(m_Collider.center, m_Collider.size);
            Gizmos.color = Color.blue;
            Vector3 arrowHead = m_Collider.center + m_PushingDirection.normalized * Mathf.Min(m_Collider.size.x, m_Collider.size.y) * 0.5f;
            float length = (arrowHead - m_Collider.center).magnitude * 0.5f;
            Gizmos.DrawLine(m_Collider.center, arrowHead);
            Vector3 rightSide = new Vector3(-1, -1, 0);
            Vector3 leftSide = new Vector3(-1, 1, 0);
            float angle = Mathf.Atan2(-m_PushingDirection.normalized.x, m_PushingDirection.normalized.y) * Mathf.Rad2Deg + 90.0f;
            rightSide = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * rightSide;
            rightSide.Normalize();
            leftSide = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1)) * leftSide;
            leftSide.Normalize();
            Gizmos.DrawLine(arrowHead, arrowHead + rightSide * length);
            Gizmos.DrawLine(arrowHead, arrowHead + leftSide * length);
        }
    }

}