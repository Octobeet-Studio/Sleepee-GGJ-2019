using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    AudioClip audioClip;
    IPlayer player;


    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Verso della balena");
            player = collision.gameObject.GetComponent<IPlayer>();
            player.ObstaclesAudio.clip = audioClip;
            player.ObstaclesAudio.Play();
        }
    }
}
