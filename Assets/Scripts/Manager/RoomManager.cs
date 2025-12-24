using UnityEngine;
using System.Collections;

public class RoomManager : baseManager
{
    private RoomManagerConfig cfg;

    private Camera cam;
    private Transform player;
    private bool isTransitioning;

    public RoomManager(RoomManagerConfig cfg)
    {
        this.cfg = cfg;
    }

    public override void Init()
    {
        player = controller.playerTransform;
        cam = Camera.main;
    }

    public override void Update() { }
    public override void Destory() { }

    public void MoveTo(Room targetRoom, Transform optionalSpawn = null)
    {
        if (isTransitioning) return;
        if (targetRoom == null) return;

        isTransitioning = true;

        player.position = optionalSpawn != null ? optionalSpawn.position : targetRoom.transform.position;

        if (cam != null)
        {
            var p = cam.transform.position;
            p.x = targetRoom.transform.position.x;
            p.y = targetRoom.transform.position.y;
            cam.transform.position = p;
        }

        controller.StartCoroutine(EndTransition());
    }

    private IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(0.1f);
        isTransitioning = false;
    }
}
