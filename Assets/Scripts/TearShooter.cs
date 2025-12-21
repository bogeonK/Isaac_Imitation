using UnityEngine;

public class TearShooter : MonoBehaviour
{
    public TearWeaponSO currentWeapon;
    public Transform muzzle;

    [Header("Build (optional)")]
    public PlayerBuildController build;   // Ãß°¡

    float nextFireTime;

    public bool TryFire(Vector2 dir)
    {
        TearSpec tear;

        if (build != null)
        {
            tear = build.currentTear;
        }
        else
        {
            if (currentWeapon == null) return false;
            tear = TearSpec.FromWeapon(currentWeapon);
        }

        if (tear.projectilePrefab == null) return false;
        if (Time.time < nextFireTime) return false;
        if (dir.sqrMagnitude < 0.01f) return false;

        nextFireTime = Time.time + tear.fireInterval;

        dir.Normalize();

        Vector3 origin = muzzle != null ? muzzle.position : transform.position;
        origin += (Vector3)tear.spawnOffset;

        var proj = Instantiate(tear.projectilePrefab, origin, Quaternion.identity);

        Vector2 velocity = dir * tear.speed;
        proj.Init(velocity, tear.gravityScale, tear.damage, tear.lifeTime);

        return true;
    }
}
