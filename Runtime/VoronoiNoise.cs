using UnityEngine;

using Random = System.Random;

public sealed class VoronoiNoise : Noise
{
    public VoronoiNoise(NoiseSettings settings) : base(settings)
    {
        Random rng = settings.GetRNG();

        positions = new Vector2[m_settings.CellCount];
        for (int i = 0; i < m_settings.CellCount; i++)
        {
            positions[i] = new Vector2(rng.Next(0, 1000), rng.Next(0, 1000)) / 1000f;
        }
    }

    private Vector2[] positions;

    protected override float Sample(Vector2 uv)
    {
        float minDistance = float.MaxValue;

        foreach (var pos in positions)
        {
            float dst = Vector2.Distance(pos, uv);

            minDistance = Mathf.Min(dst, minDistance);
        }

        return minDistance;
    }
}