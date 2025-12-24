using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoorTrigger : MonoBehaviour
{
    public Room targetRoom;
    public Transform targetSpawnPoint;

    [Header("Anti Bounce")]
    public float reenterCooldown = 0.25f;

    private bool _locked;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_locked) return;
        if (!other.CompareTag("Player")) return;
        if (targetRoom == null) return;

        _locked = true;

        GameController.instance
            .GetManager<RoomManager>()
            .MoveTo(targetRoom, targetSpawnPoint);

        StartCoroutine(UnlockAfterDelay());
    }

    private IEnumerator UnlockAfterDelay()
    {
        yield return new WaitForSeconds(reenterCooldown);
        _locked = false;
    }
}
