using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {

        //                              Taking the active scene - taking the build index adding a one, essentially going to the next in the list and loading it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // All of these examples loads level 1
        //SceneManager.LoadScene(1);

    }

    public void Options()
    {

        //options
        //SceneManager.LoadScene({the scene level for options});

    }

    public void QuitGame()
    {

        Debug.Log ("Exiting Application");
        Application.Quit();

    }
}
