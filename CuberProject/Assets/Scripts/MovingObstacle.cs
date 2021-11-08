using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingObstacle : MonoBehaviour
{
    public static float StartSpeed = 5;
    public static float Speed;
    public static float BoostEachLevel;
    public GameObject[] Targets;
    private int current = 0;
    private float Radius = 1f;
    public static bool IsFirst = true;

    private void Start()
    {
        if (IsFirst && SceneManager.GetActiveScene().name != "UnlimitedLevel")
        {
            IsFirst = false;
            for (int i = 2; ; ++i, BoostEachLevel += 0.01f)
            {
                if (SceneManager.GetActiveScene().buildIndex == i)
                {
                    Speed *= BoostEachLevel;
                    break;
                }
            }
        }
    }

    private void Update()
    {
        Vector3 offset = transform.position - Targets[current].transform.position;
        if (offset.sqrMagnitude < Radius*Radius)
        {
            ++current;
            if (current == Targets.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, Targets[current].transform.position, Speed * Time.deltaTime);
    }
}