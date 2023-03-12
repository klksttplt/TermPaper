using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public static class Noise 
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,int seed,float scale, int octaves, float persistance, float lacunarity, UnityEngine.Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        
        var rnd = new System.Random(seed);
        var octaveOffsets = new UnityEngine.Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            var offsetX = rnd.Next(-10000, 10000) + offset.x;
            var offsetY = rnd.Next(-10000, 10000) + offset.y;
            octaveOffsets[i] = new UnityEngine.Vector2(offsetX, offsetY);
        }
        
        if (scale <= 0) scale = 0.0001f;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;
        
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    var sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    var sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseMap[x, y] = perlinValue;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight) maxNoiseHeight = noiseHeight;
                else if (noiseHeight < minNoiseHeight) minNoiseHeight = noiseHeight;
                
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
