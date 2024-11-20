using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float SearchTimer;
    private float moveTimer;
    public override void Enter()
    {
     enemy.Agent.SetDestination(enemy.LastKnowPos);   
    }
       public override void Perform()
    {
        if(enemy.CanSeePlayer())
            stateMachine.ChangeState(new AttackState());
        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            SearchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(3, 4))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 9));
                moveTimer = 0f;
            }
            if(SearchTimer > 5)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
       public override void Exit()
    {
        
    }
}