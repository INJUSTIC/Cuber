using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingBar;
   // public Slider slider;
    public GameObject Cube;
    public GameObject BackGrPanel;
    /*private void Update()
    {
        if(Cube.activeSelf)
        {
            Cube.transform.Rotate(0, 0.8f, 0);
        }
    }*/
    public void LoadNextScene()
    {
        ScriptCollision.IsOvered = false;
        StartGame.AttemptNumber = 0;
        StartGame.IsFirstCalled = true;
        MovingObstacle.IsFirst = true;
        Time.timeScale = 0;
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            LoadingBar.SetActive(true);
        }
        else
        {
            BackGrPanel.SetActive(true);
        }
        FindObjectOfType<AudioManager>().Stop(AudioManager.CurrentMusic);
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex+1));
    }
    public void LoadScene(int IndexofLevel)
    {
        ScriptCollision.IsOvered = false;
        MovingObstacle.IsFirst = true;
        StartGame.AttemptNumber = 0;
        StartGame.IsFirstCalled = true;
        Time.timeScale = 0;
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            LoadingBar.SetActive(true);
        }
        else
        {
            BackGrPanel.SetActive(true);
        }
        Destroy(FindObjectOfType<AudioManager>().gameObject);
        StartCoroutine(LoadAsynchronously(IndexofLevel));
    }
    IEnumerator LoadAsynchronously(int IndexofLevel)
    {        
        Animation CubeAnimation = Cube.GetComponent<Animation>();
        Animation LineMovingAnimation = LoadingBar.GetComponent<Animation>();
        AsyncOperation operation = SceneManager.LoadSceneAsync(IndexofLevel);
        LoadingBar.SetActive(true);
        //Cube.SetActive(true);
        CubeAnimation.Play();
        LineMovingAnimation.Play();
        //Camera.main.transform.position = new Vector3(0, 150, 0);
        while (!operation.isDone)
        {
            float value = Mathf.Clamp01(operation.progress);
            LineMovingAnimation["LoadingLinesAnim"].speed = 1;
            CubeAnimation["LoadingCubeAnim"].speed = 1;
            LineMovingAnimation["LoadingLinesAnim"].time = value/0.9f * LineMovingAnimation.clip.length;
            CubeAnimation["LoadingCubeAnim"].time = value / 0.9f * CubeAnimation.clip.length;
            //LoadingLineRed.fillAmount += CubeAnimation["LoadingCubeAnim"].time / CubeAnimation.clip.length;
            CubeAnimation["LoadingCubeAnim"].speed = 0;
            LineMovingAnimation["LoadingLinesAnim"].speed = 0;
            yield return null;
            // slider.value = value*5;
        }
        Time.timeScale = 1f;
    }

}
