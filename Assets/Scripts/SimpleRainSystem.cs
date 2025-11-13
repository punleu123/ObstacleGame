using UnityEngine;

/// <summary>
/// Simple and lightweight rain particle system creator.
/// Usage: Just add this script to any GameObject and enable it, or call CreateRain() directly.
/// </summary>
public class SimpleRainSystem : MonoBehaviour
{
    /// <summary>
    /// Call this method to instantly create and configure a rain particle system.
    /// </summary>
    public static GameObject CreateRain(Vector3 position, Vector3 areaSize, float emissionRate = 150f)
    {
        // Create parent GameObject
        GameObject rainObj = new GameObject("Rain");
        rainObj.transform.position = position;

        // Add ParticleSystem component
        ParticleSystem ps = rainObj.AddComponent<ParticleSystem>();

        // Main module
        ParticleSystem.MainModule main = ps.main;
        main.duration = 10f;
        main.loop = true;
        main.prewarm = true;
        main.startLifetime = 5f;
        main.startSpeed = 12f;
        main.startSize = 0.15f;
        main.gravityModifier = 0.3f;

        // Emission module
        ParticleSystem.EmissionModule emission = ps.emission;
        emission.rateOverTime = emissionRate;

        // Shape module (Box)
        ParticleSystem.ShapeModule shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = areaSize;

        // Velocity module (falling downward)
        ParticleSystem.VelocityOverLifetimeModule velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.y = new ParticleSystem.MinMaxCurve(-8f);

        // Renderer
        ParticleSystemRenderer renderer = rainObj.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Particles/Standard Surface"));
        renderer.renderMode = ParticleSystemRenderMode.Billboard;

        return rainObj;
    }

    // Optional: auto-create on scene start
    private void Start()
    {
        // Uncomment and adjust to auto-create rain when scene loads
        // CreateRain(new Vector3(0, 15, 0), new Vector3(50, 1, 50), 150f);
    }
}
