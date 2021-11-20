using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Rigidbody rig;
    public Animator anim;
    public Transform model;
    public float runspeed = 5;
    public float turnspeed = 5;

    Vector3 inputdir;
    bool lockinput = false;

    float h;
    float v;

    Camera m_MainCamera;

    Vector3 newPosition;

    public Vector3 attackoffset = new Vector3(0f, 1f, 0.95f);
    public float attackoradio = 1f;

    public CameraShake cameraShake;

    float currentWeight;
    float lerpTarget;
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        anim = gameObject.GetComponentInChildren<Animator>();
        //model = GameObject.Find("YBot").GetComponent<Transform>();
        model = GameObject.Find("Eagle").GetComponent<Transform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //inputdir = new Vector3(h, 0f, v).normalized;

        inputdir = Camera.main.transform.forward * v + Camera.main.transform.right * h;
        inputdir.y = 0;
        inputdir.Normalize();
        if (lockinput)
        {
            inputdir = Vector3.zero;
        }

        anim.SetBool("Run", (h != 0 || v != 0 ? true : false));

        //if(h != 0 || v != 0) 
        //{
        //    model.transform.forward = inputdir;
        //}


        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    anim.SetTrigger("Evade");
        //}


        //Camera.main.transform.forward
        //Camera.main.transform.eulerAngles.y;

        //if (Input.GetKey(KeyCode.W))
        //{
        //    var cameraDirection = Camera.main.transform.forward;
        //    cameraDirection.y = 0.0f;
        //    cameraDirection.Normalize();
        //    GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + cameraDirection * runspeed * Time.deltaTime);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            checkHit();

        }

        

    }
    private void FixedUpdate()
    {
        if(inputdir == Vector3.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(inputdir);
        targetRotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            360 * Time.fixedDeltaTime);

        //Vector3 pos = rig.position;
        //pos.x = Mathf.Clamp(pos.x, 15, 30);
        //pos.z = Mathf.Clamp(pos.z, 10, 20);

        rig.MovePosition(rig.position + inputdir * runspeed * Time.fixedDeltaTime);
        rig.MoveRotation(targetRotation);
    }


    /// <summary>
    /// Animator 使用
    /// </summary>
    public void OnAttack1Enter()
    {                      
        lockinput = true;
        lerpTarget = 1.0f;
    }
    public void OnAttack1Update()
    {
        currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.3f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);
    }
    public void OnAttackIdle()
    {
        lerpTarget = 0.0f;
        lockinput = false;
    }
    public void OnAttackIdleUpdate()
    {
        currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("Attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.3f);

        anim.SetLayerWeight(anim.GetLayerIndex("Attack"), currentWeight);
    }
    /// <summary>
    /// 攻擊範圍繪製
    /// </summary>
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
            Collider other = hits[0].GetComponent<Collider>();
            enemy.AddForce(new Vector3(0, 2, 1), ForceMode.Impulse);            
            Vector3 dispalyLocation = Camera.main.WorldToScreenPoint(enemy.transform.position + Vector3.up * 0.8f);
            //計算傷害&顯示
            GameObject.Find("UICanvas").GetComponent<UIsetting>().Generate_DamageText(dispalyLocation, 2, other);

            //StartCoroutine(cameraShake.Shake());
        }
        else
            print("沒有攻擊到目標");
        //return hits;
    }
}
