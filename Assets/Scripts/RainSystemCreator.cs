using UnityEngine;

/// <summary>
/// Runtime rain particle system creator.
/// Add this script to any GameObject in your scene and it will auto-create rain on Start.
/// Or call CreateRainSystem() manually.
/// </summary>
public class RainSystemCreator : MonoBehaviour
{
    [SerializeField] private Vector3 rainPosition = new Vector3(0, 15, 0);
    [SerializeField] private Vector3 rainAreaSize = new Vector3(50, 1, 50);
    [SerializeField] private float emissionRate = 150f;
    [SerializeField] private float rainSpeed = 12f;
    [SerializeField] private float rainLifetime = 5f;
    [SerializeField] private bool autoCreateOnStart = true;

    private GameObject rainSystemObject;

    private void Start()
    {
        if (autoCreateOnStart)
        {
            CreateRainSystem();
        }
    }

    /// <summary>
    /// Create and configure the rain particle system.
    /// </summary>
    public void CreateRainSystem()
    {
        if (rainSystemObject != null)
        {
            Debug.LogWarning("Rain system already exists. Skipping creation.");
            return;
        }

        // Create GameObject
        rainSystemObject = new GameObject("RainParticleSystem");
        rainSystemObject.transform.position = rainPosition;

        // Add ParticleSystem
        ParticleSystem ps = rainSystemObject.AddComponent<ParticleSystem>();

        // Main module
        ParticleSystem.MainModule main = ps.main;
        main.duration = 10f;
        main.loop = true;
        main.prewarm = true;
        main.startLifetime = rainLifetime;
        main.startSpeed = rainSpeed;
        main.startSize = 0.15f;
        main.gravityModifier = 0.3f;

        // Emission module
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.rateOverTime = emissionRate;

        // Shape module (Box)
        ParticleSystem.ShapeModule shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = rainAreaSize;

        // Velocity module (downward)
        ParticleSystem.VelocityOverLifetimeModule velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.y = new ParticleSystem.MinMaxCurve(-8f);

        // Renderer
        ParticleSystemRenderer renderer = rainSystemObject.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        renderer.renderMode = ParticleSystemRenderMode.Billboard;

        Debug.Log($"✓ Rain particle system created at {rainPosition}");
    }

    /// <summary>
    /// Destroy the rain system.
    /// </summary>
    public void DestroyRainSystem()
    {
        if (rainSystemObject != null)
        {
            Destroy(rainSystemObject);
            Debug.Log("✓ Rain particle system destroyed");
        }
    }

    /// <summary>
    /// Toggle rain on/off.
    /// </summary>
    public void ToggleRain(bool enabled)
    {
        if (rainSystemObject != null)
        {
            rainSystemObject.SetActive(enabled);
        }
    }
}
