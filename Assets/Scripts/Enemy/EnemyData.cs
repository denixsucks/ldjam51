using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName ="Scriptible Object/Enemy Data")]
public class EnemyData : ScriptableObject 
{
  public int health;
  public int damageValue;
  public float speed;
}
