using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Settings")]
    public float moveSpeed = 5f;

    [Header("Body Visual")]
    public Animator bodyAnimator;
    public SpriteRenderer bodyRenderer;

    private static readonly int MoveX = Animator.StringToHash("MoveX");
    private static readonly int MoveY = Animator.StringToHash("MoveY");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");


    public HeadVisual headVisual;
    public TearShooter tearShooter;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private LookDir lastAppliedDir;

    private StateMachine locomotionSM;

    private PlayerIdle idleState;
    private PlayerMove moveState;
    private PlayerAttack attackState;

    private Vector2 attackInput;
    public Vector2 GetAttackInput() => attackInput;

    public LookDir LookDir { get; private set; } = global::LookDir.Down;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        locomotionSM = new StateMachine();

        idleState = new PlayerIdle(this, locomotionSM);
        moveState = new PlayerMove(this, locomotionSM);
        attackState = new PlayerAttack(this, locomotionSM);

        locomotionSM.AddState(idleState);
        locomotionSM.AddState(moveState);
        locomotionSM.AddState(attackState);
    }

    private void Start()
    {
        locomotionSM.ChangeState<PlayerIdle>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //공격
        attackInput = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow)) attackInput = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow)) attackInput = Vector2.down;
        else if (Input.GetKey(KeyCode.LeftArrow)) attackInput = Vector2.left;
        else if (Input.GetKey(KeyCode.RightArrow)) attackInput = Vector2.right;


        //방향결정
        Vector2 lookInput = (attackInput.sqrMagnitude > 0.01f) ? attackInput : moveInput;
        LookDir = CalcLookDir(lookInput, LookDir);

        if (headVisual != null && LookDir != lastAppliedDir)
        {
            headVisual.Apply(LookDir);
            lastAppliedDir = LookDir;
        }

        locomotionSM.Tick();

        if (bodyAnimator != null)
        {
            Vector2 dir = moveInput;
            if (dir.sqrMagnitude > 0.01f) dir.Normalize();

            float animX = 0f;
            float animY = 0f;

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {

                animX = 1f; 
                animY = 0f;
            }
            else
            {

                animX = 0f;
                animY = -1f;
            }

            bodyAnimator.SetFloat(MoveX, animX);
            bodyAnimator.SetFloat(MoveY, animY);
        }

        if (bodyRenderer != null)
        {
            if (LookDir == global::LookDir.Left) bodyRenderer.flipX = true;
            else if (LookDir == global::LookDir.Right) bodyRenderer.flipX = false;

        }

    }

    private void FixedUpdate()
    {
        locomotionSM.FixedTick();
    }

    public Vector2 GetMoveInput() => moveInput;

    public void SetVelocity(Vector2 vel)
    {
        rb.linearVelocity = vel;
    }

    private static LookDir CalcLookDir(Vector2 input, LookDir fallback)
    {
        if (input.sqrMagnitude < 0.01f) return fallback;

        input = input.normalized;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            return input.x > 0 ? global::LookDir.Right : global::LookDir.Left;
        else
            return input.y > 0 ? global::LookDir.Up : global::LookDir.Down;
    }
}
