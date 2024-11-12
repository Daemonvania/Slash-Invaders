using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   [SerializeField] private float fallSpeed = 10;

   private enum PowerUps
   {
      Strength,
      Speed,
      Invincibility
   }

   private PowerUps selectedPowerUp;

   private void Awake()
   {
      PowerUps selectedPowerUp = (PowerUps)UnityEngine.Random.Range(0, 3);
      switch (selectedPowerUp)
      {
         case PowerUps.Strength:
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            break;
         case PowerUps.Speed:
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            break;
         case PowerUps.Invincibility:
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            break;
      }
   }

   public virtual void Update()
   {
      transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
   }

   public void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.CompareTag("Player"))
      {
         Debug.Log(selectedPowerUp);
         Destroy(gameObject);
      }
   }
}
