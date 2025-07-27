using UnityEngine;

public class FireArea : MonoBehaviour
{
    private float duration;

    public void Initialize(float durationFromSkillBase)
    {
        duration = durationFromSkillBase;
        Destroy(gameObject, duration);
    }
}