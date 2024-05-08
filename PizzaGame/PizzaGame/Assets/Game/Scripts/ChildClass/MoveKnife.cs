using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveKnife
{
    public Transform transform;
    public Sequence sequence;
    private Vector3 startPos;
    private Quaternion starRotate;

    public MoveKnife(Transform p_transform)
    {
        transform = p_transform;
        sequence = DOTween.Sequence();
        startPos = p_transform.localPosition;
        starRotate = p_transform.localRotation;
    }

    public void DoMoveTo(Vector3 localposMoveTo, Vector3 localrotateMoveTo, float time, Action onComplete = null)
    {
        sequence = DOTween.Sequence();
        sequence
            .Append(transform.DOLocalMove(localposMoveTo, time))
            .Insert(0, transform.DOLocalRotate(localrotateMoveTo, time))
            .OnKill(() =>
            {
                transform.localPosition = localposMoveTo; transform.localRotation = Quaternion.Euler(localrotateMoveTo);
                onComplete?.Invoke();
            })

            ;
    }

    public void SetPos()
    {
        transform.localPosition = startPos;
        transform.localRotation = starRotate;
    }
}
