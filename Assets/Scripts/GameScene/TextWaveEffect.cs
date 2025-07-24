using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextWaveEffect : MonoBehaviour
{
    private TextMeshProUGUI tmpText;
    private Mesh mesh;
    private Vector3[] vertices;

    [Header("Wave Settings")]
    [Range(0f, 5f)] public float amplitude = 0.5f;    // Lower = less vertical movement
    [Range(0f, 10f)] public float frequency = 0.05f;  // Lower = stretched wave
    [Range(0f, 5f)] public float waveSpeed = 1f;      // Controls animation speed

    void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        tmpText.ForceMeshUpdate(); // Ensure the mesh is initialized
    }

    void Update()
    {
        tmpText.ForceMeshUpdate();
        mesh = tmpText.mesh;
        vertices = mesh.vertices;

        int characterCount = tmpText.textInfo.characterCount;

        for (int i = 0; i < characterCount; i++)
        {
            if (!tmpText.textInfo.characterInfo[i].isVisible)
                continue;

            int vertexIndex = tmpText.textInfo.characterInfo[i].vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                Vector3 orig = tmpText.textInfo.meshInfo[0].vertices[vertexIndex + j];
                float wave = Mathf.Sin(Time.time * waveSpeed + orig.x * frequency) * amplitude;
                vertices[vertexIndex + j] = orig + new Vector3(0, wave, 0);
            }
        }

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }
}
