using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewGame : MonoBehaviour
{
    public GameObject Panel;
    public GameObject NextPanel;
    [HideInInspector]
    public void Load()
    {
        NextPanel.SetActive(true);
        FindObjectOfType<PauseManager>().transform.Find("Pause").gameObject.SetActive(false);
    }
    public void OnNextLevelClicked()
    {
        Camera.main.GetComponent<FastMobileBloom>().enabled = false;
        FinishGame.IsCompleted = false;
        Panel.SetActive(false);
        NextPanel.SetActive(false);
        FindObjectOfType<SceneLoader>().LoadNextScene();

    }
    public void OnMainMenuClicked()
    {
        Camera.main.GetComponent<FastMobileBloom>().enabled = false;
        FinishGame.IsCompleted = false;
        Panel.SetActive(false);
        NextPanel.SetActive(false);
        FindObjectOfType<SceneLoader>().LoadScene(0);
    }
}
