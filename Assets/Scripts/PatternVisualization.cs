using UnityEngine;
using DG.Tweening;

public class PatternVisualization : MonoBehaviour
{
    public AudioSource AudioSource;

    public void Play()
    {
        this.AudioSource.Play();

        //start effects, etc. here
    }

    public void Stop()
    {
        this.AudioSource.Stop();

        //stop effects, etc. here
    }
}