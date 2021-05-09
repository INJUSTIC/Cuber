using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadAfterClicking(int IndexofLevel = -1)
    {
        MovingObstacle.IsFirst = true;
        if (IndexofLevel == -1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(IndexofLevel);
        }
    }
    public static void Load()
    {
        MovingObstacle.IsFirst = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public static void Load(string name)
    {
        MovingObstacle.IsFirst = true;
        if (name == "same")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            SceneManager.LoadScene(name);
        }
    }
    public static void Load(int IndexofLevel)
    {
        SceneManager.LoadScene(IndexofLevel);
    }
}
