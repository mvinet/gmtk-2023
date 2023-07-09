using System.Collections;
using TMPro;
using System.Collections.Generic;
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
    public List<Passive> passiveObjects = new();

    public GameObject fxDamages;
    public GameObject explosionFxPrefab;
    
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
            currentCooldown <= 0 && currentHp > 0 && target.currentHp > 0)
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

    public void DealDamage(int damage, Context context)
    {

        if (fxDamages && damage > 0)
        {
            var fx = Instantiate(fxDamages, transform.position, Quaternion.identity);
            fx.GetComponentInChildren<TextMesh>().text = damage.ToString();
            StartCoroutine(DestroyFx(fx));
        }

        currentHp -= damage;
        var damageReceiveContext = new Context()
        {
            source = this,
            target = this,
            value = damage
        };
        TriggerManager.OnDamageReceived.Invoke(damageReceiveContext);
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
        var context = new Context()
        {
            source = this,
            target = this
        };
        TriggerManager.OnDeath.Invoke(context);
        ClearPassives();
        Instantiate(explosionFxPrefab, transform);
    }
    
    public void ClearPassives()
    {
        foreach (Passive passive in passiveObjects)
        {
            if (passive != null)
            {
                passive.Delete(new Context());
            }
        }

        passiveObjects.Clear();
    }
}

public abstract class Entity<T> : Entity where T : EntityDefinition
{
    public T definition;
    public Passive passivePrefab;

    public void Init(T definition)
    {
        this.definition = definition;
        var position = transform.position;
        initialPosition = new Vector2(position.x,position.y);
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
        
        ClearPassives();
        foreach (var passiveDefinition in definition.passives)
        {
            Passive newPassive = Instantiate(passivePrefab, this.transform);
            newPassive.holder = this;
            newPassive.definition = passiveDefinition;
            passiveObjects.Add(newPassive);
        }
    }


    public override void Attack()
    {
        var attackContext = new Context()
        {
            passiveHolder = this,
            source = this,
            target = target,
            value = currentAttackDamage
        };
        target.DealDamage(currentAttackDamage, attackContext);
        TriggerManager.OnAttack.Invoke(attackContext);
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