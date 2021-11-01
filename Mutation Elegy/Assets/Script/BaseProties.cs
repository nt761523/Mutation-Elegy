using UnityEngine;
using UnityEngine.UI;

public class BaseProties : MonoBehaviour
{
    public int maxHp = 100;
    public float currentHp;
    public int maxMp = 100;
    public float currentMp;
    public float moveSpeed = 5;
    public float Power = 2;
    public bool isGrounded;
    public Slider hpBar;

    private void Awake()
    {
        //currentHp = maxHp;
        //currentMp = maxMp;
        ////���ͦ��
        //hpBar = GameObject.Find("UICanvas").GetComponent<UIsetting>().Generate_Bar();
    }
    void Start()
    {
        currentHp = maxHp;
        currentMp = maxMp;
        //���ͦ��
        hpBar = GameObject.Find("UICanvas").GetComponent<UIsetting>().Generate_Bar();
    }

    void Update()
    {
        //��s����ܴ���T
        GameObject.Find("UICanvas").GetComponent<UIsetting>().Transform_Bar(hpBar, transform);
    }
}
