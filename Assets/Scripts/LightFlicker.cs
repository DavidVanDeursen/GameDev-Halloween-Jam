using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour
{
    private Light lightComponent;

    [Header("Flicker Settings")]
    [SerializeField] private float minIntensity = 0.8f;
    [SerializeField] private float maxIntensity = 1.2f;
    [SerializeField] private float flickerSpeed = 0.1f;

    [Header("Color Flicker Settings")]
    [SerializeField] private Color baseColor = new Color(1.0f, 0.55f, 0.2f);
    [SerializeField] private float colorVariation = 0.08f;

    private float targetIntensity;
    private float flickerTimer;
    private Color targetColor;

    void Start()
    {
        lightComponent = GetComponent<Light>();
        targetIntensity = lightComponent.intensity;
        targetColor = baseColor;
        lightComponent.color = baseColor;
    }

    void Update()
    {
        flickerTimer -= Time.deltaTime;
        if (flickerTimer <= 0f)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);

            // Slightly vary the color within a warm range
            float r = Mathf.Clamp01(baseColor.r + Random.Range(-colorVariation, colorVariation));
            float g = Mathf.Clamp01(baseColor.g + Random.Range(-colorVariation, colorVariation * 0.5f));
            float b = Mathf.Clamp01(baseColor.b + Random.Range(-colorVariation * 0.5f, colorVariation * 0.5f));
            targetColor = new Color(r, g, b);

            flickerTimer = flickerSpeed;
        }

        lightComponent.intensity = Mathf.Lerp(lightComponent.intensity, targetIntensity, Time.deltaTime * 10f);
        lightComponent.color = Color.Lerp(lightComponent.color, targetColor, Time.deltaTime * 10f);
    }
}