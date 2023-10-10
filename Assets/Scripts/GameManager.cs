using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex > 1)
        //{
        //    Destroy(GameObject.FindWithTag("GameManager"));
        //}
    }
}
