using UnityEngine;

public class TearShooter : MonoBehaviour
{
    public TearWeaponSO currentWeapon;
    public Transform muzzle;

    float nextFireTime;

    public bool TryFire(Vector2 dir)
    {
        if (currentWeapon == null || currentWeapon.projectilePrefab == null) return false;
        if (Time.time < nextFireTime) return false;
        if (dir.sqrMagnitude < 0.01f) return false;

        nextFireTime = Time.time + currentWeapon.fireInterval;

        dir.Normalize();

        Vector3 origin = muzzle != null ? muzzle.position : transform.position;
        origin += (Vector3)currentWeapon.spawnOffset;

        var proj = Instantiate(currentWeapon.projectilePrefab, origin, Quaternion.identity);

        Vector2 velocity = dir * currentWeapon.speed;
        proj.Init(velocity, currentWeapon.gravityScale, currentWeapon.damage, currentWeapon.lifeTime);

        return true;
    }
}
