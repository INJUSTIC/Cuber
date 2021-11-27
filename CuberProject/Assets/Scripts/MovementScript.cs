using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class MovementScript : MonoBehaviour
{
    private Rigidbody rd;
    private float StartForwardSpeed;
    private float StartSidewaySpeed;
    private float ForwardSpeed;
    private float SidewaySpeed;
    [HideInInspector]
    public static float boost; // for Unlimited Level to boost speed
    [HideInInspector]
    public float BoostEachLevel = 1f;
    //private static bool IsFirst = true;
    private int ScreenWidth = Screen.width;

    private void Awake()
    {
        MovingObstacle.Speed = 5;
        MovingObstacle.BoostEachLevel = 1;
        LineMoving.BoostEachLevel = 1;
        LineMoving.Speed = 3;
        boost = 1;

        if (SceneManager.GetActiveScene().name == "UnlimitedLevel")
        {
            ForwardSpeed = 17;
            SidewaySpeed = 35;
            StartForwardSpeed = ForwardSpeed;
            StartSidewaySpeed = SidewaySpeed;
        }
        else
        {
            ForwardSpeed = 28;
            SidewaySpeed = 55;
        }
        // UnityEngine.Debug.Log(ForwardSpeed);
        if (SceneManager.GetActiveScene().name != "UnlimitedLevel")
        {
            for (int i = 2; ; ++i, BoostEachLevel += 0.001f)
            {
                if (SceneManager.GetActiveScene().buildIndex == i)
                {
                    ForwardSpeed *= BoostEachLevel;
                    SidewaySpeed *= BoostEachLevel;
                    break;
                }
            }
        }
        rd = transform.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (!ScriptCollision.IsOvered)
        {
            // if(SceneManager.GetActiveScene().name == "UnlimitedLevel" || (!FindObjectOfType<FinishGame>().IsCameraCollidesFinishObst))
            // {
            if (Input.GetKey(KeyCode.A))
            {
                rd.AddForce(-SidewaySpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                //transform.position += new Vector3(-SidewaySpeed * Time.deltaTime, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rd.AddForce(SidewaySpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                //transform.position += new Vector3(SidewaySpeed * Time.deltaTime, 0, 0);
            }
            if(PlayerPrefs.GetInt("Accelerator") != 1)
            {
                rd.AddForce(SidewaySpeed * Input.acceleration.x * 4 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.position.x > ScreenWidth / 2)
                    {
                        rd.AddForce(SidewaySpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                    }
                    else
                    {
                        rd.AddForce(-SidewaySpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                    }
                }
            }
            transform.position += new Vector3(0, 0, ForwardSpeed * Time.deltaTime);
            // }          
        }
        else
        {
            rd.Sleep();
        }
    }
    public IEnumerator IncreaseSpeedUnLev()
    {
        while (Time.timeScale != 0 && !ScriptCollision.IsOvered)
        {
            boost += (float)0.017;
            MovingObstacle.Speed = MovingObstacle.StartSpeed * boost;
            LineMoving.Speed = LineMoving.StartSpeed * boost;
            ForwardSpeed = StartForwardSpeed * boost;
            SidewaySpeed = StartSidewaySpeed * boost;
            yield return new WaitForSeconds(5);
        }
    }
}