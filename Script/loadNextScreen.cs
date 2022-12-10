using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNextScreen : MonoBehaviour
{
    void Start()
    {
        Invoke("nextScreen", 3f);
        
    }

    private void nextScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
