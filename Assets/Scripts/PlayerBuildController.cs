using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildController : MonoBehaviour
{
    [Header("Refs")]
    public TearShooter tearShooter;

    [Header("Base Weapon (SO)")]
    public TearWeaponSO baseWeapon;

    [Header("Owned Passives (stack)")]
    public List<PassiveItemData> ownedPassives = new List<PassiveItemData>();

    [Header("Runtime Result")]
    public TearSpec currentTear;

    void Awake()
    {
        if (tearShooter == null)
            tearShooter = GetComponentInChildren<TearShooter>();

        if (baseWeapon == null && tearShooter != null)
            baseWeapon = tearShooter.currentWeapon;

        Rebuild();
    }

    public void AddPassive(PassiveItemData item)
    {
        if (item == null) return;

        ownedPassives.Add(item);
        Rebuild();
    }

    public void Rebuild()
    {
        currentTear = TearSpec.FromWeapon(baseWeapon);

        //패시브 적용
        foreach (var passive in ownedPassives)
        {
            if (passive == null) continue;

            foreach (var effect in passive.effects)
            {
                if (effect == null) continue;
                effect.Apply(ref currentTear);
            }
        }
    }
}
