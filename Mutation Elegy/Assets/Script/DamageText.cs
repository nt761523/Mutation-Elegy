using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public float destroyTime = 1f;
    //public float damage;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position += Vector3.up;
    }
    public void setValue(float number)
    {
        GetComponent<Text>().text = number.ToString();
    }
}