using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    CinemachineVirtualCamera cameraV;
    Transform player;

    // Start is called before the first frame update

    private void OnEnable()
    {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cameraV = GetComponent<CinemachineVirtualCamera>();
        cameraV.Follow = player;
    }
}
