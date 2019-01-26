using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerGrid : MonoBehaviour
{
    public float gridSize;
    public float speed;
    public VisionConeController visionConeController;
    bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;
        if (!moving) {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement += Vector2.left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement += Vector2.right;
            }
            if (movement == Vector2.zero)
            {
                if(Input.GetKey(KeyCode.UpArrow))
                {
                    movement += Vector2.up;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    movement += Vector2.down;
                }
            }
            if (movement != Vector2.zero)
            {
                moving = true;
                StartCoroutine(MoveOnGrid(movement * gridSize));
            }
        }
    }

    IEnumerator MoveOnGrid(Vector2 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x + direction.x, startPosition.y + direction.y, startPosition.z);
        print("start: " + startPosition);
        print("target:" + targetPosition);
        float t = 0;
        float step = (targetPosition - startPosition).magnitude;
        while (transform.position != targetPosition)
        {
            print("current:" + transform.position);
            t += step * Time.deltaTime * speed;
            print("t:" + t);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }
        moving = false;
        visionConeController.OpenCone();
    }
}
