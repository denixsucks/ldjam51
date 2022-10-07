using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public Animator anim;
    public GameObject SceneTransition;

    [Header("ButonBox")]
    public GameObject boxOne;
    public GameObject boxTwo;
    public GameObject boxThree;

    [Header("MenuChoosing")]
    public int inputKeys = 0;
    public bool isActive;
    public bool isSelected1;
    public bool isSelected2;
    public bool isSelected3;

    [Header("Floats")]
    public float maxScaleX;
    public float minScaleX;
    public float maxScaleY;
    public float minScaleY;

    public void Update()
    {
        if (inputKeys == 1)
        {
            boxOne.transform.localScale = Vector2.Lerp(boxOne.transform.localScale, new Vector2(maxScaleX, maxScaleY), Time.deltaTime * 5);
            boxTwo.transform.localScale = Vector2.Lerp(boxTwo.transform.localScale, new Vector2(1, 1), Time.deltaTime * 20);
            //boxOne.transform.localScale = new Vector2(2, 1);
            //boxTwo.transform.localScale = new Vector2(1, 1);
            //boxOne.transform.localScale = new Vector2(Mathf.Abs(Mathf.Sin(Time.time) * 2), 1);
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                isSelected1 = true;
                Debug.Log("Buton " + inputKeys + " is Running");
                FirstScene();
                Debug.Log("ha");
            }
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                isSelected1 = false;
            }
        }
        if (inputKeys == 2)
        {
            boxOne.transform.localScale = Vector2.Lerp(boxOne.transform.localScale, new Vector2(1, 1), Time.deltaTime * 20);
            boxTwo.transform.localScale = Vector2.Lerp(boxTwo.transform.localScale, new Vector2(maxScaleX, maxScaleY), Time.deltaTime * 5);
            boxThree.transform.localScale = Vector2.Lerp(boxThree.transform.localScale, new Vector2(1, 1), Time.deltaTime * 20);
            //boxTwo.transform.localScale = new Vector2(2, 1);
            //boxOne.transform.localScale = new Vector2(1, 1);
            //boxThree.transform.localScale = new Vector2(1, 1);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                isSelected2 = true;
                Debug.Log("Buton " + inputKeys + " is Running");
                Credits();
            }
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                isSelected2 = false;
            }
        }

        if (inputKeys == 3)
        {
            boxTwo.transform.localScale = Vector2.Lerp(boxTwo.transform.localScale, new Vector2(1, 1), Time.deltaTime * 20);
            boxThree.transform.localScale = Vector2.Lerp(boxThree.transform.localScale, new Vector2(maxScaleX, maxScaleY), Time.deltaTime * 5);
            //boxThree.transform.localScale = new Vector2(2, 1);
            //boxTwo.transform.localScale = new Vector2(1, 1);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                isSelected3 = true;
                Debug.Log("Buton " + inputKeys + " is Running");
                QuitGame();
            }
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.KeypadEnter))
            {
                isSelected3 = false;
            }
        }

        if (isSelected1 == true)
        {
            boxOne.transform.localScale = Vector2.Lerp(boxOne.transform.localScale, new Vector2(minScaleX, minScaleY), Time.deltaTime * 50);
        }
        if (isSelected2 == true)
        {
            boxTwo.transform.localScale = Vector2.Lerp(boxTwo.transform.localScale, new Vector2(minScaleX, minScaleY), Time.deltaTime * 50);
        }
        if (isSelected3 == true)
        {
            boxThree.transform.localScale = Vector2.Lerp(boxThree.transform.localScale, new Vector2(minScaleX, minScaleY), Time.deltaTime * 50);
        }


        if (isActive == false)
        {
            inputKeys = Mathf.Clamp(inputKeys, 0, 3);
        }else
        inputKeys = Mathf.Clamp(inputKeys, 1, 3);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            isActive = true;
            inputKeys += 1;
            /*
            if (inputKeys == 4)
            {
                inputKeys = 3;
            }
            */
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            isActive = true;
            inputKeys -= 1;
            /*
            if (inputKeys == 0)
            {
                inputKeys = 1;
            }
            if (inputKeys == -1)
            {
                inputKeys = 0;
            }
            */
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
