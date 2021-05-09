using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class AnimatingButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [HideInInspector]
    public bool IsButtonUp = false;
    public bool IsButtonExit = false;
    public Vector3 AnimatingButtonMinSize;
    public Vector3 AnimatingButtonMaxSize;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            if (!FindObjectOfType<Restart>().GetComponent<Animation>().IsPlaying("DeathAnim"))
            {
                GetComponent<Animator>().enabled = false;
                transform.localScale = AnimatingButtonMaxSize;
            }
        }
        else
        {
            GetComponent<Animator>().enabled = false;
            transform.localScale = AnimatingButtonMaxSize;
        }
        //gameObject.GetComponent<Animator>().StopPlayback();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            if (!FindObjectOfType<Restart>().GetComponent<Animation>().IsPlaying("DeathAnim"))
            {
                if (!IsButtonUp)
                {
                    IsButtonExit = true;
                    GetComponent<Animator>().enabled = true;
                    transform.localScale = AnimatingButtonMinSize;
                }
                else
                {
                    GetComponent<Animator>().enabled = false;
                }
            }
        }
        else
        {
            if (!IsButtonUp)
            {
                IsButtonExit = true;
                GetComponent<Animator>().enabled = true;
                transform.localScale = AnimatingButtonMinSize;
            }
            else
            {
                GetComponent<Animator>().enabled = false;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            if (!FindObjectOfType<Restart>().GetComponent<Animation>().IsPlaying("DeathAnim"))
            {
                if (!IsButtonExit)
                {
                    IsButtonUp = true;
                }
            }
        }
        else
        {
            if (!IsButtonExit)
            {
                IsButtonUp = true;
            }
        }
    }
}
