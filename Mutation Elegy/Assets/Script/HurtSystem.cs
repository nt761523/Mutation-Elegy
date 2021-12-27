using UnityEngine;
using UnityEngine.Events;

public class HurtSystem : MonoBehaviour
{
    [Header("血量"), Range(0f, 5000f)]
    public float hp = 100;
    [Header("受傷事件")]
    public UnityEvent onHurt;
    [Header("死亡事件")]
    public UnityEvent onDead;
    [Header("動畫參數:受傷&死亡")]
    public string parameterHurt = "";
    public string parameterDead = "";

    protected float hpMax;
    private Animator animator;

    [Header("受傷特效")]
    public GameObject vfx;

    [Header("受傷音效")]
    public GameObject hurtAudio;

    private void Awake()
    {
        hpMax = hp;
        animator = GetComponentInChildren<Animator>();
        //gameObject.GetComponentInChildren<Animator>();
    }
    public virtual bool Hurt(float damage)
    {
        if (animator.GetBool(parameterDead)) return true;
        hp -= damage;
        animator.SetTrigger(parameterHurt);

        //生成粒子特效
        GameObject.Instantiate<GameObject>(vfx, StaticVal.playerpos.position + StaticVal.playerpos.up, Quaternion.identity,null);
        Instantiate(hurtAudio, Vector2.zero, Quaternion.identity);
        onHurt.Invoke();
        if (hp <= 0)
        {
            Dead();
            return true;
        }
        else return false;
    }
    public void Dead()
    {
        animator.SetBool(parameterDead, true);
        onDead.Invoke();
    }
}
