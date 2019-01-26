using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLinear : MonoBehaviour
{
    [SerializeField]
    float movementSpeed;

    [SerializeField]
    KeyCode up, down, right, left;

    float speed;

    public AudioSource ObstaclesAudio;
    public VisionConeController visionConeController;

    private void Update()
    {
        speed = movementSpeed * Time.deltaTime;

        if (Input.GetKey(left))
            transform.position += Vector3.left * speed;

        if (Input.GetKey(right))
            transform.position += Vector3.right * speed;

        if (Input.GetKey(up))
            transform.position += Vector3.up * speed;

        if (Input.GetKey(down))
            transform.position += Vector3.down * speed;

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right) || Input.GetKeyUp(up) || Input.GetKeyUp(down))
            visionConeController.OpenCone();
    }
}
