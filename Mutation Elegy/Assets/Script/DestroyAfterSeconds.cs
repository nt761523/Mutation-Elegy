using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float destroyTime = 0.5f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}