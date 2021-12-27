using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum State
{
    Idle,
    Run,
    Attack1,
    Attack2,
    Cooldown,
    Dead
}
public class Boss_v2 : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animator;
    private State bossState;

    public Transform postarget;
    public GameObject player;
    public bool canHurtPlayer = false;
    public State BossState { get => bossState;
        set { bossState = value;
            switch (value)
            {
                case State.Idle:
                    animator.SetFloat("Run", 0f);
                    break;
                case State.Run:
                    animator.SetFloat("Run", 0.6f);
                    //agent.isStopped = false;
                    agent.SetDestination(postarget.position);
                    break;
                case State.Attack1:
                    break;
                case State.Attack2:
                    break;
                case State.Cooldown:
                    break;
                case State.Dead:
                    break;
            }
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        BossState = State.Run;
    }
    private void Update()
    {
        BossStateForUpdate();
    }
    private void BossStateForUpdate()
    {
        switch (bossState)
        {
            case State.Idle:
                break;
            case State.Run:
                //if (Vector3.Distance(postarget.position, transform.position) < 1.5f)
                if(agent.stoppingDistance < 0.6f)
                {
                    //隨機休息或更換目標
                    if (Random.Range(0, 2) == 0)
                    {
                        BossState = State.Run;
                    }
                    else
                    {
                        BossState = State.Idle;
                        Invoke("Stay", Random.Range(3, 5));
                    }
                }
                break;
            case State.Attack1:

                break;
            case State.Attack2:
                break;
            case State.Dead:
                break;
        }
    }
    private void Stay()
    {
        if (BossState == State.Idle)
            BossState = State.Run;
    }
}