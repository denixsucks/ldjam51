using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public int levelIndex;
    public int levelSize = 4;
    public int otherLevelSize = 4;
    //oylesine yazdim, sonradan liste ile her levele sey belirleriz zaten :P


    public List<GameObject> levelList;

    public GameObject player;

    void Update()
    {
        if (levelSize <= 0)
        {
            levelSize = otherLevelSize;
            Debug.Log("stage is completed");
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            PickRandomNumber(levelSize);
            levelSize = levelSize - 1;
            player.transform.position = levelList[levelIndex].transform.position;

            if (levelSize <= 0)
            {
                levelSize = otherLevelSize;
                Debug.Log("stage is completed");
            }
           
        }
        /*
        if (Input.GetKeyDown(KeyCode.O))
        {
            //levelIndex = levelIndex - 1;
            player.transform.position = levelList[levelIndex].transform.position;
        }
        */

        
    }

    public void PickRandomNumber(int maxInt)
    {
        levelIndex = Random.Range(1, maxInt + 1);
    }

    public void levelChangeSystem()
    {
        PickRandomNumber(levelSize);
        levelSize = levelSize - 1;
        player.transform.position = levelList[levelIndex].transform.position;
    }
}
