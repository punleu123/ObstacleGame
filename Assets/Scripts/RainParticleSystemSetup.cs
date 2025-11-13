using UnityEngine;

/// <summary>
/// Automatically creates and configures a rain particle system.
/// Add this script to any GameObject and call SetupRain() in Start() or via editor button.
/// </summary>
public class RainParticleSystemSetup : MonoBehaviour
{
    [SerializeField] private Vector3 rainPosition = new Vector3(0, 10, 0);
    [SerializeField] private Vector3 rainBoxScale = new Vector3(20, 1, 20);
    [SerializeField] private float emissionRate = 150f;
    [SerializeField] private float rainSpeed = 12f;
    [SerializeField] private float rainLifetime = 5f;

    private GameObject rainSystemObject;
    private ParticleSystem rainParticleSystem;

    private void Start()
    {
        // Uncomment to auto-setup on play
        // SetupRain();
    }

    /// <summary>
    /// Creates the rain particle system with predefined settings.
    /// Call this from the editor or from code.
    /// </summary>
    public void SetupRain()
    {
        // Create the GameObject
        rainSystemObject = new GameObject("RainParticleSystem");
        rainSystemObject.transform.position = rainPosition;

        // Add Particle System component
        rainParticleSystem = rainSystemObject.AddComponent<ParticleSystem>();

        // Configure main particle system settings
        ConfigureMainModule();
        ConfigureEmissionModule();
        ConfigureShapeModule();
        ConfigureVelocityModule();
        ConfigureRendererModule();

        Debug.Log("Rain particle system created at " + rainPosition);
    }

    private void ConfigureMainModule()
    {
        var main = rainParticleSystem.main;
        main.duration = 10f;
        main.loop = true;
        main.prewarm = true;
        main.startLifetime = new ParticleSystem.MinMaxCurve(rainLifetime);
        main.startSpeed = new ParticleSystem.MinMaxCurve(rainSpeed);
        main.startSize = new ParticleSystem.MinMaxCurve(0.15f);
        main.gravityModifier = 0.3f; // slight gravity for realism
    }

    private void ConfigureEmissionModule()
    {
        var emission = rainParticleSystem.emission;
        emission.enabled = true;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(emissionRate);
    }

    private void ConfigureShapeModule()
    {
        var shape = rainParticleSystem.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = rainBoxScale;
    }

    private void ConfigureVelocityModule()
    {
        var velocity = rainParticleSystem.velocityOverLifetime;
        velocity.enabled = true;
        velocity.x = new ParticleSystem.MinMaxCurve(0);
        velocity.y = new ParticleSystem.MinMaxCurve(-8f); // downward velocity
        velocity.z = new ParticleSystem.MinMaxCurve(0);
    }

    private void ConfigureRendererModule()
    {
        // Get or create a simple particle material
        var renderer = rainParticleSystem.GetComponent<ParticleSystemRenderer>();
        if (renderer != null)
        {
            // Try to use the default particle material
            renderer.material = new Material(Shader.Find("Particles/Standard Surface"));
            renderer.renderMode = ParticleSystemRenderMode.Billboard;
        }
    }

    /// <summary>
    /// Destroy the rain system (optional cleanup).
    /// </summary>
    public void DestroyRain()
    {
        if (rainSystemObject != null)
        {
            Destroy(rainSystemObject);
        }
    }
}
