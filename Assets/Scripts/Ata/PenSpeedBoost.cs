using System.Collections;
using UnityEngine;
using Murat.Data.UnityObject.CDS;

public class PenSpeedBoost : SkillBase
{
    [Header("Pen Speed Boost")]
    public float speedMultiplier = 3f;   
    public float buffDuration = 5f;     

    private CD_LINE cdLine;
    private float baseSpeed;            
    private Coroutine buffCR;

    private void OnEnable()
    {

        cdLine = Resources.Load<CD_LINE>("Data/CDS/CD_LINE");
        if (cdLine != null)
        {
            baseSpeed = cdLine.LineMovementData.MoveSpeed; 
            var data = cdLine.LineMovementData;
            data.MoveSpeed = baseSpeed;
            cdLine.LineMovementData = data;
        }
    }

    private void OnDisable()
    {
        if (cdLine != null)
        {
            var data = cdLine.LineMovementData;
            data.MoveSpeed = baseSpeed;
            cdLine.LineMovementData = data;
        }
    }

    public override void UseSkill(Vector2 targetPosition)
    {
        if (cdLine == null) return;

        if (buffCR != null) StopCoroutine(buffCR); 
        buffCR = StartCoroutine(BuffRoutine());
    }

    private IEnumerator BuffRoutine()
    {
        var data = cdLine.LineMovementData;
        data.MoveSpeed = baseSpeed * speedMultiplier;
        cdLine.LineMovementData = data;

        yield return new WaitForSeconds(buffDuration);

        data = cdLine.LineMovementData;
        data.MoveSpeed = baseSpeed;
        cdLine.LineMovementData = data;

        buffCR = null;
    }
}
