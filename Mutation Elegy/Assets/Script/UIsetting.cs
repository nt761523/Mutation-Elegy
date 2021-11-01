using UnityEngine;
using UnityEngine.UI;

public class UIsetting : MonoBehaviour
{
    [Header("是否顯示傷害")]
    public static bool isShowDamage = true;
    [Header("UICanvas的transform")]
    public static Transform UIframe;
    [Header("要使用的HPbar樣式")]
    public Slider bar;
    [Header("hpbar縮放比例")]
    private float Scale;
    [Header("傷害顯示字形")]
    public Text damageText;
    private void Awake()
    {
        UIframe = gameObject.transform;
        //bar = GameObject.Find("EnemyHpbar");
        ////默認血量的距離，可以改為常量
        //Scale = Vector3.Distance(transform.position, Camera.main.transform.position);
        Scale = 5;
    }
    private void Update()
    {
        damageText.enabled = isShowDamage;
    }

    public Slider Generate_Bar()
    {
        //生成Object語法：
        //Instantiate(要生成的物件,物件位置,物件旋轉值)
        //Instantiate(Hpbar, canvas, false);
        //Instantiate(bar, UIframe, false);
        return Instantiate(bar, UIframe, false);
    }

    public void Transform_Bar(Slider bar,Transform objTransform)
    {
        ////HP跟隨：螢幕座標。 物件：世界座標
        ////世界座標->螢幕座標

        //bar.transform.position = Camera.main.WorldToScreenPoint(objTransform.position + Vector3.up * 2);
        bar.transform.position = Camera.main.WorldToScreenPoint(objTransform.position + Vector3.up * 0.8f);

        //位置變化就改變縮放比例
        float newScale = Scale / Vector3.Distance(objTransform.position, Camera.main.transform.position);
        //調整血條大小
        bar.transform.localScale = Vector3.one * newScale;
    }
    public void Generate_DamageText(Vector3 displaylocation,float damage, Collider other)
    {
        //產生傷害顯示Object
        Text temp = Instantiate(damageText, displaylocation, Quaternion.identity, UIframe);
        //設定數值
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
