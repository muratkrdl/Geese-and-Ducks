using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    private float freezeDuration;

    public void Initialize(float areaDuration, float freezeTime)
    {
        freezeDuration = freezeTime;

        Debug.Log("Freeze alan� olu�tu, d��manlar " + freezeDuration + " saniye donacak (sim�lasyon)");

        Destroy(gameObject, areaDuration);
    }


  /*  
     private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Bir d��man alana girdi ve dondu (" + freezeDuration + " saniye)"); 
        }
     } 
  */

}
