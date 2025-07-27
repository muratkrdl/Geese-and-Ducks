using UnityEngine;
using UnityEngine.EventSystems;

public class SkillInput : MonoBehaviour
{
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
