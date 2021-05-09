using UnityEngine;
public class QuitGame : MonoBehaviour
{
    public GameObject Panel;
    public void Quit()
    {
        Panel.SetActive(true);
    }
    public void OnYesClicked()
    {
        Application.Quit();
    }
    public void OnNoClicked()
    {
        Panel.SetActive(false);
    }
}
