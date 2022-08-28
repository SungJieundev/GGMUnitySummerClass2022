using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : PoolableMono, IHittable, IAgent
{
    [SerializeField]
    protected EnemyDataSO _enemyData;
    public EnemyDataSO EnemyData { get => _enemyData; }

    public bool IsEnemy => true;

    public Vector3 HitPoint { get; private set; }
    [field: SerializeField]
    public int Health { get; private set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }
    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    protected bool _isDead = false;

    [SerializeField]
    protected bool _isActive = false; //������ ��Ƽ�� �����ָ� ������ �����ҰŴ�.
    protected EnemyAIBrain _brain;
    protected EnemyAttack _attack;

    protected CapsuleCollider2D _bodyCollider;
    protected SpriteRenderer _spriteRenderer = null;
    protected AgentMovement _agentMovement = null;


    protected virtual void Awake()
    {
        _brain = GetComponent<EnemyAIBrain>();
        _attack = GetComponent<EnemyAttack>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _agentMovement = GetComponent<AgentMovement>();
        _spriteRenderer = transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
        SetEnemyData();
        Init();
    }

    public override void Init()
    {
        _brain.enabled = false;
        _isActive = false;
        _bodyCollider.enabled = false;
        _agentMovement.enabled = false;
        _isDead = false;

        if (_spriteRenderer.material.HasProperty("_Dissolve"))
        {
            _spriteRenderer.material.SetFloat("_Dissolve", 0);
        }
    }


    public void Spawn()
    {
        Sequence seq = DOTween.Sequence();
        Tween dissolve = DOTween.To(
            () => _spriteRenderer.material.GetFloat("_Dissolve"),
            x => _spriteRenderer.material.SetFloat("_Dissolve", x),
            1f,
            1f);

        seq.Append(dissolve);
        seq.AppendCallback(() => ActiveObject());
    }

    public void ActiveObject()
    {
        _brain.enabled = true;
        _isActive = true;
        _bodyCollider.enabled = true;
        _agentMovement.enabled = true;
        Health = _enemyData.maxHealth;
    }

    private void SetEnemyData()
    {
        _attack.AttackDelay = _enemyData.attackDelay; //���ݵ����̸� ����

        transform.Find("AI/IdleState/TranChase")
            .GetComponent<DecisionInner>().Distance = _enemyData.chaseRange;
        transform.Find("AI/ChaseState/TranIdle")
            .GetComponent<DecisionInner>().Distance = _enemyData.chaseRange;

        transform.Find("AI/ChaseState/TranAttack")
            .GetComponent<DecisionInner>().Distance = _enemyData.attackRange;
        transform.Find("AI/AttackState/TranChase")
            .GetComponent<DecisionOuter>().Distance = _enemyData.attackRange;

        Health = _enemyData.maxHealth;
    }

    public virtual void PerformAttack()  //������ �õ��Ѵ�.
    {
        if(_isDead == false && _isActive == true)
        {
            _attack.Attack(_enemyData.damage);
        }
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead == true) return;

        bool isCritical = GameManager.Instance.IsCritical;
        if(isCritical){
            damage = GameManager.Instance.GetCriticalDamage(damage);
        }
        
        Health -= damage;
        HitPoint = damageDealer.transform.position; //���� �� �༮

        OnGetHit?.Invoke(); //�ǰݽ� �ǵ���� ���� �̺�Ʈ Ʈ����


        PopupText popupText = PoolManager.Instance.Pop("PopupText") as PopupText;
        popupText?.Setup(damage, transform.position + new Vector3(0, 0.3f), isCritical, Color.white);

        if (Health <= 0)
            DeadProcess();
    }

    private void DeadProcess()
    {
        Health = 0;
        _isDead = true;
        GameManager.Instance._score++;
        OnDie?.Invoke();
    }

    public void Die()
    {
        PoolManager.Instance.Push(this);
    }    
}
