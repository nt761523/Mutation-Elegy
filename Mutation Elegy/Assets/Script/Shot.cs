using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [Header("要生成的子彈Object")]
    public GameObject Bullet;
    [Header("子彈生成的位置")]
    public Transform Muzzle;


    void Start()
    {

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ProduceBullet();
        }
    }
    private void FixedUpdate()
    {



        //if (Input.GetMouseButton(1))
        //{
        //    ProduceBullet();
        //}
    }
    void ProduceBullet()
    {
        GameObject newbullet = Instantiate(Bullet, Muzzle.position, Muzzle.rotation);
    }
}
