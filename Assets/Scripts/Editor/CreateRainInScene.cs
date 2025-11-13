using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Editor utility script to quickly add a rain particle system to the current scene.
/// Menu: Tools > Create Rain System in Scene
/// </summary>
public class CreateRainInScene
{
    [MenuItem("Tools/Create Rain System in Scene")]
    public static void CreateRain()
    {
        // Create the rain GameObject
        GameObject rainObj = new GameObject("RainParticleSystem");
        rainObj.transform.position = new Vector3(0, 15, 0);

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
        emission.rateOverTime = 150f;

        // Shape module (Box covering playground)
        ParticleSystem.ShapeModule shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(50, 1, 50);

        // Velocity module (falling downward)
        ParticleSystem.VelocityOverLifetimeModule velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.y = new ParticleSystem.MinMaxCurve(-8f);

        // Renderer
        ParticleSystemRenderer renderer = rainObj.GetComponent<ParticleSystemRenderer>();
        renderer.material = new Material(Shader.Find("Particles/Standard Unlit"));
        renderer.renderMode = ParticleSystemRenderMode.Billboard;

        // Mark scene as dirty so changes save
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        Debug.Log("✓ Rain particle system created in scene at position (0, 15, 0)");
        Selection.activeGameObject = rainObj; // Select it so you can see it in hierarchy
    }
}
