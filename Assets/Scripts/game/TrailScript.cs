using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    public static TrailScript Instance;
    private TrailRenderer _trailRenderer;
    private IEnumerator _drawCoroutine;
    private Vector3[] _positions;
    private IEnumerator Draw(Transform toFollow)
    {
        transform.position = toFollow.position;
        _trailRenderer.Clear();
        _trailRenderer.emitting = true;
        while (true)
        {
            transform.position = toFollow.position;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void StartDrawing(Transform toFollow)
    {
        _drawCoroutine = Draw(toFollow);
        StartCoroutine(_drawCoroutine);
    }

    public void StopDrawing()
    {
        StopCoroutine(_drawCoroutine);
        _trailRenderer.emitting = false;
        _trailRenderer.GetPositions(_positions);
        int finalVec = _trailRenderer.positionCount;
        /*for (int i = 0; i < 1000; i++)
            if (_positions[i] == Vector3.zero)
            {
                finalVec = i - 5;
                break;
            }
        Vector3[] goodPositions = new Vector3[finalVec];
        for (int i = 0; i < finalVec; i++)
        {
            goodPositions[i] = _positions[i];
        }
        _trailRenderer.SetPositions(goodPositions);*/
        
        /*Debug.Log(finalVec);
        Vector3 pre3 = _trailRenderer.GetPosition(finalVec - 10);
        _trailRenderer.SetPosition(finalVec-1,pre3);
        _trailRenderer.SetPosition(finalVec-2,pre3);*/
        
        /*for (int i = 0; i < 100; i++)
            Debug.Log(_positions[i]);
        _trailRenderer.GetPositions(_positions);
        for (int i = 0; i < 100; i++)
            Debug.Log(_positions[i]);*/
    }

    public void Clear()
    {
        _trailRenderer.Clear();
    }

    private void Start()
    {
        Instance = this;
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.emitting = false;
        _positions = new Vector3[10000];
    }
}
