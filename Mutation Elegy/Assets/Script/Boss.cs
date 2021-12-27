using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Idle,
    Run,
    Attack1,
    Attack2,
    Cooldown,
    Dead
}

public class Boss : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animator;
    private BossState bossState;

    public Transform postarget;
    public GameObject player;

    public bool canHurtPlayer = false;

    public BossState BossState { get => bossState;
        set {
            bossState = value;
            switch (value)
            {
                case BossState.Idle:
                    print("Idle");
                    animator.SetFloat("Run", 0);
                    agent.isStopped = true;
                    break;
                case BossState.Run:
                    print("Run");
                    animator.SetFloat("Run", 0.6f);
                    //����H���y��
                    //postarget = BossManager.Instance.GetRandomTarget();
                    //postarget = player.transform;
                    agent.isStopped = false;
                    agent.SetDestination(postarget.position);
                    break;
                case BossState.Attack1:
                    //animator.SetTrigger("Spit");
                    break;
                case BossState.Attack2:
                    //animator.SetTrigger("Attack");
                    break;

                case BossState.Cooldown:

                    break;
                case BossState.Dead:
                    break;
            }
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        BossState = BossState.Run;
    }

    private void Update()
    {
        BossStateForUpdate();
        //LookAtPlayer();
    }
    private void BossStateForUpdate()
    {
        switch (bossState)
        {
            case BossState.Idle:
                CheckAttack();
                break;
            case BossState.Run:
                //�p�G��F�ؼ�,�󴫾ɯ�ؼ�
                if (Vector3.Distance(postarget.position,transform.position) < 1.5f)
                //if(agent.stoppingDistance < 0.6f)
                {
                    //�H���𮧩Χ󴫥ؼ�
                    if(Random.Range(0,2) == 0)
                    {
                        BossState = BossState.Run;
                    }
                    else
                    {
                        BossState = BossState.Idle;
                        Invoke("Stay",Random.Range(3,5));
                    }
                }
                CheckAttack();
                break;
            case BossState.Attack1:
                //if (canHurtPlayer && Vector3.Distance(player.transform.position, transform.position) < 3.9)
                //{
                //    canHurtPlayer = false;

                //    Debug.Log("���Z������");
                //}
                break;
            case BossState.Attack2:                
                //if(canHurtPlayer && Vector3.Distance(player.transform.position, transform.position) < 4.9)
                //{
                //    canHurtPlayer = false;
                    
                //    Debug.Log("���a�l��");
                //}
                break;
            case BossState.Dead:
                break;
        }
    }
    //�ˬd����
    private void CheckAttack()
    {
            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                BossState = BossState.Attack2;
                agent.isStopped = true;
            }
    }

    /// <summary>
    /// ���d�@�q�ɶ��A�~��樫
    /// </summary>
    private void Stay()
    {
        if(BossState == BossState.Idle)
            BossState = BossState.Run;
    }

    private void CanHurt()
    {
        canHurtPlayer = true;
    }
    private void Recovery()
    {
        canHurtPlayer = false;
        BossState = BossState.Idle;
        Invoke("Stay", Random.Range(3, 5));
    }
}
