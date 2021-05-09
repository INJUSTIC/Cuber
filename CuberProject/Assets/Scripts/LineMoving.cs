using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineMoving : MonoBehaviour
{
    public static float StartSpeed = 3;
    public static float BoostEachLevel = 1;
    public static float Speed;
    // public static bool IsStoppedWhileGenerating = false;
    // public static bool IsStoppedWhileDestroying = false;

    private void Update()
    {
        if (!(StartGame.IsFirstCalled) && !ScriptCollision.IsOvered)
        {
            /*transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -1.9f, transform.position.z), lerpTime);
            if (Vector3.Distance(transform.position, new Vector3(transform.position.x, -1.9f, transform.position.z)) <= 0.5f)
            {
                transform.position = new Vector3(transform.position.x, 49f, transform.position.z);
            }*/

            //if (!IsStoppedWhileGenerating)
            //{
            //if(SceneManager.GetActiveScene().name != "UnlimitedLevel" || !IsStoppedWhileDestroying)
            //{      
            //Debug.Log(Speed);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -1.81f, transform.position.z), Speed * Time.deltaTime);
            if (Vector3.Distance(transform.position/*.y + 2.3f <= 0.5f*/, new Vector3(transform.position.x, -2.3f, transform.position.z)) <= 0.5f)
            {
                transform.position = new Vector3(transform.position.x, 49f, transform.position.z);
            }
            // }
            /* }              
             else if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
             {
                 List<GameObject> ActiveParts = FindObjectOfType<LevelGenerate>().ActiveParts;
                 Transform PreviousSideObst = ActiveParts[ActiveParts.Count - 2].transform.Find("SideObst");
                 Transform LevelPart = ActiveParts[ActiveParts.Count - 1].transform;
                 Transform SideObst1 = LevelPart.Find("SideObst");
                 Transform SideObst2 = LevelPart.Find("SideObst (1)");
                 for (int i = 0; i < SideObst1.childCount; ++i)
                 {
                     SideObst1.GetChild(i).position = new Vector3(SideObst1.GetChild(i).position.x, PreviousSideObst.GetChild(i).position.y, SideObst1.GetChild(i).position.z);
                     SideObst2.GetChild(i).position = new Vector3(SideObst2.GetChild(i).position.x, PreviousSideObst.GetChild(i).position.y, SideObst2.GetChild(i).position.z);
                 }
                 IsStoppedWhileGenerating = false;
             }*/
        }
    }
}
