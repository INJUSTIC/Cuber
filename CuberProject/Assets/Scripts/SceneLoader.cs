using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class SceneLoader : MonoBehaviour
{
    public GameObject LoadingPanel;
    public Slider slider;
    public GameObject Cube;

    private void Update()
    {
        if(Cube.activeSelf)
        {
            Cube.transform.Rotate(0, 0.8f, 0);
        }
    }
    public void LoadNextScene()
    {
        ScriptCollision.IsOvered = false;
        StartGame.AttemptNumber = 0;
        StartGame.IsFirstCalled = true;
        MovingObstacle.IsFirst = true;
        Time.timeScale = 0;
        LoadingPanel.SetActive(true);
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
        LoadingPanel.SetActive(true);
        Destroy(FindObjectOfType<AudioManager>().gameObject);
        StartCoroutine(LoadAsynchronously(IndexofLevel));
    }
    IEnumerator LoadAsynchronously(int IndexofLevel)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(IndexofLevel);
        LoadingPanel.SetActive(true);
        Cube.SetActive(true);
        Camera.main.transform.position = new Vector3(0, 150, 0);
        while (!operation.isDone)
        {
            float value = Mathf.Clamp01(operation.progress / .9f);
            slider.value = value*5;
            yield return null;
        }
        Time.timeScale = 1f;
    }

}
