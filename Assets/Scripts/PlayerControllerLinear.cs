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
    [SerializeField]
    float chargeTimer;
    [SerializeField]
    float chargePower;
    [SerializeField]
    float bounceDistance;

    float currentAccelleration;

    public float maxSpeed;
    Rigidbody2D rb;
    Animator anim;

    public AudioSource ObstaclesAudio;    
    public VisionConeController visionConeController;
    public GameObject hitPrefab;

    AudioSource playerAudioSource;
    public List<AudioClip> playerHurtClips;
    public List<AudioClip> playerStepClips;
    public float stepsFrequency;
    private float timeFromLastStep;
    public float animatorSpeedCoefficient;

    Vector2 direction;

    bool stop1bool, stop2bool, isStop, Ischarge, isCharging;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        input.Player.Movement.performed += ctx => setDirection(ctx.ReadValue<Vector2>());
        input.Player.Stop1.performed += ctx => stop1(ctx.ReadValue<float>());
        input.Player.Stop2.performed += ctx => stop2(ctx.ReadValue<float>());
        input.Player.Pause.performed += ctx => pause();
        currentAccelleration = accelleration;
    }

    bool _pause = false;
    void pause()
    {
        if (!_pause)
        {
            Time.timeScale = 0;
            _pause = true;
        }
        else
        {
            Time.timeScale = 1;
            _pause = false;
        }
    }

    void stop1(float axis)
    {
        if (axis > 0.5f)
            stop1bool = true;
        else
            stop1bool = false;
    }

    void stop2(float axis)
    {
        if (axis > 0.5f)
        {
            stop2bool = true;
        }
        else
        {
            stop2bool = false;
        }
    }

    private void Update()
    {
        HandleAnimation();
        HandleStepsAudio();
        if (_pause == false)
            visionConeController.SetDirection(direction.normalized);

    }

    private void FixedUpdate()
    {
        rb.AddForce(currentAccelleration * direction);

        if (stop1bool && stop2bool)
        {
            if (!isStop)
            {
                currentAccelleration = 0;
                isStop = true;
            }
        }
        else
        {
            isCharging = false;
            if (isStop)
            {
                StopCoroutine(charge());
                currentAccelleration = accelleration;
                if (Ischarge)
                {
                    Debug.Log("Super carica");
                    rb.AddForce(chargePower * direction.normalized);
                    currentAccelleration = chargePower;
                    Ischarge = false;
                }
                //currentAccelleration = accelleration;
                isStop = false;
            }
        }

        if (rb.velocity.magnitude <= Vector2.one.magnitude * 0.1f  && stop1bool && stop2bool)
            visionConeController.OpenCone();
        else
            visionConeController.CloseCone();


        if (rb.velocity.magnitude <= 0.2 && isStop)
        {
            if (!isCharging)
                StartCoroutine(charge());
        }

        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = rb.velocity * maxSpeed / rb.velocity.magnitude;
        }
    }

    IEnumerator charge()
    {
        isCharging = true;
        Debug.Log("caricando");
        yield return new WaitForSeconds(chargeTimer);
        Ischarge = true;
        Debug.Log("Carica pronta");
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
        if (rb.velocity.magnitude < 0.2)
        {
            anim.speed = 0.5f;
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
            anim.speed = rb.velocity.magnitude * animatorSpeedCoefficient;
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
            anim.speed = rb.velocity.magnitude * animatorSpeedCoefficient;
        }
        else
        {
            anim.SetBool("Horizontal", false);
            anim.SetBool("Down", true);
            anim.SetBool("Up", false);
            anim.SetBool("Idle", false);
            anim.speed = rb.velocity.magnitude * animatorSpeedCoefficient;
        }
    }

    private void HandleStepsAudio()
    {
        timeFromLastStep += Time.deltaTime;
        if(timeFromLastStep > 1 / (stepsFrequency *rb.velocity.magnitude) && rb.velocity.magnitude>0.2)
        {
            playerAudioSource.clip = playerStepClips[Random.Range(0, playerStepClips.Count)];
            playerAudioSource.Play();
            timeFromLastStep = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 bounce = collision.GetContact(0).normal;
        bounce *= bounceDistance;
        transform.position = new Vector3(transform.position.x + bounce.x, transform.position.y + bounce.y, transform.position.z);
        if (!(stop1bool && stop2bool))
            currentAccelleration = accelleration;
        direction = Vector2.zero;
        playerAudioSource.clip = playerHurtClips[Random.Range(0, playerHurtClips.Count)];
        playerAudioSource.Play();
        GameObject.Instantiate(hitPrefab, new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 0), Quaternion.identity);
    }

}