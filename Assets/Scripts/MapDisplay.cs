using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapDisplay : MonoBehaviour
{
   public Renderer textureRender;

   public void Drawtexture(Texture2D texture)
   {
     

      textureRender.sharedMaterial.mainTexture = texture;
      textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
   }
}
