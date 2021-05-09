using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    Touch touch = default;
    private float XRotation;
    private float YRotation;
    private float MaxXRotation;
    private float MaxYRotation;
    [SerializeField]
    private List<GameObject> ListofSkinCube = new List<GameObject>();
    public Camera cameraa;
    [HideInInspector]
    public static Material CubeMat;
    private const float ScreenResAttitude = 2.2558f;
    // private const int NormalScreenWidth = 
    private void Start()
    {
        float Multiplier = (float)Screen.width / Screen.height / ScreenResAttitude;
        transform.localScale = new Vector3(transform.localScale.x * Multiplier, transform.localScale.y * Multiplier, transform.localScale.z * Multiplier);
        /*
        float DivideWidthAndHeight = (float)Screen.width / Screen.height;
        if(DivideWidthAndHeight > 1.65 && DivideWidthAndHeight < 1.7)
        {
            transform.localScale = new Vector3(transform.localScale.x * 0.748f, transform.localScale.z * 0.748f, transform.localScale.z * 0.748f);
        }
        else if (DivideWidthAndHeight > 1.75 && DivideWidthAndHeight < 1.8)
        {
            transform.localScale = new Vector3(transform.localScale.x * 0.87f, transform.localScale.z * 0.87f, transform.localScale.z * 0.87f);
        }
        else if(Screen.width % Screen.height == 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * 0.9f, transform.localScale.z * 0.9f, transform.localScale.z * 0.9f);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * 0.97f, transform.localScale.z * 0.97f, transform.localScale.z * 0.97f);
        }
        //Camera.main.pro*/
    }
    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("PlayerSkin") != 1)
        {
           /* for (int i = 0; i < transform.childCount; ++i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }*/
            GetComponent<MeshRenderer>().enabled = true;
            CubeMat = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            ColorData colorData = SaveSystem.LoadColorofCube();
            ColorData colorDataEmission = SaveSystem.LoadEmissionColorofCube();
            CubeMat.color = new Color32(colorData.Color[0], colorData.Color[1], colorData.Color[2], 255);
            if (colorData.Color[0] == 178 && colorData.Color[1] == 12 && colorData.Color[2] == 0)
            {
                CubeMat.SetColor("_EmissionColor", new Color32(92, 0, 0, 255));
            }
            else if (colorData.Color[0] == 247 && colorData.Color[1] == 255 && colorData.Color[2] == 0)
            {
                CubeMat.SetColor("_EmissionColor", new Color32(199, 204, 0, 255));
            }
            else if (colorData.Color[0] == 255 && colorData.Color[1] == 59 && colorData.Color[2] == 93)
            {
                CubeMat.SetColor("_EmissionColor", new Color32(199, 0, 36, 255));
            }
            else if (colorData.Color[0] == 75 && colorData.Color[1] == 0 && colorData.Color[2] == 147)
            {
                CubeMat.SetColor("_EmissionColor", new Color32(0, 0, 166, 255));
            }
            else
            {
                CubeMat.SetColor("_EmissionColor", new Color32(colorDataEmission.Color[0], colorDataEmission.Color[1], colorDataEmission.Color[2], 255));
            }
        }
        else
        {
            CubeMat = gameObject.GetComponent<MeshRenderer>().sharedMaterial;
            GetComponent<MeshRenderer>().enabled = false;
            Instantiate(ListofSkinCube[SaveSystem.LoadIndexofSkinCube()], transform, false);
            for (int i = 0; i < transform.childCount - 1; ++i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            // PlayerModel.transform.localRotation = new Quaternion(90, 0, 0,90);
        }
        transform.localRotation = Quaternion.Euler(0, -76.681f, 0);
        touch.phase = TouchPhase.Ended;
    }
    private void Update()
    {
        transform.Rotate(0, 0, 0.2f);
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit) || touch.phase != TouchPhase.Ended /*&& raycastHit.collider == gameObject*/)
            {
                touch = Input.GetTouch(0);
                XRotation = touch.deltaPosition.x * 0.03f;
                YRotation = touch.deltaPosition.y * 0.03f;
                MaxXRotation = XRotation;
                MaxYRotation = YRotation;
                // UnityEngine.Debug.Log(XRotation);
                // UnityEngine.Debug.Log(YRotation);
                Vector3 right = Vector3.Cross(cameraa.transform.up, transform.position - cameraa.transform.position);
                Vector3 up = Vector3.Cross(transform.position - cameraa.transform.position, right);
                transform.rotation = Quaternion.AngleAxis(-XRotation * 2, up * 2) * transform.rotation;
                transform.rotation = Quaternion.AngleAxis(YRotation * 2, right * 2) * transform.rotation;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (XRotation > 0.03f || XRotation < -0.03f)
                {
                    // UnityEngine.Debug.Log(MaxXRotation);
                    XRotation -= XRotation / 100;
                    //  UnityEngine.Debug.Log(XRotation);
                }
                else
                {
                    XRotation = 0;
                }
                if (YRotation > 0.03f || YRotation < -0.03f)
                {
                    //UnityEngine.Debug.Log(MaxYRotation);
                    YRotation -= YRotation / 100;
                    //UnityEngine.Debug.Log(YRotation);
                }
                else
                {
                    YRotation = 0;
                }

                Vector3 right = Vector3.Cross(cameraa.transform.up, transform.position - cameraa.transform.position);
                Vector3 up = Vector3.Cross(transform.position - cameraa.transform.position, right);
                transform.rotation = Quaternion.AngleAxis(-XRotation, up) * transform.rotation;
                transform.rotation = Quaternion.AngleAxis(YRotation, right) * transform.rotation;
                /*else
                {
                    RotateAfterClickEnding.Stop();
                }*/
            }
        }
        else
        {
            if (touch.phase == TouchPhase.Ended)
            {
                if (XRotation > 0.03f || XRotation < -0.03f)
                {
                    // UnityEngine.Debug.Log(MaxXRotation);
                    XRotation -= XRotation / 100;
                    //  UnityEngine.Debug.Log(XRotation);
                }
                else
                {
                    XRotation = 0;
                }
                if (YRotation > 0.03f || YRotation < -0.03f)
                {
                    //UnityEngine.Debug.Log(MaxYRotation);
                    YRotation -= YRotation / 100;
                    //UnityEngine.Debug.Log(YRotation);
                }
                else
                {
                    YRotation = 0;
                }
                Vector3 right = Vector3.Cross(cameraa.transform.up, transform.position - cameraa.transform.position);
                Vector3 up = Vector3.Cross(transform.position - cameraa.transform.position, right);
                transform.rotation = Quaternion.AngleAxis(-XRotation, up) * transform.rotation;
                transform.rotation = Quaternion.AngleAxis(YRotation, right) * transform.rotation;
                /*else
                {
                    RotateAfterClickEnding.Stop();
                }*/
            }
        }
    }
    private void OnDisable()
    {
        XRotation = 0;
        YRotation = 0;
    }
}
