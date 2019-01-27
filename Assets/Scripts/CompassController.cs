using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassController : MonoBehaviour
{
    Image image;
    public List<Sprite> sprites;
    GameObject player;
    GameObject bathroom;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bathroom = GameObject.FindGameObjectWithTag("Win");
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Atan2(bathroom.transform.position.y - player.transform.position.y, bathroom.transform.position.x - player.transform.position.x) * 180 / Mathf.PI;
        if (angle < 0) angle += 360;
        int index = Mathf.RoundToInt(angle / (360/sprites.Count));
        index = index % 8;
        image.sprite = sprites[index];
    }
}
