using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionConeController : MonoBehaviour
{
    public GameObject leftSprite;
    public GameObject rightSprite;
    public float closeSpeed;
    public float angle;
    public float openDelay;

    bool open;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCone()
    {
        if (!open)
        {
            open = true;
            //leftSprite.transform.localRotation = Quaternion.Euler(0, 0, angle / 2);
            //rightSprite.transform.localRotation = Quaternion.Euler(0, 0, -angle / 2);
            StartCoroutine(OpenConeCoroutine());
        }
    }

    IEnumerator OpenConeCoroutine()
    {
        Quaternion targetRotationLeft = Quaternion.Euler(0, 0, angle / 2);
        Quaternion targetRotationRight = Quaternion.Euler(0, 0, -angle / 2);
        Quaternion startRotation = Quaternion.identity;
        float t = 0;
        yield return new WaitForSeconds(openDelay);
        while (t < 1)
        {
            t += closeSpeed * Time.deltaTime;
            leftSprite.transform.localRotation = Quaternion.Lerp(startRotation, targetRotationLeft, t);
            rightSprite.transform.localRotation = Quaternion.Lerp(startRotation, targetRotationRight, t);
            yield return null;
        }
        StartCoroutine(CloseCone());
    }

    IEnumerator CloseCone()
    {
        Quaternion startRotationLeft = leftSprite.transform.localRotation;
        Quaternion startRotationRight = rightSprite.transform.localRotation;
        Quaternion targetRotation = Quaternion.identity;
        float t = 0;
        while (t < 1)
        {
            t += closeSpeed * Time.deltaTime;
            leftSprite.transform.localRotation = Quaternion.Lerp(startRotationLeft, targetRotation, t);
            rightSprite.transform.localRotation = Quaternion.Lerp(startRotationRight, targetRotation, t);
            yield return null;
        }
        open = false;
    }
}
