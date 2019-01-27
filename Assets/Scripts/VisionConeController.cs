using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeController : MonoBehaviour
{
    public float openSpeed;
    public float closeSpeed;
    public float minRange;
    public float maxRange;
    public float minAngle;
    public float maxAngle;
    //public float maxIntensity;
    public float openDelay;
    public float closeDelay;
    private Light spotLight;
    private Quaternion initialRotation;

    float currentRange;
    RaycastHit Hit;

    enum State
    {
        idle,
        opening,
        closing
    }

    private State state;
    
    // Start is called before the first frame update
    void Start()
    {
        spotLight = GetComponent<Light>();  
        state = State.idle;
        initialRotation = transform.parent.rotation;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        
        //if (maxIntensity < 33 - currentRange)
        //    spotLight.intensity = maxIntensity;
        //else
        //    spotLight.intensity = 33 - currentRange;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 10))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.yellow);
            if (Vector3.Distance(transform.position, Hit.point) < maxRange)
                currentRange = Vector3.Distance(transform.position, Hit.point);
            Debug.Log(currentRange);
        }
        else
        {
            currentRange  = maxRange;
        }

        spotLight.range = currentRange;
    }

    public void SetDirection(Vector2 direction)
    {
        if(direction != Vector2.zero)
        transform.parent.rotation = initialRotation * Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI);
    }

    public void OpenCone()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        if (state == State.idle)
        {
            state = State.opening;
            StartCoroutine(OpenConeCoroutine());
        }
    }

    public void CloseCone()
    {
        if (state == State.opening)
        {
            state = State.closing;
            StartCoroutine(CloseConeCoroutine());
        }
    }

    IEnumerator OpenConeCoroutine()
    {

        yield return new WaitForSeconds(openDelay);
        float t = 0;
        while (t < 1 && state == State.opening)
        {
            t += openSpeed * Time.deltaTime;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, 10))
                currentRange = Mathf.Lerp(minRange, Hit.point.magnitude, t);
            else
                currentRange = Mathf.Lerp(minRange, maxRange, t);
            spotLight.spotAngle = Mathf.Lerp(minAngle, maxAngle, t);
            yield return null;
        }
    }

    IEnumerator CloseConeCoroutine()
    {
        float t = 0;
        float initialRange = currentRange;
        while (t < 1)
        {
            t += closeSpeed * Time.deltaTime;
            currentRange = Mathf.Lerp(initialRange, minRange, t);
            spotLight.spotAngle = Mathf.Lerp(maxAngle, minAngle, t);
            yield return null;
        }
        state = State.idle;
        gameObject.SetActive(false);
    }
}
