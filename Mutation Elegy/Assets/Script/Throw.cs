using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [Header("要生成的子彈Object")]
    public GameObject Bullet;
    [Header("子彈生成的位置")]
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
        if (Input.GetMouseButtonDown(2))
        {
            //Invoke("ProduceBullet", 0.05f);
            ProduceBullet();
        }
    }
    void ProduceBullet()
    {
        Vector3 offect = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        Instantiate(Bullet, Muzzle.position + offect, Muzzle.rotation);
        //Instantiate(Bullet, Muzzle.position + offect, Muzzle.rotation);
        //Instantiate(Bullet, Muzzle.position + offect, Muzzle.rotation);
    }
}
