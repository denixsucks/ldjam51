using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public Animator anim;
    public GameObject SceneTransition;

    [Header("MenuChoosing")]
    public int inputKeys = 0;


    public void Update()
    {
        if (inputKeys == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("ha");
        }
        if (inputKeys == 2 && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Credits();
        }
        if (inputKeys == 3 && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputKeys += 1;

            if (inputKeys == 4)
            {
                inputKeys = 3;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputKeys -= 1;

            if (inputKeys == 0)
            {
                inputKeys = 1;
            }
            if (inputKeys == -1)
            {
                inputKeys = 0;
            }
        }
    }
    public void FirstScene()
    {
        StartCoroutine("SceneAnimation");
    }


    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {

    }

    IEnumerator SceneAnimation()
    {
        SceneTransition.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
