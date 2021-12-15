using SFB;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class ComputeScript : MonoBehaviour
{
    //This code is pretty bad and unoptimized, but it works and since this is an experiment im not fixing it
    public ComputeShader shader;
    public int width = 2048;
    public int height = 2048;
    public Texture startTexture;
    public RenderTexture texture;
    public bool overrideCamera = false;
    public float updateEvery = 0.5f;
    private float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
        float speedPercent = transform.position.y / 4.5f;
        updateEvery = Mathf.Lerp(0.03f,0.0005f,speedPercent);
        if (time > updateEvery)
        {
            RunShader();
            time = 0f;
        }
    }

    public void openImage()
    {
        // Open file with filter
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
        };
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, true)[0];
        if (path.Length != 0)
        {
            var fileContent = File.ReadAllBytes(path);
            Texture2D newtext = new Texture2D(4096, 4096);
            newtext.LoadImage(fileContent);
            Graphics.Blit(newtext, texture);
        }
    }

    private void Awake()
    {
        if(texture == null) { 
            texture = new RenderTexture(width, height, 24);
        }
        texture.enableRandomWrite = true;
        texture.filterMode = FilterMode.Point;
        texture.Create();
        shader.SetTexture(0, "Result", texture);
        shader.SetFloat("width", texture.width);
        shader.SetFloat("height", texture.height);
        Graphics.Blit(startTexture, texture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(overrideCamera) { 
            Graphics.Blit(texture, destination);
        } else
        {
            Graphics.Blit(source, destination);
        }
    }

    public void RunShader()
    {
        shader.Dispatch(0, texture.width / 8, texture.height / 8, 1);
    }
}
