using UnityEngine;
using UnityEngine.Events;

public class HurtSystem : MonoBehaviour
{
    [Header("血量"), Range(0f, 5000f)]
    public float hp = 100;
    [Header("受傷時間")]
    public UnityEvent onHurt;
    [Header("死亡事件")]
    public UnityEvent onDead;
    [Header("動畫參數:受傷&死亡")]
    public string parameterHurt = "";
    public string parameterDead = "";

    protected float hpMax;
    private Animator animator;

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
