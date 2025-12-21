using UnityEngine;

public abstract class ItemEffectSO : ScriptableObject
{
    public abstract void Apply(ref TearSpec spec);
}