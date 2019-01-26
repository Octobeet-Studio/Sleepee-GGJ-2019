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
    public float openDelay;
    public float closeDelay;
    private Light spotLight;
    private Quaternion initialRotation;

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

    public void SetDirection(Vector2 direction)
    {
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
            spotLight.range = Mathf.Lerp(minRange, maxRange, t);
            spotLight.spotAngle = Mathf.Lerp(minAngle, maxAngle, t);
            yield return null;
        }
    }

    IEnumerator CloseConeCoroutine()
    {
        float t = 0;
        float initialRange = spotLight.range;
        while (t < 1)
        {
            t += closeSpeed * Time.deltaTime;
            spotLight.range = Mathf.Lerp(initialRange, minRange, t);
            spotLight.spotAngle = Mathf.Lerp(maxAngle, minAngle, t);
            yield return null;
        }
        state = State.idle;
        gameObject.SetActive(false);
    }
}
