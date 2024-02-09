using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
         if(other.CompareTag("Player"))
         {
            other.gameObject.SetActive(false);                          // if playe touches the death barrier we die            
         }
        else
        {
            Destroy(other.gameObject);                              // if enemies touch the death barreir they die
        }

    }
}
