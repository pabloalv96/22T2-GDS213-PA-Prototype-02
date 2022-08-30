using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }



    public void LoadLevel1 ()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game Level");
    }

    public void Quit ()
    {
        Application.Quit();
    }

    public void MainMenu ()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver ()
    {
        StartCoroutine(WaitAndLoad("Game Over", sceneLoadDelay));
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

}
