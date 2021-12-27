using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockMove : MonoBehaviour
{
    public UnityEvent lockmove;
    public UnityEvent releasemove;
    private void OnEnable()
    {
        LockedControl();
    }
    private void OnDisable()
    {
        ReleaseControl();
    }
    public void LockedControl()
    {
        lockmove.Invoke();
    }
    public void ReleaseControl()
    {
        releasemove.Invoke();
    }
}
