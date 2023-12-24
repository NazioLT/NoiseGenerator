using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    [SerializeField] private NoiseSettings m_settings;

    [Header("Debug")]
    [SerializeField] private MeshRenderer m_meshRenderer = null;

    private const int RESOLUTION = 256;

    [ContextMenu("Generate")]
    private void Generate()
    {
        //Creating Noise
        Noise noise = m_settings.GetNoise();

        //Creating Texture
        Texture2D texture = new Texture2D(RESOLUTION, RESOLUTION);
        float[,] heightMap = noise.GetHeightMap(RESOLUTION);
        Color[] colour = new Color[RESOLUTION * RESOLUTION];
        for (var i = 0; i < colour.Length; i++)
        {
            int x = i % RESOLUTION;
            int y = i / RESOLUTION;

            colour[i] = new Color(heightMap[x, y], 0, 0, 1);
        }

        texture.SetPixels(colour);
        texture.Apply();

        m_meshRenderer.sharedMaterial.SetTexture("_MainTex", texture);
    }

    private void OnValidate()
    {
        Generate();
    }
}
