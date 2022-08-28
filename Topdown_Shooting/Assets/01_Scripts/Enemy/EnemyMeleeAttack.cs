using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        if(_waitBeforeNextAttack == false)
        {
            _brain.AIActionData.isAttack = true;

            float range = _brain.Enemy.EnemyData.attackRange;

            float distance = Vector2.Distance(_brain.BasePosition.position, _brain.Target.position);

            if(distance < range){
                IHittable hit = _brain.Target.GetComponent<IHittable>();
                hit?.GetHit(damage, gameObject);
            }
            
            AttackFeedback?.Invoke();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
}
