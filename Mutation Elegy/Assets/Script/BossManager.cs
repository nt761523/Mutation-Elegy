using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;
    //���ʥؼ�
    public Transform[] Targets;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }
    //�H������@�Ӯy��
    public Transform GetRandomTarget()
    {
        int index = Random.Range(0, Targets.Length - 1);
        return Targets[index];
    }
}
