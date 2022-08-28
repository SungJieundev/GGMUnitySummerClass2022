using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    private EnemyAIBrain _brain = null;

    [SerializeField]
    private List<AIAction> _actions = null;
    [SerializeField]
    private List<AITransition> _transition = null;

    private void Awake()
    {
        _brain = transform.parent.parent.GetComponent<EnemyAIBrain>();
    }

    public void UpdateState()
    {
        foreach(AIAction a in _actions)
        {
            a.TakeAction();
        }

        foreach(AITransition tr in _transition)
        {
            bool result = false;
            foreach(AIDecision d in tr.decisions)
            {
                result = d.MakeADecision();
                if (result == false) break;
            }

            if(result == true) //해당 전이에 있는 모든 Decision이 참
            {
                if(tr.positiveState != null)
                {
                    _brain.ChangeState(tr.positiveState); // EnemyBrain 에게 positive 로 변경하라고 통보
                    return;
                }
            }else //해당 전이에ㅔ 있는 Decision 중 하나가 거짓
            {
                if (tr.negativeState != null)
                {
                    _brain.ChangeState(tr.negativeState); // EnemyBrain 에게 negative 로 변경하라고 통보
                    return;
                }
            }
        }
    }
}
