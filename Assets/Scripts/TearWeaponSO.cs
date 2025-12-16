using UnityEngine;

[CreateAssetMenu(menuName = "Isaac/Tear Weapon")]
public class TearWeaponSO : ScriptableObject
{
    [Header("Projectile")]
    public BasicTear projectilePrefab;

    [Header("Stats")]
    public float fireInterval = 0.25f;
    public float speed = 12f;
    public float gravityScale = 1.2f;
    public float damage = 1f;
    public float lifeTime = 2f;

    [Header("Spawn")]
    public Vector2 spawnOffset = new Vector2(0, 0.1f);
}