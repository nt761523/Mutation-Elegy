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


    [Header("攻擊範圍位移&尺寸")]
    public Vector3 v3AttackOffset;
    public Vector3 v3AttackSize = Vector3.one;
    private void OnDrawGizmos()
    {
        #region 攻擊丶追蹤隨機範圍&行走座標
        Gizmos.color = new Color(1,0,0.2f,0.3f);
        Gizmos.DrawSphere(transform.position,rangeAttack);
        Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeTrack);

        if(state == StateEnemy.Move)
        {
            Gizmos.color = new Color(1f, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(v3RandomMoveFinal, 0.3f);
        }
        #endregion

        //攻擊碰撞判定範圍
        Gizmos.color = new Color(0.8f, 0.2f, 0.7f, 0.3f);
        Gizmos.matrix = Matrix4x4.TRS(
            transform.position +
            transform.forward * v3AttackOffset.x +
            transform.up * v3AttackOffset.y +
            transform.right * v3AttackOffset.z,
            transform.rotation, transform.localScale);
        Gizmos.DrawCube(Vector3.zero, v3AttackSize);
    }

    private Animator animator;
    private NavMeshAgent nma;
    private Vector3 v3RandomIdleMove
    {
        get => Random.insideUnitSphere * rangeTrack + transform.position;
    }

    private Transform traPlayer;
    private string namePlayer = "Player";

    private void Awake()
    {
        traPlayer = GameObject.Find(namePlayer).transform;
        animator = GetComponent<Animator>();
        nma = GetComponent<NavMeshAgent>();
        nma.speed = speed;

        nma.SetDestination(transform.position);
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
                Track();
                break;
            case StateEnemy.Attack:
                Attack();
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
        if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;

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
        if (!targetIsDead && playerInTrackRange) state = StateEnemy.Track;

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

    private bool playerInTrackRange { get => Physics.OverlapSphere(transform.position, rangeTrack, 1 << 7).Length > 0; }

    private bool isTrack;
    private void Track()
    {
        if(!isTrack)
        {
            StopAllCoroutines();
        }

        isTrack = true;

        nma.isStopped = false;
        nma.SetDestination(traPlayer.position);
        animator.SetBool("Move", true);

        if (nma.remainingDistance <= rangeAttack) state = StateEnemy.Attack;
    }
    [Header("攻擊冷卻時間"),Range(0,5)]
    public float timeAttack = 2.5f;
    [Header("攻擊延遲傳送傷害時間"), Range(0, 5)]
    public float delaySendDamage = 0.5f;

    private string parameterAttack = "Attack";
    private bool isAttack;

    private void Attack()
    {
        nma.isStopped = true;
        animator.SetBool("Move",false);
        nma.SetDestination(traPlayer.position);
        LookAtPlayer();


        if (nma.remainingDistance >= rangeAttack) state = StateEnemy.Track;

        if (isAttack) return;
        isAttack = true;

        animator.SetTrigger(parameterAttack);

        StartCoroutine(DelaySendDamageToTarget());
    }

    private bool targetIsDead;
    private IEnumerator DelaySendDamageToTarget()
    {
        yield return new WaitForSeconds(delaySendDamage);

        Collider[] hits = Physics.OverlapBox(transform.position +
        transform.forward * v3AttackOffset.x +
        transform.up * v3AttackOffset.y +
        transform.right * v3AttackOffset.z,
        v3AttackSize / 2, Quaternion.identity, 1 << 7);

        if (hits.Length > 0) targetIsDead = hits[0].GetComponent<HurtSystem>().Hurt(attack);
        if(targetIsDead) TargetDead();

        float waitToNextAttack = timeAttack - delaySendDamage;
        yield return new WaitForSeconds(waitToNextAttack);

        isAttack = false;
    }
    private void TargetDead()
    {
        state = StateEnemy.Move;
        isIdle = false;
        isMove = false;

        nma.isStopped = false;
    }

    [Header("面向玩家的速度"), Range(0, 50)]
    public float speedLookAt = 10;
    private void LookAtPlayer()
    {
        Quaternion angle = Quaternion.LookRotation(traPlayer.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * speedLookAt);
    }

    public void Dead()
    {
        StaticVal.Enemykilled++;
    }
}