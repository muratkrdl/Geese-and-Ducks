using UnityEngine;
using UnityEngine.EventSystems;

public class SkillInput : MonoBehaviour
{
    void Start()
    {
        if (SkillManager.instance == null)
        {
            Debug.LogError("SkillManager sahnede yok!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SkillManager.instance.UseSelectedSkill(worldPos);
        }
    }
}
