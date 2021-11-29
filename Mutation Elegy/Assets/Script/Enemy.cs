using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"),Range(0,20)]
    public float speed = 2.5f;
    [Header("攻擊力"), Range(0, 20)]
    public float attack = 5;
    [Header("範圍:追蹤&攻擊")]
    [Range(0, 7)]
    public float rangeAttack = 1.5f;
    [Range(7, 20)]
    public float rangeTrack = 10;

    [SerializeField]
    private StateEnemy state;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0.2f,0.3f);
        Gizmos.DrawSphere(transform.position,rangeAttack);
        Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeTrack);

        if(state == StateEnemy.Move)
        {
            Gizmos.color = new Color(1f, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(v3RandomMoveFinal, 0.3f);
        }
    }






    private Animator animator;
    private NavMeshAgent nma;
    private Vector3 v3RandomIdleMove
    {
        get => Random.insideUnitSphere * rangeTrack + transform.position;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nma = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        StateManger();
    }

    void StateManger()
    {
        switch (state)
        {
            case StateEnemy.Idle:
                Idle();
                break;
            case StateEnemy.Move:
                Move();
                break;
            case StateEnemy.Track:
                break;
            case StateEnemy.Attack:
                break;
            case StateEnemy.Hurt:
                break;
            case StateEnemy.Dead:
                break;
            default:
                break;
        }
    }


    private bool isIdle;
    [Header("等待隨機秒數")]
    public Vector2 v2RandomWait = new Vector2(1, 5);
    private void Idle()
    {
        if (isIdle) return;
        isIdle = true;
        //print("wait");

        animator.SetBool("Move", false);
        StartCoroutine(IdleEffect());
    }
    private IEnumerator IdleEffect()
    {
        float randomWait = Random.Range(v2RandomWait.x,v2RandomWait.y);
        yield return new WaitForSeconds(randomWait);

        state = StateEnemy.Move;
        isIdle = false;
    }



    private bool isMove;
    [Header("移動隨機秒數")]
    public Vector2 v2RandomMove = new Vector2(3, 7);
    public Vector3 v3RandomMoveFinal;
    private void Move()
    {
        nma.SetDestination(v3RandomMoveFinal);
        animator.SetBool("Move", nma.remainingDistance > 0.1f);

        if (isMove) return;
        isMove = true;
        //print("Move");

        NavMeshHit hit;
        NavMesh.SamplePosition(v3RandomIdleMove, out hit,rangeTrack, NavMesh.AllAreas);
        v3RandomMoveFinal = hit.position;
        
        StartCoroutine(MoveEffect());
    }
    private IEnumerator MoveEffect()
    {
        float randomMove = Random.Range(v2RandomMove.x, v2RandomMove.y);
        yield return new WaitForSeconds(randomMove);

        state = StateEnemy.Idle;
        isMove = false;
    }
}