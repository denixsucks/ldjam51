using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public static Upgrade Instance {get; private set;}
    const int startUpgradeSize = 1;
    private int currentUpgradeOrbCount;
    private int upgradeMilestone;
    private int upgradeLevel;

    [SerializeField] private Material upgradeBar;
    
    void Awake() {
      if (Instance != null && Instance != this) {
        Destroy(this);
      }
      else {
        Instance = this;
        Upgrade.Instance.init();
      } 
    }
    void init()
    {
      currentUpgradeOrbCount = startUpgradeSize;
      upgradeMilestone = 5;
      upgradeLevel = 0;
    }


    public void getUpgradeOrb(int getSize) 
    {
      currentUpgradeOrbCount += getSize;
      checkUpdate();
    }

    private void checkUpdate() 
    {
      if(currentUpgradeOrbCount == upgradeMilestone) {
        nextLevel();
      }
      else if(currentUpgradeOrbCount < 0) {
        previousLevel();
      }
      updateCanvas();
    }
    void nextLevel()
    {
      upgradeMilestone += 1;
      currentUpgradeOrbCount = 0;
      upgradeLevel += 1;
    }
    void previousLevel()
    {
      upgradeMilestone -= 1;
      currentUpgradeOrbCount = upgradeMilestone - 1;
      upgradeLevel -= 1;
    }

    void updateCanvas()
    {
      float value = (float) currentUpgradeOrbCount/upgradeMilestone;
      upgradeBar.SetFloat("_BarValue", Mathf.Clamp01(value));
    }

    private void Update() 
    {
      if(Input.GetKeyDown(KeyCode.A)) {
        getUpgradeOrb(-1);
      }

      if(Input.GetKeyDown(KeyCode.D)) {
        getUpgradeOrb(+1);
      } 
    }

    private void OnGUI() {
      GUI.Label(new Rect(5,5,300,600), $"{currentUpgradeOrbCount.ToString()}/{upgradeMilestone.ToString()}");
      GUI.Label(new Rect(5,20,300,600), $"{upgradeLevel.ToString()}");
    }
}
