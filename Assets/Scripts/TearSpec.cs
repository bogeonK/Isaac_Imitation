using UnityEngine;

[System.Serializable]
public struct TearSpec
{
    public BasicTear projectilePrefab;

    public float fireInterval;
    public float speed;
    public float gravityScale;
    public float damage;
    public float lifeTime;

    public Vector2 spawnOffset;

    public static TearSpec FromWeapon(TearWeaponSO weapon)
    {
        TearSpec spec = new TearSpec();

        if (weapon == null)
            return spec;

        spec.projectilePrefab = weapon.projectilePrefab;
        spec.fireInterval = weapon.fireInterval;
        spec.speed = weapon.speed;
        spec.gravityScale = weapon.gravityScale;
        spec.damage = weapon.damage;
        spec.lifeTime = weapon.lifeTime;
        spec.spawnOffset = weapon.spawnOffset;

        return spec;
    }
}
