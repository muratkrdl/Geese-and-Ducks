using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Enemy Properties")]
    public float maxHealth;
    public float speed;
    public float damage;
}
