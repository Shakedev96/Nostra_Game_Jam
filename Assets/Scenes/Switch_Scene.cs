using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Switch_Scene : MonoBehaviour
{
    public void OnPlay_Pressed()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExit_Pressed()
    {
        Application.Quit();
    }
}
