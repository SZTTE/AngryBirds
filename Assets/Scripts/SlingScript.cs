using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;
public enum SlingState
{
    Stretched,Release
};
public class SlingScript : MonoBehaviour
{
    private Transform _transform;
    public List<Bird> birds;
    private Tube _birdNumber;//鸟的计数器，鸟的编号从零开始
    private SlingState _state;
    public HolderScript holder;
    public CameraScript camera;

    public SlingState State
    {
        get => _state;
    }

    //重新装填一只鸟
    public void Reload()
    {
        birds[_birdNumber.Now].JumpTo(_transform.position);
    }

    private void Fire(double angle, double intensity)//角度从0到2pi，强度从0到1
    {
        birds[_birdNumber.Now]._rigidbody2D.simulated = true;
        double speed = 15;
        Vector2 v = new Vector2( (float) (speed*Math.Cos(angle)*intensity) , (float) (speed*Math.Sin(angle)*intensity));
        birds[_birdNumber.Now]._rigidbody2D.velocity = v;
    }


    // Start is called before the first frame update
    void Start()
    {
        _birdNumber.Full = birds.Count-1;
        _birdNumber.Now = 0;
        _transform = GetComponent<Transform>();
        _state = SlingState.Release;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Reload();
        if (Input.GetMouseButtonDown(1)) Fire(Math.PI/900,1);
        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////拖拽控制
        if (_state == SlingState.Release && holder.draging) _state = SlingState.Stretched;
        if (_state == SlingState.Stretched && !holder.draging) _state = SlingState.Release;

        if (_state == SlingState.Stretched)//紧绷状态
        {
            //计算准备
            double _maxLength = 1;
            Vector3 targetPosition = camera.MousePosition+new Vector3(0,0,10);
            Vector3 relativePosition = transform.InverseTransformPoint(targetPosition);
            double theta = Math.Atan2(relativePosition.y,relativePosition.x);
            if (Math.Pow(relativePosition.x, 2) + Math.Pow(relativePosition.y, 2) >= Math.Pow(_maxLength,2))//要是超过距离
            {
                targetPosition = transform.TransformPoint(    new Vector3((float) (  _maxLength*Math.Cos(theta) ),(float)( _maxLength*Math.Sin(theta) ),10)    );
            }

            holder.transform.eulerAngles = new Vector3(0,0,(float) (theta/ (2*Math.PI) * 360 - 180) );

            holder.transform.position = targetPosition;

        }
    }
}
