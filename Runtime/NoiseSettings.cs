using UnityEngine;

using Random = System.Random;

[System.Serializable]
public struct NoiseSettings
{
    private enum NoiseType { Perlin, Voronoi }

    [SerializeField] private NoiseType m_type;
    [SerializeField] private int m_cellCount;
    [SerializeField] private float m_scale;
    [SerializeField] private int m_seed;
    [SerializeField] private int m_octaves;
    [SerializeField] private float m_lacunarity;
    [SerializeField] private float m_persistance;
    [SerializeField] private Vector2 m_offset;

    private bool m_useOctaves;

    public float Scale => m_scale;
    public float Persistance => m_persistance;
    public float Lacunarity => m_lacunarity;
    public Vector2 Offset => m_offset;
    public int Octaves => m_octaves;
    public int CellCount => m_cellCount;
    public bool UseOctaves => m_useOctaves;
    
    public Noise GetNoise()
    {
        m_useOctaves = m_type == NoiseType.Perlin;
        
        switch (m_type)
        {
            case NoiseType.Perlin:
                return new PerlinNoise(this);
            case NoiseType.Voronoi:
                return new VoronoiNoise(this);
            default:
                break;
        }
        
        throw new System.Exception("Noise Type is'nt defined");
    }

    public Random GetRNG()
    {
        return new Random(m_seed);
    }
}
