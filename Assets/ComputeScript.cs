using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeScript : MonoBehaviour
{
    //This code is pretty bad and unoptimized, but it works and since this is an experiment im not fixing it
    public ComputeShader shader;
    public int width = 2048;
    public int height = 2048;
    public Texture startTexture;
    private RenderTexture texture;
    public float updateEvery = 0.5f;
    private float time = 0f;

    private void Update()
    {
        time += Time.deltaTime;
        if(time > updateEvery)
        {
            RunShader();
            time = 0f;
        }
    }

    private void Awake()
    {
        texture = new RenderTexture(width, height, 24);
        texture.enableRandomWrite = true;
        texture.Create();
        shader.SetTexture(0, "Result", texture);
        shader.SetFloat("width", texture.width);
        shader.SetFloat("height", texture.height);
        Graphics.Blit(startTexture, texture);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(texture, destination);
    }

    public void RunShader()
    {
        shader.Dispatch(0, texture.width / 8, texture.height / 8, 1);
    }
}
