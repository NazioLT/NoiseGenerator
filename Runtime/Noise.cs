using UnityEngine;
using Random = System.Random;

public abstract class Noise
{
    public Noise(NoiseSettings settings)
    {
        m_settings = settings;
    }

    protected NoiseSettings m_settings;

    public float[,] GetHeightMap(int resolution)
    {
        return m_settings.UseOctaves ? OctaveBasedHeightMap(resolution) : BasicHeightMap(resolution);
    }

    private float[,] BasicHeightMap(int resolution)
    {
        float[,] heights = new float[resolution, resolution];
        
        for (var x = 0; x < resolution; x++)
        {
            for (var y = 0; y < resolution; y++)
            {
                Vector2 uv = new Vector2(x, y) / resolution;
                heights[x, y] = Sample(uv);
            }
        }

        return heights;
    }

    private float[,] OctaveBasedHeightMap(int resolution)
    {
        float[,] heights = new float[resolution, resolution];
        Random rng = m_settings.GetRNG();
        float scale = m_settings.Scale;

        Vector2[] octaveOffsets = new Vector2[m_settings.Octaves];
        for (int i = 0; i < m_settings.Octaves; i++)
        {
            float offsetX = rng.Next(-100000, 100000) + m_settings.Offset.x;
            float offsetY = rng.Next(-100000, 100000) + m_settings.Offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float half = resolution / 2f;

        for (var x = 0; x < resolution; x++)
        {
            for (var y = 0; y < resolution; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < m_settings.Octaves; i++)
                {
                    Vector2 uv = new Vector2(
                        (x - half) / scale * frequency + octaveOffsets[i].x,
                        (y - half) / scale * frequency + octaveOffsets[i].y);

                    float perlinValue = Sample(uv) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= m_settings.Persistance;
                    frequency *= m_settings.Lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                heights[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                heights[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heights[x, y]);
            }
        }

        return heights;
    }

    protected abstract float Sample(Vector2 uv);
}