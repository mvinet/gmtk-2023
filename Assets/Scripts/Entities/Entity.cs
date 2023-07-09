using System.Collections;
using TMPro;
using UI;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Vector2 initialPosition;
    public int currentHp;
    public int currentAttackDamage;
    public float currentCooldown;
    public float currentAttackSpeed;
    public float currentAttackRange;
    public float moveSpeed;

    public SpriteRenderer _spriteRenderer;
    public Entity target;

    public GameObject fxDamages;
    
    public virtual void Start()
    {
        PlayStateManager.instance.entities.Add(this);
    }

    public virtual void Update()
    {
        if (PlayStateManager.instance.currentMode != PlayMode.Play)
            return;
        currentCooldown -= Time.deltaTime;
        if (target == null)
            return;
        
        if (Vector2.Distance(transform.position, target.transform.position) <= currentAttackRange &&
            currentCooldown <= 0 && currentHp > 0)
        {
            currentCooldown = 1 / currentAttackSpeed;
            
            Attack();
        }
    }

    public virtual void FixedUpdate()
    {
        if (target == null)
            return;
        if (PlayStateManager.instance.currentMode != PlayMode.Play ||
            Vector2.Distance(target.transform.position,transform.position) <= currentAttackRange)
            return;
        
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            Time.deltaTime * moveSpeed
        );
        
        _spriteRenderer.flipX = target.transform.position.x < transform.position.x;

    }


    public virtual void Attack()
    {
    }

    public void DealDamage(int damage)
    {

        if (fxDamages && damage > 0)
        {
            var fx = Instantiate(fxDamages, transform.position, Quaternion.identity);
            fx.GetComponentInChildren<TextMesh>().text = damage.ToString();
            StartCoroutine(DestroyFx(fx));
        }

        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }

    private IEnumerator DestroyFx(GameObject fx)
    {
        yield return new WaitForSeconds(1);
        Destroy(fx);
    }
    
    public virtual void ReloadDefinition(){}

    public virtual void Die()
    {
    }
}

public abstract class Entity<T> : Entity where T : EntityDefinition
{
    public T definition;


    public void Init(T definition)
    {
        this.definition = definition;
        initialPosition = new Vector2(transform.position.x,transform.position.y);
        ReloadDefinition();
    }

    public override void ReloadDefinition()
    {
        transform.position = initialPosition;
        currentHp = definition.hp;
        currentAttackDamage = definition.attackDamage;
        currentCooldown = 0f;
        currentAttackRange = definition.attackRange;
        moveSpeed = definition.moveSpeed;
        currentAttackSpeed = definition.attackSpeed;
    }


    public override void Attack()
    {
        target.DealDamage(currentAttackDamage);
    }
    
    private void OnMouseEnter()
    {
        TooltipManager.Instance.SetEntityDefinitionTooltip(definition);
    }

    private void OnMouseExit()
    {
        TooltipManager.Instance.HideTooltip();
    }
    
    
}