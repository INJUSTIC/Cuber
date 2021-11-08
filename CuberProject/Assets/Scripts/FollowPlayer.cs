using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    [HideInInspector]
    public Vector3 OffsetCamera;
    [HideInInspector]
    public Vector3 OffsetTrail;
    //private Vector3 CameraMove;
    private void Awake()
    {
        //CameraMove = new Vector3(0, 0, 0);
        OffsetTrail = new Vector3(0, -0.5f, -0.5f);
        OffsetCamera = new Vector3(0, 1.6f, -6f);
        if (transform.tag == "MainCamera") transform.position = Player.position + OffsetCamera;
        if (transform.tag == "TrackTag") transform.position = Player.position + OffsetTrail;
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "UnlimitedLevel" && transform.tag == "LineContainerTag")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Player.position.z);
        }
        if (transform.tag == "MainCamera" && !FindObjectOfType<SceneLoader>().BackGrPanel.activeSelf)
        {
            /*if (Input.GetKey(KeyCode.A))
            {
                CameraMove.x += 0.001f;
                transform.position = Player.position + OffsetCamera - CameraMove;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                CameraMove.x -= 0.001f;
                transform.position = Player.position + OffsetCamera + CameraMove;
            }*/
            /*if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.position.x > Screen.width / 2)
                {
                    transform.position = Player.position + OffsetCamera + CameraMove;
                }
                else
                {
                    transform.position = Player.position + OffsetCamera - CameraMove;
                }
            }*/
            /*else
            {
                if(CameraMove.x != 0)
                {
                    CameraMove.x = CameraMove.x > 0 ? CameraMove.x - 0.001f : CameraMove.x + 0.001f;

                }*/
                transform.position = Player.position + OffsetCamera/* + CameraMove*/;
            //}
        }
        if (transform.tag == "TrackTag")
        {
            if (FindObjectsOfType<SpringCollision>().Length > 0)
            {
                if (SpringCollision.IsFlying)
                {
                    transform.position = Player.position + new Vector3(0, -0.48f, -0.5f);
                }
                else
                {
                    transform.position = Player.position + OffsetTrail;
                }
            }
            else
            {
                transform.position = Player.position + OffsetTrail;
            }
        }
    }
}
