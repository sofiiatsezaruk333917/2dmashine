using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float offsetX = 0;
    public GameObject player;
    public GameObject waterCamera;


    void Start()
    {
        //float height = gameObject.GetComponent<Camera>().orthographicSize * 2;
        //float width = gameObject.GetComponent<Camera>().aspect * height;
    }

    void Update()
    {
        if (player && (player.transform.position.x + offsetX) >= transform.position.x)
        {
            transform.position = new Vector3(player.transform.position.x + offsetX, transform.position.y, transform.position.z);
        }
    }

    void LateUpdate()
    {
        waterCamera.transform.position = new Vector3(transform.position.x, waterCamera.transform.position.y, waterCamera.transform.position.z);
    }
}

