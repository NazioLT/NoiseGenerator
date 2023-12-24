using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    [SerializeField] private float m_scale = 2f;

    [Header("Debug")]
    [SerializeField] private MeshRenderer m_meshRenderer = null;

    private const int RESOLUTION = 256;

    [ContextMenu("Generate")]
    private void Generate()
    {
        Texture2D texture = new Texture2D(RESOLUTION, RESOLUTION);
        Color[] colour = new Color[RESOLUTION * RESOLUTION];
        for (var i = 0; i < colour.Length; i++)
        {
            int x = i % RESOLUTION;
            int y = i / RESOLUTION;
            Vector2 uv = new Vector2(x, y) / RESOLUTION;

            colour[i] = SampleNoise(uv);
        }

        texture.SetPixels(colour);
        texture.Apply();

        m_meshRenderer.sharedMaterial.SetTexture("_MainTex", texture);
    }

    private Color SampleNoise(Vector2 uv)
    {
        uv *= m_scale;
        float height = Mathf.PerlinNoise(uv.x, uv.y);
        return new Color(height, 0, 0f, 1f);
    }

    private void OnValidate()
    {
        Generate();
    }
}
