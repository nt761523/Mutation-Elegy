using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;


enum StateBoss
{
    Idle, Move, Track, Attack, Hurt, Dead,Cooldown
}

public class EnemyBoss : MonoBehaviour
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
    private StateBoss state;


    [Header("攻擊範圍位移&尺寸")]
    public Vector3 v3AttackOffset;
    public Vector3 v3AttackSize = Vector3.one;

    [Header("攻擊特效")]
    public GameObject attackVFX;

    public PlayableDirector bosscut;

    private void OnDrawGizmos()
    {
        #region 攻擊丶追蹤隨機範圍&行走座標
        Gizmos.color = new Color(1,0,0.2f,0.3f);
        Gizmos.DrawSphere(transform.position,rangeAttack);
        Gizmos.color = new Color(0.2f, 1, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, rangeTrack);

        if(state == StateBoss.Move)
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
            case StateBoss.Idle:
                Idle();
                break;
            case StateBoss.Move:
                Move();
                break;
            case StateBoss.Track:
                Track();
                break;
            case StateBoss.Attack:
                Attack();
                break;
            case StateBoss.Hurt:
                break;
            case StateBoss.Dead:
                break;
            case StateBoss.Cooldown:
                Cooldown();
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
        if (!targetIsDead && playerInTrackRange) state = StateBoss.Track;

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

        state = StateBoss.Move;
        isIdle = false;
    }

    private bool isMove;
    [Header("移動隨機秒數")]
    public Vector2 v2RandomMove = new Vector2(3, 7);
    public Vector3 v3RandomMoveFinal;
    private void Move()
    {
        if (!targetIsDead && playerInTrackRange) state = StateBoss.Track;

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

        state = StateBoss.Idle;
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

        if (nma.remainingDistance <= rangeAttack) state = StateBoss.Attack;
    }
    [Header("攻擊冷卻時間"),Range(0,5)]
    public float timeAttack = 2.5f;
    [Header("攻擊延遲傳送傷害時間1"), Range(0, 5)]
    public float delaySendDamage1 = 0.5f;
    [Header("攻擊延遲傳送傷害時間2"), Range(0, 5)]
    public float delaySendDamage2 = 0.5f;
    [Header("攻擊延遲傳送傷害時間3"), Range(0, 5)]
    public float delaySendDamage3 = 0.5f;


    private string parameterAttack = "Attack";
    private bool isAttack;

    private void Attack()
    {
        nma.isStopped = true;
        animator.SetBool("Move",false);
        nma.SetDestination(traPlayer.position);
        LookAtPlayer();


        if (nma.remainingDistance >= rangeAttack) state = StateBoss.Track;

        if (isAttack) return;
        isAttack = true;

        animator.SetTrigger(parameterAttack);

        StartCoroutine(DelaySendDamageToTarget());
    }

    private bool targetIsDead;
    private IEnumerator DelaySendDamageToTarget()
    {
        yield return new WaitForSeconds(delaySendDamage1);
        Collider[] hit1 = Physics.OverlapBox(transform.position +
        transform.forward * v3AttackOffset.x +
        transform.up * v3AttackOffset.y +
        transform.right * v3AttackOffset.z,
        v3AttackSize / 2, Quaternion.identity, 1 << 7);
        GenerateVFX(0,-30);
        if (hit1.Length > 0) targetIsDead = hit1[0].GetComponent<HurtSystem>().Hurt(attack);

        yield return new WaitForSeconds(delaySendDamage2);
        Collider[] hit2 = Physics.OverlapBox(transform.position +
        transform.forward * v3AttackOffset.x +
        transform.up * v3AttackOffset.y +
        transform.right * v3AttackOffset.z,
        v3AttackSize / 2, Quaternion.identity, 1 << 7);
        GenerateVFX(0,30);
        if (hit2.Length > 0) targetIsDead = hit2[0].GetComponent<HurtSystem>().Hurt(attack);

        yield return new WaitForSeconds(delaySendDamage3);
        Collider[] hit3 = Physics.OverlapBox(transform.position +
        transform.forward * v3AttackOffset.x +
        transform.up * v3AttackOffset.y +
        transform.right * v3AttackOffset.z,
        v3AttackSize / 2, Quaternion.identity, 1 << 7);

        GenerateVFX(-0.7f,-10);
        GenerateVFX(0.7f,10);
        if (hit3.Length > 0) targetIsDead = hit3[0].GetComponent<HurtSystem>().Hurt(attack);

        if (targetIsDead) TargetDead();

        float waitToNextAttack = timeAttack - delaySendDamage3;

        yield return new WaitForSeconds(waitToNextAttack);


        isAttack = false;

    }
    private void GenerateVFX(float aroundDis,float rotation)
    {
        GameObject.Instantiate<GameObject>(attackVFX, transform.position +
        transform.forward * v3AttackOffset.x +
        transform.up * v3AttackOffset.y +
        transform.right * v3AttackOffset.z + new Vector3(aroundDis,0,0), transform.rotation * Quaternion.Euler(0,0, rotation),null);
    }

    private void TargetDead()
    {
        state = StateBoss.Move;
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
        bosscut.Play();
    }

    private bool isCooldow;
    [Header("冷卻隨機秒數")]
    public Vector2 v2RandomCooldown = new Vector2(3, 5);
    public void Cooldown()
    {
        isCooldow = true;
        animator.SetBool("Cooldown",true);
        StartCoroutine(IdleCooldown());
    }
    private IEnumerator IdleCooldown()
    {
        float randomWait = Random.Range(v2RandomCooldown.x, v2RandomCooldown.y);
        yield return new WaitForSeconds(randomWait);

        state = StateBoss.Move;
        isCooldow = false;
    }
}