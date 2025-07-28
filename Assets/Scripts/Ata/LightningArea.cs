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
                   Debug.Log(" Y�ld�r�m d��man� yok etti: " + hit.name);
               }
           }

           Destroy(gameObject, effectDuration);
       
    }
}
