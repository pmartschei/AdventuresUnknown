using AdventuresUnknownSDK.Core.Entities.Controllers;
using AdventuresUnknownSDK.Core.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 m_Offset = new Vector3(0, 400, 0);
    [SerializeField] private float m_Speed = 5.0f;
    // Update is called once per frame

    void LateUpdate()
    {
        EntityController playerController = PlayerManager.PlayerController;
        if (!playerController) return;
        //m_RigidBody.MovePosition(Vector3.Lerp(m_RigidBody.position, m_Offset + playerController.transform.position,Time.deltaTime * m_Speed));
        this.transform.position = Vector3.Lerp(this.transform.position, m_Offset + playerController.transform.position, Time.deltaTime * m_Speed);
    }
}
