using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    const int INDEX_WORLD_SCENE = 0;
    const int INDEX_BATTLE_SCENE = 1;

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    LoadNextLevel();
        //}
    }

    public void LoadNextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if (buildIndex == 0)
        {
            buildIndex++;
        } else if (buildIndex == 1)
        {
            buildIndex--;
        }
        StartCoroutine(LoadLevel(buildIndex));
    }
    public void LoadLevel(string level)
    {
        int buildIndex = 0;
        switch (level.ToLower().Trim())
        {
            case "battle": buildIndex = INDEX_BATTLE_SCENE; break;
            case "world": buildIndex = INDEX_WORLD_SCENE; break;
        }
        StartCoroutine(LoadLevel(buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        
        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
