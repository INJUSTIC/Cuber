using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class LandColor : MonoBehaviour
{
    public Material ObstMat;
    private Color32 Land_Color;
    private void Awake()
    {
        /*if(gameObject.tag != "ground")
        {
           // GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("Standard");
        }      */
        ColorData colorData = SaveSystem.LoadColorofLand();
        /*  else if (gameObject.tag == "TopObstacleTag" || gameObject.tag == "BoundaryObstacleTag")
          {
              gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color32(7, 7, 7, 255);
              gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));

          }
          if (gameObject.tag == "ground")
          {
              gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(22, 54, 219);

          }
          else if (gameObject.tag == "TopObstacleTag" || gameObject.tag == "BoundaryObstacleTag")
          {
              gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color32(7, 7, 7, 255);
              gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));

          }*/
        Land_Color = new Color32(colorData.Color[0], colorData.Color[1], colorData.Color[2], 255);
        if (Land_Color == Color.black)
        {
            if (gameObject.CompareTag("ground"))
            {
                gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color(0, 0, 0);

            }
            else if (gameObject.CompareTag("TopObstacleTag") || gameObject.CompareTag("BoundaryObstacleTag"))
            {
                gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color32(7, 7, 7, 255);
                // gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", new Color(0, 0, 0));
            }
            ObstMat.shader = Shader.Find("Standard");
            ObstMat.SetFloat("_Glossiness", .2f);
            ObstMat.color = new Color32(219, 219, 219, 255);
            ObstMat.SetColor("_EmissionColor", new Color(0, 0, 0));
            // ObstMat.SetColor("_EmissionColor", new Color((float)27 / 255, (float)27 / 255, (float)27 / 255));
            //ObstMat.SetFloat("_Glossiness", 0.2f);
        }
        else if (Land_Color == Color.white)
        {
            //Camera.main.GetComponent<FastMobileBloom>().enabled = false;
            ObstMat.color = new Color(0, 0, 0);
            if (gameObject.CompareTag("ground"))
            {
                gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color32(241, 241, 241, 255);
            }
            else if (gameObject.CompareTag("TopObstacleTag") || gameObject.CompareTag("BoundaryObstacleTag"))
            {
                // gameObject.GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("Unlit/Color");
                gameObject.GetComponent<MeshRenderer>().sharedMaterial.color = new Color32(234, 234, 234, 255);
                // gameObject.GetComponent<MeshRenderer>().sharedMaterial.SetColor("_EmissionColor", new Color32(106, 106, 106, 255));
            }
        }
        else
        {
            //ObstMat.shader = Shader.Find("Unlit/Color");
            /*  if ((Land_Color.r == 241 && Land_Color.g == 0 && Land_Color.b == 0) || (Land_Color.r == 0 && Land_Color.g == 255 && Land_Color.b == 96) || (Land_Color.r == 255 && Land_Color.g == 105))
              {
                  Camera.main.GetComponent<FastMobileBloom>().enabled = false;
                  ObstMat.color = new Color(1,1,1);
              }*/
            //  else
            // {
            ObstMat.color = new Color32(219, 219, 219, 255);
            //}
            Material mat = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            int r, g, b;
            if (gameObject.CompareTag("ground"))
            {
                r = colorData.Color[0] - 25 >= 0 ? colorData.Color[0] - 14 : 0;
                g = colorData.Color[1] - 25 >= 0 ? colorData.Color[1] - 14 : 0;
                b = colorData.Color[2] - 25 >= 0 ? colorData.Color[2] - 14 : 0;
            }
            else
            {
                if (colorData.Color[0] != 36 && colorData.Color[1] != 68 && colorData.Color[2] != 233)
                {
                    //mat.shader = Shader.Find("Unlit/Color");
                }
                r = colorData.Color[0] - 25 >= 0 ? colorData.Color[0] - 25 : 0;
                g = colorData.Color[1] - 25 >= 0 ? colorData.Color[1] - 25 : 0;
                b = colorData.Color[2] - 25 >= 0 ? colorData.Color[2] - 25 : 0;
            }
            mat.color = new Color32((byte)r, (byte)g, (byte)b, 255);
            /* if (Land_Color.r == 36 && Land_Color.g == 68 && Land_Color.b == 233)
             {
                 mat.SetColor("_EmissionColor", new Color32(0, 15,90,255));
             }
             else if(Land_Color.r == 241 && Land_Color.g == 0 && Land_Color.b == 0)
             {
                 mat.SetColor("_EmissionColor", new Color32(111, 0, 0, 255));
             }
             else if(Land_Color.r == 0 && Land_Color.g == 255 && Land_Color.b == 96)
             {
                 mat.SetColor("_EmissionColor", new Color32(0, 118, 37, 255));
             }
             else if(Land_Color.r == 255 && Land_Color.g == 105)
             {
                 mat.SetColor("_EmissionColor", new Color32(111, 44, 77, 255));
             }
             else if(Land_Color.r == 111 && Land_Color.g == 111 && Land_Color.b == 111)
             {
                 mat.SetColor("_EmissionColor", new Color32(58, 58, 58, 255));
             }*/
            //ObstMat.SetFloat("_Glossiness", 0.2f);
            //ObstMat.SetColor("_EmissionColor", new Color((float)38 / 255, (float)38 / 255, (float)38 / 255));
        }
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel" && gameObject.CompareTag("BoundaryObstacleTag"))
        {
            int r, g, b;
            r = colorData.Color[0] - 35 >= 0 ? colorData.Color[0] - 35 : 0;
            g = colorData.Color[1] - 35 >= 0 ? colorData.Color[1] - 35 : 0;
            b = colorData.Color[2] - 35 >= 0 ? colorData.Color[2] - 35 : 0;
            RenderSettings.fogColor = new Color32((byte)r, (byte)g, (byte)b, 255);
            Camera.main.backgroundColor = RenderSettings.fogColor;
        }
    }
}
