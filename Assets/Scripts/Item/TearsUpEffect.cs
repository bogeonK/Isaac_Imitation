using UnityEngine;

[CreateAssetMenu(menuName = "Isaac/Effects/Tears Up")]
public class TearsUpEffect : ItemEffectSO
{
    [Range(0.05f, 0.5f)]
    public float intervalReduction = 0.1f;

    public override void Apply(ref TearSpec spec)
    {
        spec.fireInterval = Mathf.Max(
            0.05f,
            spec.fireInterval - intervalReduction
        );
    }
}
