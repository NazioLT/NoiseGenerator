using UnityEngine;

public sealed class PerlinNoise : Noise
{
    public PerlinNoise(NoiseSettings settings) : base(settings)
    {
    }

    protected override float Sample(Vector2 uv)
    {
        float height = Mathf.PerlinNoise(uv.x, uv.y);
        return height;
    }
}
