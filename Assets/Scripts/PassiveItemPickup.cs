using UnityEngine;

public class PassiveItemPickup : MonoBehaviour
{
    public PassiveItemData item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var build = other.GetComponent<PlayerBuildController>();
        if (build == null) return;

        build.AddPassive(item);
        Destroy(gameObject);
    }
}
