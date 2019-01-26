using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private float z;

    private void Start()
    {
        z = transform.position.z;
    }
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, z);
    }

    //public void Shake()
    //{
    //    StartCoroutine(ShakeCoroutine());
    //}

    //IEnumerator ShakeCoroutine()
    //{

    //}
}
