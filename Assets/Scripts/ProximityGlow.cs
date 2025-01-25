using UnityEngine;

public class ProximityGlow : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector
    public float maxDistance = 2f; // Glow starts increasing beyond this distance
    public float minDistance = 1f; // Glow is at maximum within this distance
    public float maxEmissionIntensity = 1f; // Adjust based on HDR color

    private Material material;
    private Color baseEmissionColor;
    private Light glowLight;

    void Start()
    {
        // Configure material
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;
        baseEmissionColor = material.GetColor("_EmissionColor");
        material.EnableKeyword("_EMISSION");

        // Optional light setup
        glowLight = GetComponent<Light>();
        if (glowLight != null) glowLight.enabled = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        float glowStrength = Mathf.InverseLerp(maxDistance, minDistance, distance);
        glowStrength = Mathf.Clamp01(glowStrength);

        // Debug logs to track values
        Debug.Log($"Distance: {distance} | Glow Strength: {glowStrength} | Min: {minDistance} | Max: {maxDistance}");
    
        // Update material emission
            Color currentEmission = baseEmissionColor * Mathf.Lerp(0, maxEmissionIntensity, glowStrength);
        material.SetColor("_EmissionColor", currentEmission);

        // Update optional light
        if (glowLight != null)
        {
            glowLight.intensity = glowStrength * maxEmissionIntensity;
            glowLight.enabled = glowStrength > 0;
        }
    }

    void OnDestroy()
    {
        if (material != null)
        {
            // Reset emission when destroyed
            material.SetColor("_EmissionColor", baseEmissionColor);
        }
    }
}