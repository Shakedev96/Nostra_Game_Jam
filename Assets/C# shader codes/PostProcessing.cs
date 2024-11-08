using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessing : MonoBehaviour
{
    private Material m_Material;
    public Shader m_Shader;

    // Start is called before the first frame update
    void Start()
    {
        Material mat = new Material(m_Shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, m_Material);
    }
}
