using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCollision : MonoBehaviour
{
    [HideInInspector]
    public static bool IsFlying = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") /*&& IsFirst*/)
        {
            //FindObjectOfType<FollowPlayer>().OffsetTrail = new Vector3(0, 1f, -0.5f);
            if (PlayerPrefs.GetInt("PlayerSkin") != 1)
            {
                FindObjectOfType<PlayerColor>().PartMat.color = new Color(FindObjectOfType<PlayerColor>().PartMat.color.r, FindObjectOfType<PlayerColor>().PartMat.color.g, FindObjectOfType<PlayerColor>().PartMat.color.b, 0.1f);
            }
            else
            {
                string name = FindObjectOfType<PlayerColor>().ListSkinofCube[SaveSystem.LoadIndexofSkinCube()].name;
                switch (name)
                {
                    case "PokerFace":
                        {
                            FindObjectOfType<PlayerColor>().PartMat.color = new Color(0, 0, 0, 0.1f);
                            break;
                        }
                }
            }
            IsFlying = true;
            Rigidbody rd = other.gameObject.GetComponent<Rigidbody>();
            rd.AddForce(0, 1.1f * Time.deltaTime, 0);
        }
    }
}
