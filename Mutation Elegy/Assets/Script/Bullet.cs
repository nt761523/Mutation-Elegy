using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;
    private Vector3 velocity;
    private float destroyTime = 2f;

    public int LV { get; set; }

    [Header("�ˮ`�ƭ�")]
    public float damage;
    //[Header("��ܪ��ƭ�")]
    //public Text damageText;
    //[Header("��ܪ�Canvas")]
    //public GameObject canvas;

    void Start()
    {
        //LV = Level.LV;
        //damage = GameObject.Find("Model").GetComponent<BaseProties>().Power * LV;
        damage = 1;
        velocity = transform.forward * speed;
        Destroy(gameObject, destroyTime);
        //canvas = GameObject.Find("Canvas");
    }
    private void FixedUpdate()
    {
        transform.position += velocity * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Rigidbody enemy = other.GetComponent<Rigidbody>();
            enemy.AddForce(new Vector3(0,1,3), ForceMode.Impulse);
            Destroy(gameObject);

            Vector3 dispalyLocation = Camera.main.WorldToScreenPoint(other.transform.position + Vector3.up * 0.8f);
            //�p��ˮ`&���
            GameObject.Find("UICanvas").GetComponent<UIsetting>().Generate_DamageText(dispalyLocation, damage, other);
        }
        
    }
}
