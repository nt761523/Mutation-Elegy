using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [Header("�n�ͦ����l�uObject")]
    public GameObject Bullet;
    [Header("�l�u�ͦ�����m")]
    public Transform Muzzle;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ProduceBullet();
        }
    }
    void ProduceBullet()
    {
        GameObject newbullet = Instantiate(Bullet, Muzzle.position, Muzzle.rotation);
    }
}
