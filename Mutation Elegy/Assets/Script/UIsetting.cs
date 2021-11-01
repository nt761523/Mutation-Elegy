using UnityEngine;
using UnityEngine.UI;

public class UIsetting : MonoBehaviour
{
    [Header("�O�_��ܶˮ`")]
    public static bool isShowDamage = true;
    [Header("UICanvas��transform")]
    public static Transform UIframe;
    [Header("�n�ϥΪ�HPbar�˦�")]
    public Slider bar;
    [Header("hpbar�Y����")]
    private float Scale;
    [Header("�ˮ`��ܦr��")]
    public Text damageText;
    private void Awake()
    {
        UIframe = gameObject.transform;
        //bar = GameObject.Find("EnemyHpbar");
        ////�q�{��q���Z���A�i�H�אּ�`�q
        //Scale = Vector3.Distance(transform.position, Camera.main.transform.position);
        Scale = 5;
    }
    private void Update()
    {
        damageText.enabled = isShowDamage;
    }

    public Slider Generate_Bar()
    {
        //�ͦ�Object�y�k�G
        //Instantiate(�n�ͦ�������,�����m,��������)
        //Instantiate(Hpbar, canvas, false);
        //Instantiate(bar, UIframe, false);
        return Instantiate(bar, UIframe, false);
    }

    public void Transform_Bar(Slider bar,Transform objTransform)
    {
        ////HP���H�G�ù��y�СC ����G�@�ɮy��
        ////�@�ɮy��->�ù��y��

        //bar.transform.position = Camera.main.WorldToScreenPoint(objTransform.position + Vector3.up * 2);
        bar.transform.position = Camera.main.WorldToScreenPoint(objTransform.position + Vector3.up * 0.8f);

        //��m�ܤƴN�����Y����
        float newScale = Scale / Vector3.Distance(objTransform.position, Camera.main.transform.position);
        //�վ����j�p
        bar.transform.localScale = Vector3.one * newScale;
    }
    public void Generate_DamageText(Vector3 displaylocation,float damage, Collider other)
    {
        //���Ͷˮ`���Object
        Text temp = Instantiate(damageText, displaylocation, Quaternion.identity, UIframe);
        //�]�w�ƭ�
        Set_DamageText(temp, damage);
        UpdateHp(other, damage);
    }

    public void Set_DamageText(Text damageText, float damage)
    {
        damageText.GetComponent<DamageText>().setValue(damage);
    }

    public void UpdateHp(Collider other, float damage)
    {
        
        other.GetComponent<BaseProties>().currentHp -= damage;
        other.GetComponent<BaseProties>().hpBar.value = other.GetComponent<BaseProties>().currentHp / other.GetComponent<BaseProties>().maxMp;
        CheckHp(other);
    }
    public void CheckHp(Collider other)
    {
        if (other.GetComponent<BaseProties>().currentHp <= 0)
        {
            Destroy(other.GetComponent<BaseProties>().hpBar.gameObject);
            Destroy(other.gameObject);
        }
    }
}
