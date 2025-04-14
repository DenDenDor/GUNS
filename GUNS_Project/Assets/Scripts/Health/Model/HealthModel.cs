using System;
using System.Collections;
using UnityEngine;

public class HealthModel
{
   private bool _isCooldown;
   
   public float Health;
   public float MaxHealth;

   public event Action<HealthModel> TakenDamage;
   public event Action<HealthModel> Healed;
   public event Action<HealthModel> Death;

   public HealthModel(float health)
   {
      Health = health;
      MaxHealth = Health;
   }

   public void TakeDamage(float damage)
   {
      if (_isCooldown)
      {
         return;
      }
      
      Health -= damage;
      TakenDamage?.Invoke(this);

      if (Health <= 0)
      {
         Death?.Invoke(this);
      }
   }

   public void ResetHealth()
   {
      Health = MaxHealth;
      Healed?.Invoke(this);

      CoroutineController.Instance.StartCoroutine(Wait());
   }

   private IEnumerator Wait()
   {
      _isCooldown = true;
      yield return new WaitForSeconds(0.5f);
      _isCooldown = false;
   }
}
