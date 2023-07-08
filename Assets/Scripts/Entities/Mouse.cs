using UnityEngine;

public class Mouse : Entity<MouseDefinition>
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        Init(definition);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        target = PlayStateManager.instance.cat;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            Time.deltaTime * moveSpeed
        );

        _spriteRenderer.flipX = target.transform.position.x < transform.position.x;
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}