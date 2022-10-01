using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private int levelIndex;
    public List<GameObject> levelList;
    public GameObject player;

    public void getNextLevel(int maxInt)
    {
        levelList.Add(levelList[levelIndex]);
        levelList.RemoveAt(levelIndex);
        levelIndex = Random.Range(0, maxInt - 1);
    }

    public void teleportPlayerToArea()
    {
        getNextLevel(levelList.Count);
        player.transform.position = levelList[levelIndex].transform.position;
    }
}
