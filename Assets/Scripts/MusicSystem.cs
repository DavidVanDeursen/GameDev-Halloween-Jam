using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicSystem : MonoBehaviour
{
    public float duration = 60f;
    private float elapsed = 0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 0.5f;
    }

    void Update()
    {
        if (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            audioSource.pitch = Mathf.Lerp(0.5f, 1f, t);
        }
    }
}