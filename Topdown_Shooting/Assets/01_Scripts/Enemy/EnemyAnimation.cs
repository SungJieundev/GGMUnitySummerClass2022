using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : AgentAnimation
{
    protected EnemyAIBrain _brain;
    protected readonly int _attackHash = Animator.StringToHash("Attack");
    protected readonly int _DeadBoolHash = Animator.StringToHash("IsDead");

    protected override void Awake()
    {
        base.Awake();
        _brain = transform.parent.GetComponent<EnemyAIBrain>();
    }

    public void SetEndOfAttackAnimation()
    {
        //���⼭ �극���� ���ؼ� ���ݻ��¸� ��������� ��
        _brain.AIActionData.isAttack = false;
    }

    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(_attackHash);
    }

    public override void PlayDeadAnimation()
    {
        base.PlayDeadAnimation();
        _animator.SetBool(_DeadBoolHash, true);
    }

    public void EndOfDeadAnimation()
    {
        _brain.Enemy.Die();
    }

}

//AIActionData
//AIMovementData
// -> IdleAction, ChaseAction
//AttackAction
//EnemyAnimation ����� �ְ� 
