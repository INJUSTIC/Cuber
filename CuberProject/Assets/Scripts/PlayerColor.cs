using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviour
{
    public static Material PlayerMat;
    public GameObject PartSyst;
    //public static Material PlayerModelMat;
    // public static Mesh PlayerModelMesh;
    public List<GameObject> ListSkinofCube = new List<GameObject>();
    public Material PartMat;
    public static bool IsPartSystActive;
    public List<Material> ListofPlayerMat = new List<Material>();
    private void Awake()
    {
        if (PlayerPrefs.GetInt("PlayerSkin") != 1)
        {
            PlayerMat = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            ColorData colorData = SaveSystem.LoadColorofCube();
            PlayerMat.color = new Color32(colorData.Color[0], colorData.Color[1], colorData.Color[2], 255);
            PartMat.color = PlayerMat.color;
            if (PlayerMat.color != Color.black)
            {
                PartMat.SetFloat("_Metallic", 0.7f);
            }
            else
            {
                PartMat.SetFloat("_Metallic", 0.6f);
            }
            /*if (PlayerMat.color == new Color(0.698f, 0.047f, 0))
            {
                PlayerMat.SetColor("_EmissionColor", new Color32(99, 0, 0, 255));
                PartMat.SetColor("_EmissionColor", new Color32(87, 0, 0, 255));
            }
            else if (PlayerMat.color == new Color(0.969f, 1, 0))
            {
                ColorData cd = SaveSystem.LoadColorofLand();
                Color32 Land_Color = new Color(cd.Color[0], cd.Color[1], cd.Color[2]);
                if ((Land_Color.r == 241 && Land_Color.g == 0 && Land_Color.b == 0) || (Land_Color.r == 0 && Land_Color.g == 255 && Land_Color.b == 96) || (Land_Color.r == 255 && Land_Color.g == 105) || (Land_Color.r == 255 && Land_Color.g == 255 && Land_Color.b == 255))
                {
                    PlayerMat.SetColor("_EmissionColor", new Color32(54, 55, 16, 255));
                    PartMat.SetColor("_EmissionColor", new Color32(50, 50, 18, 255));
                }
                else
                {
                    PlayerMat.SetColor("_EmissionColor", new Color32(54, 55, 16, 255));
                    PartMat.SetColor("_EmissionColor", new Color32(43, 43, 18, 255));
                }
            }
            else if (PlayerMat.color == new Color(0.294f, 0, 0.575f))
            {
                PlayerMat.SetColor("_EmissionColor", new Color32(0, 0, 126, 255));
                PartMat.SetColor("_EmissionColor", new Color32(0, 0, 97, 255));
            }
            else if (PlayerMat.color == new Color(0, 0.392f, 0))
            {
                PlayerMat.SetColor("_EmissionColor", new Color32(0, 132, 13, 255));
                PartMat.SetColor("_EmissionColor", new Color32(0, 129, 13, 255));
            }
            else if (PlayerMat.color == Color.black)
            {
                PlayerMat.SetColor("_EmissionColor", new Color(0, 0, 0));
                PartMat.SetColor("_EmissionColor", new Color32(5, 5, 5, 255));
            }
            else if (PlayerMat.color == new Color(1, 0.231f, 0.365f))
            {
                PlayerMat.SetColor("_EmissionColor", new Color32(63, 0, 12, 255));
                PartMat.SetColor("_EmissionColor", new Color32(53, 0, 14, 255));
            }
            else if (PlayerMat.color == Color.white)
            {
                PlayerMat.SetColor("_EmissionColor", new Color32(34, 34, 34, 255));
                PartMat.SetColor("_EmissionColor", new Color32(34, 34, 34, 255));
            }
            else if (PlayerMat.color == new Color(0, 0.749f, 1))
            {
                PlayerMat.SetColor("_EmissionColor", new Color32(0, 32, 255, 255));
                PartMat.color = new Color32(0, 220, 255, 255);
                PartMat.SetColor("_EmissionColor", new Color32(2, 0, 50, 255));
            }
                /* if(PartMat.color == Color.black)
                 {
                     PlayerMat.SetColor("_EmissionColor", new Color(0, 0, 0));
                     PartMat.SetColor("_EmissionColor", new Color32(5, 5, 5, 255));
                 }
                 else if(PartMat.color == new Color(0.7f, 0.048f, 0))
                 {
                     PlayerMat.SetColor("_EmissionColor", new Color32(0, 0, 126, 255));
                     PartMat.SetColor("_EmissionColor", new Color32(0, 0, 97, 255));
                 }
                 else if(PartMat.color == new Color(0.294f, 0, 0.575f))
                 {

                 }*/
            /*int i = 0;
            for (i = 0; i < ListofPlayerMat.Count && PlayerMat.color != ListofPlayerMat[i].color; ++i) { }
            switch (i)
            {    
                case 0:
                    {
                        PlayerMat.SetColor("_EmissionColor", new Color(0, 0, 0));
                        PartMat.SetColor("_EmissionColor", new Color32(5, 5, 5, 255));
                        break;
                    }
                case 1:
                    {
                        PlayerMat.SetColor("_EmissionColor", new Color32(0, 0, 126, 255));
                        PartMat.SetColor("_EmissionColor", new Color32(0, 0, 97, 255));
                        break;
                    }
                case 2:
                    {
                        PlayerMat.SetColor("_EmissionColor", new Color32(0, 132, 13, 255));
                        PartMat.SetColor("_EmissionColor", new Color32(0, 129, 13, 255));
                        break;
                    }
                case 3:
                    {
                        PlayerMat.SetColor("_EmissionColor", new Color32(63, 0, 12, 255));
                        PartMat.SetColor("_EmissionColor", new Color32(53, 0, 14, 255));
                        break;
                    }
                case 4:
                    {
                        PlayerMat.SetColor("_EmissionColor", new Color32(99, 0, 0, 255));
                        PartMat.SetColor("_EmissionColor", new Color32(87, 0, 0, 255));
                        break;
                    }
                case 5:
                    {
                        PlayerMat.SetColor("_EmissionColor", new Color32(34, 34, 34, 255));
                        PartMat.SetColor("_EmissionColor", new Color32(34, 34, 34, 255));
                        break;
                    }
                case 6:
                    {
                        ColorData cd = SaveSystem.LoadColorofLand();
                        Color32 Land_Color = new Color32(cd.Color[0], cd.Color[1], cd.Color[2],255);
                        if ((Land_Color.r == 241 && Land_Color.g == 0 && Land_Color.b == 0) || (Land_Color.r == 0 && Land_Color.g == 255 && Land_Color.b == 96) || (Land_Color.r == 255 && Land_Color.g == 105) || (Land_Color.r == 255 && Land_Color.g == 255 && Land_Color.b == 255))
                        {
                            Debug.Log("WTF");
                            PlayerMat.SetColor("_EmissionColor", new Color32(54, 55, 16, 255));
                            PartMat.SetColor("_EmissionColor", new Color32(50, 50, 18, 255));
                        }
                        else
                        {
                            PlayerMat.SetColor("_EmissionColor", new Color32(54, 55, 16, 255));
                            PartMat.SetColor("_EmissionColor", new Color32(43, 43, 18, 255));
                        }                       
                        break;
                    }
                case 7:
                    {
                        PlayerMat.SetColor("_EmissionColor", new Color32(0, 32, 255, 255));
                        PartMat.color = new Color32(0, 220, 255, 255);
                        PartMat.SetColor("_EmissionColor", new Color32(2, 0, 50, 255));
                        break;
                    }
                default:
                    {
                        Debug.Log("No such color");
                        break;
                    }
            }*/
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
            GameObject PlayerModel = Instantiate(ListSkinofCube[SaveSystem.LoadIndexofSkinCube()], transform, false);
            PlayerModel.GetComponent<BoxCollider>().enabled = false;  // без этого монета 
            PlayerModel.GetComponent<MeshCollider>().enabled = false; // собирается 2-3 раза
            string name = PlayerModel.name;
            switch (name)
            {
                case "PokerFace(Clone)":
                    {
                        PartMat.color = new Color(0, 0, 0);
                        PartMat.SetFloat("_Metallic", 0.6f);
                        break;
                    }
            }
        }
    }
}
