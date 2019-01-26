using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerControllerLinear : MonoBehaviour
{
    [SerializeField]
    InputPlayer input;

    [SerializeField]
    float accelleration;
    [Range(0, 1)]
    [SerializeField]
    float rotationSpeed;

    float currenAccelleration;

    public float maxSpeed;
    Rigidbody2D rb;
    Animator anim;

    public AudioSource ObstaclesAudio;
    public VisionConeController visionConeController;

    Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        input.Player.Movement.performed += ctx => setDirection(ctx.ReadValue<Vector2>());
        input.Player.Stop1.performed += ctx => stop(ctx.ReadValue<float>());
        currenAccelleration = accelleration;
    }

    void stop(float axis)
    {
        if (axis > 0.8f)
            currenAccelleration = 0;
        else
            currenAccelleration = accelleration;
    }

    private void Update()
    {
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        rb.AddForce(currenAccelleration * direction);


        if (direction.x < 0.1f && direction.x > -0.1f && direction.y < 0.1f && direction.y > -0.1f)
        {
            if (visionConeController != null)
                visionConeController.OpenCone();
        }


        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity * maxSpeed / rb.velocity.magnitude;
        }
    }

    void setDirection(Vector2 _direction)
    {
        if (_direction.x > 0.6f || _direction.x < -0.6f || _direction.y > 0.6f || _direction.y < -0.6f)
        {
            direction = Vector2.Lerp(direction, _direction, rotationSpeed);
            //if (_direction.x > 0.6f || _direction.x < -0.6f || _direction.y > 0.6f || _direction.y < -0.6f)
        }
        else
        {
            direction = rb.velocity.normalized;
        }
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void HandleAnimation()
    {
        if (rb.velocity.magnitude < 0.3)
        {
            anim.SetBool("Horizontal", false);
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Idle", true);
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            anim.SetBool("Horizontal", true);
            anim.SetBool("Down", false);
            anim.SetBool("Up", false);
            anim.SetBool("Idle", false);
            if (rb.velocity.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("Horizontal", false);
            anim.SetBool("Down", false);
            anim.SetBool("Up", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Horizontal", false);
            anim.SetBool("Down", true);
            anim.SetBool("Up", false);
            anim.SetBool("Idle", false);
        }
    }
}
