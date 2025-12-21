using UnityEngine;

[CreateAssetMenu(menuName = "Isaac/Effects/Damage Up")]
public class DamageUpEffect : ItemEffectSO
{
    public float amount = 1f;

    public override void Apply(ref TearSpec spec)
    {
        spec.damage += amount;
    }
}