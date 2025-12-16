using UnityEngine;

public class BasicTear : MonoBehaviour
{
    [Header("Runtime")]
    public float damage = 1f;
    public float lifeTime = 2.0f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 velocity, float gravityScale, float dmg, float life)
    {
        damage = dmg;
        lifeTime = life;

        rb.gravityScale = gravityScale;     
        rb.linearVelocity = velocity;

        CancelInvoke();
        Invoke(nameof(Die), lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Enemy 판정해서 데미지 주기
        Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
