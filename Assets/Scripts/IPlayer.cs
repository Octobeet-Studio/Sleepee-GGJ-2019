using UnityEngine;
using System.Collections;

public interface IPlayer
{
    GameObject gameObject { get; }
    AudioSource ObstaclesAudio { get; set; }
}
