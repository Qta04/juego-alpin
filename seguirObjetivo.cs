using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class seguirObjetivo : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineVirtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
