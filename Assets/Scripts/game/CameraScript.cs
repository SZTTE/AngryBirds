using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    private Camera _camera;
    private BoxCollider2D _collider;
    public Vector3 MousePosition
    {
        get=> _camera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void Start()
    {
        _camera = GetComponent<Camera>();
        _collider = GetComponent<BoxCollider2D>();
        _targetSize = _camera.orthographicSize;
        preTouchDistance = 0;

    }

    private float _targetSize;
    private float scroll = 0;
    private float preTouchDistance;
    void Update()
    {
        //以下是鼠标控制的版本
        //float scroll = Input.mouseScrollDelta.y;//前一帧鼠标滚轮的滑动两

        if (Input.touchCount == 2)
        {
            if(preTouchDistance==0) preTouchDistance = (Input.touches[0].position - Input.touches[1].position).magnitude;
            float touchDistance = (Input.touches[0].position - Input.touches[1].position).magnitude;
            scroll = touchDistance - preTouchDistance;
            preTouchDistance = touchDistance;
        }
        else
        {
            preTouchDistance = 0;
        }

        if (scroll!=0)
        {
            _targetSize = _camera.orthographicSize;
            _targetSize -= scroll/10;
        }
        
        
        if (_camera.orthographicSize <= 3.8 && _targetSize<_camera.orthographicSize)//如果镜头已经太窄了，并且趋势是继续缩窄，那么就放弃目标大小
        {
            _targetSize = _camera.orthographicSize;
        }
        else if (_camera.orthographicSize >= 7 && _targetSize>_camera.orthographicSize && LevelManagerScript.Instance.level!=24)//要是镜头太宽了（二十四关除外）
        {
            _targetSize = _camera.orthographicSize;
        }
        else if (_camera.orthographicSize < _targetSize-0.5f)
        {
            _camera.orthographicSize *= 1.01f;
            _collider.size *= 1.01f;
        }
        else if(_camera.orthographicSize > _targetSize+0.5f)
        {
            _camera.orthographicSize *= 0.99f;
            _collider.size *= 0.99f;
        }

        //_camera.orthographicSize += (_targetSize - _camera.orthographicSize) * 0.05f;
    }

    private Vector3 _prePosition;
    private Vector3 _nowPosition;

    private void OnMouseDown()
    {
        _prePosition = Input.mousePosition;
    }

    void OnMouseDrag()
    {
        _nowPosition = Input.mousePosition;
        transform.position -= new Vector3(_nowPosition.x - _prePosition.x, 0, 0)*0.01f;
        _prePosition = Input.mousePosition;
    }
}
