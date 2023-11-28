using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource musicaFase;
    public AudioSource somGameOver;

    void Start()
    {
        // Comece tocando a música da fase
        PlayMusicaFase();
    }

    public void PlayMusicaFase()
    {
        musicaFase.Play();
        somGameOver.Stop();
    }

    public void PlaySomGameOver()
    {
        somGameOver.Play();
        musicaFase.Stop();
    }
}
