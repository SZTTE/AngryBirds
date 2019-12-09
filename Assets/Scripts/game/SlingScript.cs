using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;

public enum SlingState
{
    Stretched,Release,Unloaded,OutOfBird,Loading,End
};
public class SlingScript : MonoBehaviour
{
    public static SlingScript Instance;
    private Transform _transform;
    public List<Bird> birds;
    private Tube _birdNumber;//鸟的计数器，鸟的编号从零开始
    private SlingState _state;
    public HolderScript holder;
    public new CameraScript camera;
    public LineRenderer lineBack;
    public LineRenderer lineForward;
    public Transform holderPicTransform;
    public SlingState State
    {
        get => _state;
    }
    private IEnumerator _reloadCor;
    SlingScript()
    {
        Instance = this;
    }



    /// <summary>
    /// 协程：装填等待中的鸟
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(3);
        birds[_birdNumber.Now].JumpTo(holder.transform.position);
        while (birds[_birdNumber.Now].haveJumpedToSling)//等待鸟跳上弹弓完成
            yield return new WaitForSeconds(0.1f);
        birds[_birdNumber.Now].transform.SetParent(holder.transform);
        birds[_birdNumber.Now].transform.eulerAngles = Vector3.zero;
        _state = SlingState.Release;
        birds[_birdNumber.Now].Ani_OnSling();
    }

    public IEnumerator ShowBirdsScore()
    {
        _state = SlingState.End;
        while (_birdNumber.Now <= _birdNumber.Full)
        {
            birds[_birdNumber.Now].ShouMyScore();
            birds[_birdNumber.Now].JumpAndRoll();
            yield return new WaitForSeconds(1.8f);
            _birdNumber.Now++;
        }
        SettlementCanvasScript.Instance.Appear(LevelManagerScript.Instance.Score);
    }

    /// <summary>
    /// 炮台开火
    /// </summary>
    /// <param name="angle">发射角，从0到2Pi</param>
    /// <param name="intensity">强度，从0到1</param>
    private void Fire(double angle, double intensity)//角度从0到2pi，强度从0到1
    {
        if (_birdNumber.Now != 0)
        {
            TrailScript.Instance.StopDrawing();
            birds[_birdNumber.Now-1].DeleteTrait();
        }
        
        TrailScript.Instance.StartDrawing(birds[_birdNumber.Now].transform);
        birds[_birdNumber.Now]._rigidbody2D.simulated = true;
        double speed = 15;
        Vector2 v = new Vector2( (float) (speed*Math.Cos(angle)*intensity) , (float) (speed*Math.Sin(angle)*intensity));
        birds[_birdNumber.Now]._rigidbody2D.velocity = v;
        _state = SlingState.Unloaded;
        birds[_birdNumber.Now].transform.parent = null;
        birds[_birdNumber.Now].Ani_Fly();

        if (_birdNumber.Now < _birdNumber.Full) _birdNumber.Now++;
        else _state = SlingState.OutOfBird;
    }

    /// <summary>
    /// 协程：地上的鸟随机跳跃
    /// </summary>
    private IEnumerator BirdsJumpRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            int n = UnityEngine.Random.Range(0, 10);
            //Debug.Log("state now is  " + _state);
            if (n >= _birdNumber.Now && n <= _birdNumber.Full) birds[n].JumpAndRoll();
            if (_state == SlingState.OutOfBird) yield break;
        }
    }

    void Start()
    {
        _birdNumber.Full = birds.Count-1;
        _birdNumber.Now = 0;
        _transform = GetComponent<Transform>();
        _state = SlingState.Unloaded;
        lineBack.alignment = LineAlignment.TransformZ;
        lineForward.alignment = LineAlignment.TransformZ;
        StartCoroutine(BirdsJumpRandomly());
    }

    private bool lineLongEnough = false;
    void Update()
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////拖拽控制
        if (_state == SlingState.Release && holder.draging) _state = SlingState.Stretched;
        if (_state == SlingState.Stretched && !holder.draging) _state = SlingState.Release;
        
        //线渲染器控制
        lineBack.SetPosition(1,holderPicTransform.position);
        lineForward.SetPosition(1,holderPicTransform.position);
        
        //计算准备
        double _maxLength = 1.2;
        double _minLengthToFire = 0.4;
        Vector3 targetPosition = camera.MousePosition+new Vector3(0,0,10);
        Vector3 relativePosition = transform.InverseTransformPoint(targetPosition);
        double theta = Math.Atan2(relativePosition.y,relativePosition.x);
        double lineLength = Math.Sqrt( Math.Pow(relativePosition.x, 2) + Math.Pow(relativePosition.y, 2) );
        if (lineLength > _maxLength) lineLength = _maxLength;

        switch (_state) {
        case SlingState.Stretched://紧绷状态
        {
            //设置transform
            targetPosition = transform.TransformPoint(    new Vector3((float) (  lineLength*Math.Cos(theta) ),(float)( lineLength*Math.Sin(theta) ),-101)    );
            holder.transform.eulerAngles = new Vector3(0,0,(float) (theta/ (2*Math.PI) * 360 - 180) );
            holder.transform.position = targetPosition;
            
            //判断线是否达到最小发射长度
            lineLongEnough = (lineLength > _minLengthToFire) ? true : false;
            break;
        }
        case SlingState.OutOfBird:
        {
            break;
        }
        case SlingState.Unloaded:
        {
            _reloadCor = Reload();
            StartCoroutine(_reloadCor);
            _state = SlingState.Loading;
            break;
        }
        case SlingState.Release:
        {
            holder.transform.localPosition = new Vector3(0,0,-101);
            holder.transform.eulerAngles = Vector3.zero;
            if (lineLongEnough)
            {
                Fire(theta - Math.PI, lineLength / _maxLength);
                lineLongEnough = false;
            }
            break;
        }
        case SlingState.End:
        {
            StopCoroutine(_reloadCor);
            break;
        }
        default:
        {
            

            break;
        }
        }
    }
}
