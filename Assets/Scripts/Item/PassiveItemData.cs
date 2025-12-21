using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Isaac/Items/Passive Item")]
public class PassiveItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;


    [TextArea]
    public string description;

    [Header("Effects")]
    public List<ItemEffectSO> effects = new List<ItemEffectSO>();
}
