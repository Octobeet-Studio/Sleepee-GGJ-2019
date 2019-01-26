using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeController : MonoBehaviour
{
    public float openSpeed;
    public float closeSpeed;
    public float maxRange;
    public float minRange;
    public float openDelay;
    public float closeDelay;
    private Light spotLight;

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
    }

    public void OpenCone()
    {
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
            yield return null;
        }
        yield return new WaitForSeconds(closeDelay);
        CloseCone();
    }

    IEnumerator CloseConeCoroutine()
    {
        float t = 0;
        float initialRange = spotLight.range;
        while (t < 1)
        {
            t += closeSpeed * Time.deltaTime;
            spotLight.range = Mathf.Lerp(initialRange, minRange, t);
            yield return null;
        }
        state = State.idle;
    }
}
