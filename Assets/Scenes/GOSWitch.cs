using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOSWitch : MonoBehaviour

  



{
    public void OnPlay_Pressed()
    {
        SceneManager.LoadScene(0);
    }

    public void OnExit_Pressed()
    {
        Application.Quit();
    }


}
