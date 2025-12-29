using UnityEngine;
using System.Collections;
using Unity.Cinemachine;

public class RoomManager : baseManager
{
    private RoomManagerConfig cfg;
    private Transform player;
    private bool isTransitioning;
    private Transform cineCamTr;

    private Coroutine camMoveCo;

    private float camMoveTime = 0.25f;

    public RoomManager(RoomManagerConfig cfg)
    {
        this.cfg = cfg;
    }

    public override void Init()
    {
        player = controller.playerTransform;

        var cineCam = Object.FindFirstObjectByType<CinemachineCamera>();
        cineCamTr = cineCam != null ? cineCam.transform : null;

        if (cineCamTr == null)
            Debug.LogError("[RoomManager] CinemachineCamera를 못 찾았음!");
    }

    public override void Update() { }
    public override void Destory() { }

    public void MoveTo(Room targetRoom, Transform optionalSpawn = null)
    {
        if (isTransitioning) return;
        if (targetRoom == null) return;

        isTransitioning = true;

        player.position = optionalSpawn != null ? optionalSpawn.position : targetRoom.transform.position;

        if (cineCamTr != null)
        {
            Vector3 targetCamPos = cineCamTr.position;
            targetCamPos.x = targetRoom.transform.position.x;
            targetCamPos.y = targetRoom.transform.position.y;

            if (camMoveCo != null) controller.StopCoroutine(camMoveCo);
            camMoveCo = controller.StartCoroutine(MoveCameraSmooth(targetCamPos, camMoveTime));
        }
        controller.StartCoroutine(EndTransition());
    }

    private IEnumerator MoveCameraSmooth(Vector3 targetPos, float duration)
    {
        Vector3 start = cineCamTr.position;
        float t = 0f;

        if (duration <= 0f)
        {
            cineCamTr.position = targetPos;
            yield break;
        }

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            cineCamTr.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }

        cineCamTr.position = targetPos;
    }

    private IEnumerator EndTransition()
    {
        yield return new WaitForSeconds(0.3f);
        isTransitioning = false;
    }
}
