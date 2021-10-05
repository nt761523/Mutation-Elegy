using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector3 attackoffset = new Vector3(0f, 1f, 0.95f);
    public float attackoradio = 1f;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            anim.Play("Attack0");

            checkHit();
        }
            
    }
    void checkHit()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position +
            transform.right * attackoffset.x +
            transform.up * attackoffset.y +
            transform.forward * attackoffset.z,
            attackoradio, 1 << 6);
        if (hits.Length > 0) 
        {
            print("攻擊到：" + hits[0].name);
            Rigidbody enemy = hits[0].GetComponent<Rigidbody>();
            enemy.AddForce(new Vector3(0, 2, 1), ForceMode.Impulse);
        }            
        else
            print("沒有攻擊到目標");
        //return hits;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
        Gizmos.DrawSphere(
            transform.position +
            transform.right * attackoffset.x +
            transform.up * attackoffset.y +
            transform.forward * attackoffset.z,
            attackoradio);
    }
}
