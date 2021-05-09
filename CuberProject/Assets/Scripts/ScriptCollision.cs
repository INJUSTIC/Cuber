//#define UnityEditor
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using System;
using TMPro;
#if UnityEditor
using UnityEditor;
#endif

public class ScriptCollision : MonoBehaviour
{

    //private Stopwatch wait = new Stopwatch();
    public GameObject ContainerPartsofPlayer;
    public List<GameObject> PartsofPlayer = new List<GameObject>();
    public TextMeshProUGUI EarnedCoins;
    public static bool IsOvered = false;
    [ExecuteAlways]
    private void Start()
    {
        if (PlayerPrefs.GetInt("PlayerSkin") == 1)
        {
            switch (transform.GetChild(0).name)
            {
                case "PokerFace(Clone)":
                    {
                        GameObject PartsofPokerFace = Instantiate(Resources.Load("PartsofPlayerPokerFace", typeof(GameObject)) as GameObject);
                        ContainerPartsofPlayer = PartsofPokerFace;
                        break;
                    }
            }
            FindObjectOfType<Restart>().ContainerPartsofPlayer = ContainerPartsofPlayer;
        }
        else
        {
            foreach (GameObject Part in PartsofPlayer)
            {
                Part.GetComponent<MeshRenderer>().sharedMaterial.color = PlayerColor.PlayerMat.color;
            }
        }
    }
    /*async private void PlayerRotationDefalut()
    {       
        await Task.Run(() => {
            while (!SpringCollision.IsFirst && !wait.IsRunning)
            {
                UnityEngine.Debug.Log("WTF");
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        });
        //yield return null;
    }*/
    private void Update()
    {
        // PlayerRotationDefalut();
        if (SpringCollision.IsFlying/* && !wait.IsRunning*/)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            // if(FindObjectsOfType<SpringCollision>().Length > 0)
            // {
            StartCoroutine(ResetPlayerRotation());
            if (SpringCollision.IsFlying)
            {
                StartCoroutine(AfterLanding());
            }
            //  }           
        }
        if (!FinishGame.IsCompleted && !collision.collider.CompareTag("ground") && !IsOvered)
        {
            IsOvered = true;
            PlayerColor.IsPartSystActive = false;
            if (SpringCollision.IsFlying)
            {
                StartCoroutine(AfterLanding());
            }
            GetComponent<PlayerColor>().PartSyst.SetActive(false);

            ContainerPartsofPlayer.transform.position = transform.position;
            if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
            {
                EarnedCoins.text = CoinCollision.Counter.ToString();
                FindObjectOfType<StartGame>().IsFirst = true;
            }
            FindObjectOfType<AudioManager>().Stop(AudioManager.CurrentMusic);
            gameObject.SetActive(false);
            ContainerPartsofPlayer.SetActive(true);
            FindObjectOfType<Restart>().RestartGame();
        }
    }
    /*private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "ground" && FindObjectsOfType<SpringCollision>().Length > 0 && !SpringCollision.IsFirst)
        {
            SpringCollision.IsFirst = true;
            GetComponent<PlayerColor>().PartMat.color = gameObject.GetComponent<MeshRenderer>().sharedMaterial.color;
            GetComponent<PlayerColor>().PartMat.SetFloat("Glossiness", .5f);
        }
    }*/
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("ground") && !SpringCollision.IsFlying && transform.position.y > 0 && (transform.position.y - collision.transform.position.y != 1))
        {
            if (PlayerPrefs.GetInt("PlayerSkin") != 1)
            {
                FindObjectOfType<PlayerColor>().PartMat.color = new Color(FindObjectOfType<PlayerColor>().PartMat.color.r, FindObjectOfType<PlayerColor>().PartMat.color.g, FindObjectOfType<PlayerColor>().PartMat.color.b, 0.1f);
            }
            else
            {
                string name = FindObjectOfType<PlayerColor>().ListSkinofCube[SaveSystem.LoadIndexofSkinCube()].name;
                switch (name)
                {
                    case "PokerFace":
                        {
                            FindObjectOfType<PlayerColor>().PartMat.color = new Color(0, 0, 0, 0.1f);
                            break;
                        }
                }
            }
            SpringCollision.IsFlying = true;
            //Rigidbody rd = gameObject.GetComponent<Rigidbody>();
            //rd.AddForce(0, 0.001f * Time.deltaTime, 0);
        }
    }
    private IEnumerator ResetPlayerRotation()
    {
        for (int i = 0; i < 10; ++i)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            yield return new WaitForSeconds(.1f);
        }
    }
    public IEnumerator AfterLanding()
    {
        yield return new WaitForSeconds(.1f);
        if (PlayerPrefs.GetInt("PlayerSkin") != 1)
        {
            GetComponent<PlayerColor>().PartMat.color = gameObject.GetComponent<MeshRenderer>().sharedMaterial.color;
        }
        else
        {
            string name = GetComponent<PlayerColor>().ListSkinofCube[SaveSystem.LoadIndexofSkinCube()].name;
            switch (name)
            {
                case "PokerFace":
                    {
                        GetComponent<PlayerColor>().PartMat.color = new Color(0, 0, 0, 1f);
                        break;
                    }
            }
        }
        GetComponent<PlayerColor>().PartMat.SetFloat("Glossiness", .5f);
        SpringCollision.IsFlying = false;
    }
}
