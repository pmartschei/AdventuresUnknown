using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotateToCamera : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        NavMeshAgent navMeshAgent =  GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(new Vector3(0, 0,4));
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(-cam.transform.forward);
    }
}
