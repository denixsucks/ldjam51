using UnityEngine;

public interface IDamagable
{
  int health {get; set;}
  void damage(int damageValue);
}