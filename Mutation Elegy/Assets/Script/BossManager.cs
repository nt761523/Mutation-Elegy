using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;
    //移動目標
    public Transform[] Targets;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }
    //隨機獲取一個座標
    public Transform GetRandomTarget()
    {
        int index = Random.Range(0, Targets.Length - 1);
        return Targets[index];
    }
}
