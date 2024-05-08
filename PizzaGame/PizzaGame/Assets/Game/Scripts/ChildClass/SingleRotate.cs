using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingleRotate
{
    public Transform trans;
    public bool isClockWise;
    public float speedSeftRotate;
    private bool isWorking = true;


    public SingleRotate(Transform p_trans, float p_speed, bool p_isClockWise = true)
    {

        trans = p_trans;
        speedSeftRotate = p_speed;
        isClockWise = p_isClockWise;
    }
    public void Stop()
    {
        isWorking = false;
    }
    public void SetStopOrRun(Action onRun = null, Action onStop = null)
    {
        isWorking = !isWorking;
        if (isWorking)
        {
            onRun?.Invoke();
        }
        else
        {
            onStop?.Invoke();
        }
    }
    public void UpdateRotate(float tick)
    {
        if (!isWorking) return;
        if (isClockWise)
        {
            trans.Rotate(Vector3.forward * -speedSeftRotate * tick);
        }
        else
        {
            trans.Rotate(Vector3.forward * speedSeftRotate * tick);
        }

    }
}
