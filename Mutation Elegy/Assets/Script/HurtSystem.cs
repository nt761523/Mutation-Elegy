using UnityEngine;
using UnityEngine.Events;

public class HurtSystem : MonoBehaviour
{
    [Header("��q"), Range(0f, 5000f)]
    public float hp = 100;
    [Header("���˨ƥ�")]
    public UnityEvent onHurt;
    [Header("���`�ƥ�")]
    public UnityEvent onDead;
    [Header("�ʵe�Ѽ�:����&���`")]
    public string parameterHurt = "";
    public string parameterDead = "";

    protected float hpMax;
    private Animator animator;

    [Header("���˯S��")]
    public GameObject vfx;

    [Header("���˭���")]
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

        //�ͦ��ɤl�S��
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
