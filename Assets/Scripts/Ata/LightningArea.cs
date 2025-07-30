using UnityEngine;

public class LightningArea : MonoBehaviour
{
    public float effectDuration = 0.5f;

        void Start()
    {
          Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);

           foreach (Collider2D hit in hits)
           {
               if (hit.CompareTag("Enemy"))
               {
                   Debug.Log(" Yýldýrým düþmaný yok etti: " + hit.name);
               }
           }

           Destroy(gameObject, effectDuration);
       
    }
}
