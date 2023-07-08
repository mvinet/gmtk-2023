using UnityEngine;

public class Mouse : Entity<MouseDefinition>
{
    private GameObject cat;

    private void Start()
    {
        cat = GameObject.FindWithTag("Cat");
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            cat.transform.position,
            Time.deltaTime * moveSpeed
        );
    }
}