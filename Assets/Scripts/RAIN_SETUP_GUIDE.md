# Rain Particle System Setup Guide

## Quick Start Options

### **Option A: Manual Setup (5–10 minutes)**

1. **Create empty GameObject**
   - Right-click Hierarchy → Create Empty
   - Rename to `RainParticleSystem`
   - Set Position to (0, 10, 0)

2. **Add Particle System**
   - Select the GameObject
   - Inspector → Add Component → Particle System

3. **Configure in Inspector**
   - **Main:** Duration=10, Loop=✓, Prewarm=✓, Start Lifetime=5, Start Speed=12
   - **Emission:** Rate over Time = 150
   - **Shape:** Type=Box, Scale=(20, 1, 20)
   - **Velocity over Lifetime:** Y = -8 (downward)
   - **Renderer:** Billboard mode, use default particle material

### **Option B: Script Setup (2 minutes)**

#### Method 1: Using RainParticleSystemSetup.cs
1. Add the `RainParticleSystemSetup` script to any GameObject (e.g., GameManager or SceneController)
2. In your code, call:
   ```csharp
   GetComponent<RainParticleSystemSetup>().SetupRain();
   ```
3. Or in Start() method, uncomment the SetupRain() call

#### Method 2: Using SimpleRainSystem.cs (Recommended for quick use)
1. Add `SimpleRainSystem` script to any GameObject
2. In any script, call:
   ```csharp
   SimpleRainSystem.CreateRain(new Vector3(0, 15, 0), new Vector3(50, 1, 50), 150f);
   ```
   Where:
   - First param = rain center position (X, Y, Z)
   - Second param = area size (width, height, length)
   - Third param = particle emission rate

### **Option C: Hybrid Setup (Manual + Script Control)**

1. Create the particle system manually using Option A
2. Add the `SimpleRainSystem` script to control it via code
3. Adjust settings in real-time using the Inspector while playing

---

## Customization Tips

### **Make Rain Heavier/Lighter**
- **Heavier:** Increase Emission → Rate over Time (e.g., 250+)
- **Lighter:** Decrease Rate over Time (e.g., 50–100)

### **Make Rain Fall Faster/Slower**
- **Faster:** Increase Start Speed in Main module (e.g., 15+)
- **Slower:** Decrease Start Speed (e.g., 8–10)

### **Make Rain Cover Larger Area**
- **Shape module → Scale:** Increase X and Z (e.g., 50, 1, 50 for large playground)
- Adjust Y position higher if rain is visible above gameplay area

### **Add Rain Trails (Motion Blur Effect)**
- In Inspector, expand Trails module
- Enable Trails
- Adjust Trail Length and particles following

### **Change Particle Color**
- **Main → Start Color:** Click to set rain color (white, blue, gray, etc.)
- Or in Color over Lifetime module for gradient effect

### **Optimize Performance**
- Reduce Max Particles: Main → Max Particles (default 1000, reduce if needed)
- Use simpler shader: Particles/Standard Unlit instead of Standard Surface
- Limit emitting area size (smaller Shape scale)

---

## Testing Your Rain

1. **In Editor:** Play scene and look for rain falling from above playground
2. **Verify Movement:** Rain should fall straight down (mostly)
3. **Adjust Visuals:** Stop play, tweak settings in Inspector, play again to see changes
4. **Performance Check:** Open Profiler (Window → Analysis → Profiler) to monitor FPS

---

## Script Methods

### SimpleRainSystem Static Method
```csharp
// Create rain at position (0, 15, 0), covering area 50×50, emission 150 particles/sec
GameObject rainGO = SimpleRainSystem.CreateRain(
    new Vector3(0, 15, 0), 
    new Vector3(50, 1, 50), 
    150f
);

// You can then access and modify the particle system:
ParticleSystem ps = rainGO.GetComponent<ParticleSystem>();
```

### RainParticleSystemSetup Instance Method
```csharp
// In a MonoBehaviour script:
private RainParticleSystemSetup rainSetup;

void Start()
{
    rainSetup = gameObject.AddComponent<RainParticleSystemSetup>();
    rainSetup.SetupRain();
    
    // Later, destroy if needed:
    // rainSetup.DestroyRain();
}
```

---

## Common Issues & Fixes

| Issue | Fix |
|-------|-----|
| Rain not visible | Increase Emission rate; check particle lifetime isn't 0 |
| Rain only at bottom | Increase Shape scale Y or move position higher |
| Rain moving sideways | Check Velocity over Lifetime Y value (should be negative) |
| Rain too dense/sparse | Adjust Emission → Rate over Time |
| Particles disappear mid-fall | Increase Start Lifetime in Main module |
| Performance drops | Reduce emission rate or max particles; use simpler shader |

---

## Next Steps

1. Choose your setup method (manual or script-based)
2. Create the rain system in your SampleScene
3. Adjust parameters to match your game's visual style
4. Test and iterate
5. Commit changes to Git when satisfied

Happy raining! 🌧️
