using UnityEngine;

public enum LookDir { Down, Up, Left, Right }

public class HeadVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;

    [Header("기본머리 스프라이트")]
    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite rightSprite;

    [Header("기본공격 스프라이트")]
    public Sprite tearDownSprite;
    public Sprite tearUpSprite;
    public Sprite tearRightSprite;

    private bool isTearing;

    private LookDir _currentDir;
    private Coroutine _tearCo;

    public void SetCurrentDir(LookDir dir) => _currentDir = dir;

    private void Reset()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Apply(LookDir dir)
    {
        if (sr == null) return;

        _currentDir = dir;

        sr.flipX = false;

        bool useTear = isTearing;

        switch (dir)
        {
            case LookDir.Down:
                sr.sprite = (useTear && tearDownSprite != null) ? tearDownSprite : downSprite;
                break;

            case LookDir.Up:
                sr.sprite = (useTear && tearUpSprite != null) ? tearUpSprite : upSprite;
                break;

            case LookDir.Right:
                sr.sprite = (useTear && tearRightSprite != null) ? tearRightSprite : rightSprite;
                break;

            case LookDir.Left:
                sr.sprite = (useTear && tearRightSprite != null) ? tearRightSprite : rightSprite;
                sr.flipX = true;
                break;
        }
    }


    //발사순간 깜빡임
    public void TearOnce(float duration = 0.08f)
    {
        if (!gameObject.activeInHierarchy) return;

        if (_tearCo != null) StopCoroutine(_tearCo);
        _tearCo = StartCoroutine(TearOnceRoutine(duration));
    }

    private System.Collections.IEnumerator TearOnceRoutine(float duration)
    {
        isTearing = true;
        Apply(_currentDir);             
        yield return new WaitForSeconds(duration);
        isTearing = false;
        Apply(_currentDir);             
    }
}