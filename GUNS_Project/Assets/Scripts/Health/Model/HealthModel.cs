using System;
using UnityEngine;

public class HealthModel
{
   public float Health;
   public float MaxHealth;

   public event Action<HealthModel> TakenDamage;
   public event Action<HealthModel> Death;

   public HealthModel(float health)
   {
      Health = health;
      MaxHealth = Health;
   }

   public void TakeDamage(float damage)
   {
      Health -= damage;
      TakenDamage?.Invoke(this);

      if (Health <= 0)
      {
         Death?.Invoke(this);
      }
   }
}
