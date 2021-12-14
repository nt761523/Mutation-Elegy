using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour
{
    public CameraShake camerashake;
    public Controller controller;
    private bool iscollidermove;
    public int damage;

    void checkHit()
    {
        Collider[] hits = Physics.OverlapSphere(
        StaticVal.playerpos.position +
        StaticVal.playerpos.right * StaticVal.attackoffset.x +
        StaticVal.playerpos.up * StaticVal.attackoffset.y +
        StaticVal.playerpos.forward * StaticVal.attackoffset.z,
        StaticVal.attackoradio, 1 << 6);
        //Collider[] hits = Physics.OverlapSphere(StaticVal.attackRange, StaticVal.attackoradio, 1 << 6);
        //Collider[] hits = Physics.OverlapSphere(
        //    transform.position +
        //    transform.right * StaticVal.attackoffset.x +
        //    transform.up * StaticVal.attackoffset.y +
        //    transform.forward * StaticVal.attackoffset.z,
        //    StaticVal.attackoradio, 1 << 6);
        if (hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                print("攻擊到：" + hits[i].name);
                //Rigidbody enemy = hits[i].GetComponent<Rigidbody>();
                //Collider other = hits[0].GetComponent<Collider>();
                Animator animator = hits[i].GetComponent<Animator>();
                //enemy.AddForce(new Vector3(0, 1, -1), ForceMode.Impulse);
                animator.SetTrigger("Hurt");
                //Vector3 dispalyLocation = Camera.main.WorldToScreenPoint(animator.transform.position + Vector3.up * 0.8f);
                //計算傷害&顯示
                //GameObject.Find("UICanvas").GetComponent<UIsetting>().Generate_DamageText(dispalyLocation, 2, hits[i].GetComponent<Collider>());

                StartCoroutine(camerashake.Shake());
                hits[i].GetComponent<HurtSystem>().Hurt(damage);


            }

            //print("攻擊到：" + hits[0].name);
            //Rigidbody enemy = hits[0].GetComponent<Rigidbody>();
            ////Collider other = hits[0].GetComponent<Collider>();
            //Animator animator = enemy.GetComponent<Animator>();
            //enemy.AddForce(new Vector3(0,1, -1), ForceMode.Impulse);
            //animator.SetTrigger("Hurt");
            //Vector3 dispalyLocation = Camera.main.WorldToScreenPoint(enemy.transform.position + Vector3.up * 0.8f);
            ////計算傷害&顯示
            //GameObject.Find("UICanvas").GetComponent<UIsetting>().Generate_DamageText(dispalyLocation, 2, hits[0].GetComponent<Collider>());
        }
        else
            print("沒有攻擊到目標");
        //return hits;
    }
}
