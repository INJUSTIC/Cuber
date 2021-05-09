using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelGenerate : MonoBehaviour
{
    public GameObject[] Parts;
    [HideInInspector]
    public float SpawnZ = 0.0f;
    public Transform Player;
    [HideInInspector]
    public float LengthofPart = 164.2f;
    // private int PartsOnScreen = 2;
    [HideInInspector]
    public List<GameObject> ActiveParts;
    public GameObject FirstPart;
    //private int LastSpawningPart;
    private bool WasSpawned;
    [HideInInspector]
    public List<int> ListOfLastParts;
    public GameObject LoadingPanel;
    //private GameObject IsDestroying;
    //private float PlayerPositionChangeLinesPosition = 2500;
    //private float PositionIncreaser = 5000;
    //private bool LinesWasSpawned = false;
    //public GameObject[] Lines;
    private void Update()
    {
        /* if (!(FindObjectOfType<StartGame>().IsFirst))
         {
             if (FindObjectOfType<LevelGenerate>().ActiveParts[0].name == FindObjectOfType<LevelGenerate>().transform.GetChild(0).name)
             {
                 LineMoving.IsStoppedWhileDestroying = false;
             }
             else
             {
                 Debug.Log("WTF");
                 LineMoving.IsStoppedWhileDestroying = true;
             }
         }     */
        /* if(IsDestroying == null)
         {
             LineMoving.IsStoppedWhileDestroying = false;
         }*/
        if (Player.transform.position.z > (SpawnZ - /*PartsOnScreen * */LengthofPart * 1.5f) && FirstPart == null)
        {
            int SpawningPart = Randoming();
            SpawnPart(SpawningPart);
            //SpawnPart(31);
        }
        if (WasSpawned && Player.transform.position.z > SpawnZ - LengthofPart * 2 && FirstPart == null)
        {
            DeletePart();
        }
        if (FirstPart != null && !LoadingPanel.activeSelf)
        {
            Transform Ground = ActiveParts[0].transform.Find("GroundForUnLev");
            if (Player.position.z > (Ground.position.z + Ground.lossyScale.z / 2) + 4)
            {
                FirstPart = null;
                DeletePart();
            }
        }
        //if(Player.transform.position.z >= PlayerPositionChangeLinesPosition)
        //{
        //  PlayerPositionChangeLinesPosition *= 2;
        /*Transform PreviousSideObst = ActiveParts[ActiveParts.Count - 2].transform.Find("SideObst");
        Transform SpawnedPart = ActiveParts[ActiveParts.Count - 1].transform;
        Transform SideObst1 = SpawnedPart.Find("SideObst");
        Transform SideObst2 = SpawnedPart.Find("SideObst (1)");
        for (int i = 0; i < SideObst1.childCount; ++i)
        {
            SideObst1.GetChild(i).position = new Vector3(SideObst1.GetChild(i).position.x, PreviousSideObst.GetChild(i).position.y, SideObst1.GetChild(i).position.z);
            SideObst2.GetChild(i).position = new Vector3(SideObst2.GetChild(i).position.x, PreviousSideObst.GetChild(i).position.y, SideObst2.GetChild(i).position.z);
        }
        LinesWasSpawned = true;*/
        /* for(int i = 0; i < Lines.Length; ++i)
         {
             Lines[i].transform.position = new Vector3(Lines[i].transform.position.x, Lines[i].transform.position.y, Lines[i].transform.position.z * 2);
         }
     }    */
    }
    private void Start()
    {
        ListOfLastParts = new List<int>();
        ActiveParts = new List<GameObject>();
        GameObject LevelPart = Instantiate(FirstPart) as GameObject;
        SpawnZ += 53;
        LevelPart.transform.SetParent(transform);
        LevelPart.transform.position = new Vector3(-0.48f, 3.441f, 49.7f);
        ActiveParts.Add(LevelPart);
        // ActiveParts.Add(StartPart);
        for (int i = 0; i < 2; ++i)
        {
            //SpawnPart(0);
            SpawnPart(Randoming());
        }
    }
    [HideInInspector]
    public void SpawnPart(int IndexofPart = -1)
    {
        GameObject LevelPart = Instantiate(Parts[IndexofPart]) as GameObject;
        SpawnZ += LengthofPart;
        LevelPart.transform.SetParent(transform);
        LevelPart.transform.position = new Vector3(8.52f, 3.44f, SpawnZ);
        // LastSpawningPart = IndexofPart;
        ActiveParts.Add(LevelPart);
        ListOfLastParts.Add(IndexofPart);
        if (ListOfLastParts.Count > 3)
        {
            ListOfLastParts.RemoveAt(0);
        }
        WasSpawned = true;
        /* if(ActiveParts.Count > 3)
         {
             LineMoving.IsStoppedWhileGenerating = true;           
         }*/
    }
    [HideInInspector]
    public void DeletePart(int index = 0)
    {
        //IsDestroying = ActiveParts[index];
        //LineMoving.IsStoppedWhileDestroying = true;
        Destroy(ActiveParts[index]);
        ActiveParts.RemoveAt(0);
        WasSpawned = false;
    }
    [HideInInspector]
    public int Randoming()
    {
        int spawnpart = Random.Range(0, Parts.Length);
        while (ListOfLastParts.Contains(spawnpart))
        {
            spawnpart = Random.Range(0, Parts.Length);
        }
        return spawnpart;
        /* for(int i = 0; i < ListOfLastParts.Count; ++i)
         {
             if(spawnpart == ListOfLastParts[i])
             {
                 spawnpart = Random.Range(0, Parts.Length);
                 i = -1;
             }
         }*/
        /*while (spawnpart == LastSpawningPart)
        {
            spawnpart = Random.Range(0, Parts.Length);
        }*/
    }
}