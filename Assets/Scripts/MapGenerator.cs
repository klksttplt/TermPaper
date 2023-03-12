using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
   public int mapWidth;
   public int mapHeight;
   public float mapScale;

   public bool autoUpdate;

   public void GenerateMap()
   {
      float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, mapScale);

      MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
      mapDisplay.DrawNoiseMap(noiseMap);
   }
}
