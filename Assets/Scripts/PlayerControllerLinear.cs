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

    public float maxSpeed;
    Rigidbody2D rb;

    public AudioSource ObstaclesAudio;
    public VisionConeController visionConeController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {

        if (Input.GetKey(left))
            rb.AddForce(movementSpeed * Vector2.left);

        if (Input.GetKey(right))
            rb.AddForce(movementSpeed * Vector2.right);

        if (Input.GetKey(up))
            rb.AddForce(movementSpeed * Vector2.up);

        if (Input.GetKey(down))
            rb.AddForce(movementSpeed * Vector2.down);

        if (Input.GetKeyUp(left) || Input.GetKeyUp(right) || Input.GetKeyUp(up) || Input.GetKeyUp(down))
            visionConeController.OpenCone();

        if(rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity * maxSpeed / rb.velocity.magnitude;
        }
    }
}
