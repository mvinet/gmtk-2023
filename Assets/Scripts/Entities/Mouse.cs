using UnityEngine;

public class Mouse : Entity<MouseDefinition>
{
    private GameObject cat;
    private SpriteRenderer _spriteRenderer;
    
    private void Start()
    {
        Init(definition);
        cat = GameObject.FindWithTag("Cat");
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            cat.transform.position,
            Time.deltaTime * moveSpeed
        );

        _spriteRenderer.flipX = cat.transform.position.x < transform.position.x;
    }
}