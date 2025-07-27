using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    private float freezeDuration;

    public void Initialize(float areaDuration, float freezeTime)
    {
        freezeDuration = freezeTime;

        Debug.Log("Freeze alaný oluþtu, düþmanlar " + freezeDuration + " saniye donacak (simülasyon)");

        Destroy(gameObject, areaDuration);
    }


  /*  
     private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Bir düþman alana girdi ve dondu (" + freezeDuration + " saniye)"); 
        }
     } 
  */

}
