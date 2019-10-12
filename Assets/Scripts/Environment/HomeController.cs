using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    [SerializeField]
    private GameObject roof;
    [SerializeField]
    private GameObject frontWall;
    [SerializeField]
    private GameObject backWall;
    

    public void PlayerEnter()
    {
        frontWall.SetActive(false);
        roof.SetActive(false);
    }

    public void PlayerExit()
    {
        frontWall.SetActive(true);
        roof.SetActive(true);
    }
}
