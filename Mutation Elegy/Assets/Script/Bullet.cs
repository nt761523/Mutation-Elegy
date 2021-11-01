using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;
    private Vector3 velocity;
    private float destroyTime = 2f;

    public int LV { get; set; }

    [Header("傷害數值")]
    public float damage;
    //[Header("顯示的數值")]
    //public Text damageText;
    //[Header("顯示的Canvas")]
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
            //計算傷害&顯示
            GameObject.Find("UICanvas").GetComponent<UIsetting>().Generate_DamageText(dispalyLocation, damage, other);
        }
        
    }
}
