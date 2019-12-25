using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Entity : MonoBehaviour//实体类
{
    [HideInInspector]public Rigidbody2D _rigidbody2D;
    protected Collider2D _collider2D;
    protected Transform _transform;
    protected Animator _animator;
    protected Animator _containerAnimator;
    protected SpriteRenderer _spriteRenderer;
    protected TrailRenderer _trailRenderer;
    protected AudioSource _audioSource;

    public double Mass
    {
        get => _rigidbody2D.mass;
    }
    public Vector2 Speed
    {
        get => _rigidbody2D.velocity;
    }
    protected Tube life;
    public Tube Life { get=>life; }

    protected Tube state;
    public Tube State => state;
    protected abstract void Disappear();

    protected virtual void InitializeReferences()//初始化对象引用
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _containerAnimator = GetComponentInParent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }
    

    /*private void OnMouseDown()//测试
    {
        Debug.Log("my life is "+life.Now);
    }*/
}

public abstract class Bird : Entity
{
    protected Transform _anchorTransform;
    protected bool fired = false;
    protected bool hit = false;
    protected bool skillUsed = false;
    [HideInInspector]public bool haveJumpedToSling = false;
    [SerializeField] protected Sprite feather1;
    [SerializeField] protected Sprite feather2;
    [SerializeField] protected Sprite feather3;
    [SerializeField] protected Sprite myScorePic;
    [SerializeField] protected AudioClip fireSound;
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] private AudioClip skillSound = null;
    protected override void InitializeReferences()//初始化后取消鸟的模拟
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _containerAnimator = GetComponentsInParent<Animator>()[1];
        _anchorTransform = GetComponentsInParent<Transform>()[2];
        _trailRenderer = GetComponent<TrailRenderer>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _rigidbody2D.simulated = false;
    }
    public void Jump()
    {
        _containerAnimator.SetTrigger("Jump");
    }
    public void JumpAndRoll()
    {
        _containerAnimator.SetTrigger("JumpAndRoll");
    }
    protected IEnumerator Move33TimesIn330ms(Vector3 delta)
    {
        Vector3 positionBeforeJump = transform.position;
        for (int i = 0; i < 33; i++)
        {
            positionBeforeJump = positionBeforeJump + delta;
            transform.position = positionBeforeJump + new Vector3(0,(float)(-0.005*i * (i - 32)),0);
            transform.Rotate(0f,0f,-360/32f);//为什么是32次？
            yield return new WaitForSeconds(0.01f);
        }
        haveJumpedToSling = true;
    }
    IEnumerator Start()
    {
        InitializeReferences();
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(Random.Range(0.0f,2.0f));
        _animator.SetTrigger("delayEnds");
    }
    public virtual void JumpTo(Vector3 endPosition)//把上面那个函数挂进协程
    {
        _transform.parent = null;
        Vector3 beginPosition = _transform.position;
        Vector3 disPer10ms = (endPosition-beginPosition)/33;
        disPer10ms.z = 0;
        StartCoroutine(Move33TimesIn330ms(disPer10ms));
    }

    public virtual void Skill()
    {
    }

    public void Ani_OnSling()
    {
        _animator.SetTrigger("onSling");
    }
    public void Ani_Fly()
    {
        fired = true;
        _animator.SetBool("flying",true);
        _audioSource.PlayOneShot(fireSound,1);
    }
    public void Ani_HitGround()
    {
        _animator.SetBool("flying",false);
    }
    public void Ani_Disappear()
    {
        _animator.SetTrigger("disappear");
    }
    protected void OnCollisionEnter2D(Collision2D other)
    {
        _animator.SetBool("flying", false);
        if (!hit)
        {
            TrailScript.Instance.StopDrawing();
            hit = true;
            _audioSource.PlayOneShot(hitSound,1);
        }
    }
    protected void ImStillAlive()
    {
        LevelManagerScript.Instance.BirdExist();
    }
    protected virtual void DestroyMe() //由动画启动
    {
        SpecialEffectsManager.Instance.Feathers(transform.position,feather1,feather2,feather3);
        Destroy(gameObject);
    }
    public virtual void DeleteTrait()
    {
        TrailScript.Instance.Clear();
    }
    public void ShouMyScore()
    {
        SpecialEffectsManager.Instance.Score(transform.position+Vector3.up*0.5f, myScorePic);
        LevelManagerScript.Instance.ScoreAdd(10000);
    }

    protected float _highSpeedLastTime;
    protected virtual void Update()
    {
        if (fired)
        {
            if(_rigidbody2D.velocity.magnitude>0.5f)_highSpeedLastTime = Time.time;
            if(Time.time-_highSpeedLastTime>5) Ani_Disappear();
            //Debug.Log("time now"+Time.time+"low begin"+_highSpeedLastTime);
        }
        ImStillAlive();
        //Debug.Log("skill Used = "+skillUsed);
        if(!hit&&fired&&!skillUsed && Input.GetMouseButtonDown(0))
        {
            if(skillSound!=null)      _audioSource.PlayOneShot(skillSound);
            Skill();
            skillUsed = true;
        }
        if(transform.position.y<-20)     DestroyMe();
    }
}

public abstract class Block : Entity
{
    [SerializeField] protected List<Sprite> allPics; 
    protected AllBirdsFloat BirdSensitivity;
    [SerializeField] protected Sprite p1;
    [SerializeField] protected Sprite p2;
    [SerializeField] protected Sprite p3;
    [SerializeField] protected AudioClip[] destroySound;
    [SerializeField] protected AudioClip[] collisionSound;
    protected AudioClip UsedCollisionSound;
    protected AudioClip UsedDestroySound;
    [SerializeField]protected int settingLife = 0;
    

    protected void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D point  = other.GetContact(0);
        if(!_audioSource.isPlaying && _audioSource.enabled&&point.normalImpulse>0.5f)    _audioSource.PlayOneShot(UsedCollisionSound,0.3f);
        //Hurt((int) (point.normalImpulse*10) );
        switch (other.gameObject.tag)
        {
            case "RedBird":
                Hurt((int) (point.normalImpulse*10*BirdSensitivity.Red) );
                other.rigidbody.AddForce(point.otherRigidbody.velocity.normalized * point.normalImpulse*10*(BirdSensitivity.Red-1));
                break;
            case "BlueBird":
                Hurt((int) (point.normalImpulse*10*BirdSensitivity.Blue) );
                other.rigidbody.AddForce(point.otherRigidbody.velocity.normalized * point.normalImpulse*10*(BirdSensitivity.Blue-1));
                break;
            case "YellowBird":
                Hurt((int) (point.normalImpulse*10*BirdSensitivity.Yellow) );
                other.rigidbody.AddForce(point.otherRigidbody.velocity.normalized * point.normalImpulse*10*(BirdSensitivity.Yellow-1));
                break;
            case "BlackBird":
                Hurt((int) (point.normalImpulse*10*BirdSensitivity.Black) );
                other.rigidbody.AddForce(point.otherRigidbody.velocity.normalized * point.normalImpulse*10*(BirdSensitivity.Black-1));
                break;
            case "WhiteBird":
                Hurt((int) (point.normalImpulse*10*BirdSensitivity.White) );
                other.rigidbody.AddForce(point.otherRigidbody.velocity.normalized * point.normalImpulse*10*(BirdSensitivity.White-1));
                break;
            default:
                Hurt((int) (point.normalImpulse*5) );
                break;
        }
    }
    protected new void InitializeReferences()//初始化对象引用
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _containerAnimator = GetComponentInParent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        UsedCollisionSound = collisionSound[Random.Range(0, collisionSound.Length - 1)];
        UsedDestroySound = destroySound[Random.Range(0, destroySound.Length - 1)];
    }
    public void Hurt(int damage)
    {
        if (damage <= 3) return;
        life.Now -= damage;
        if (life.Now <= 0) Disappear();
        state.Now = life.Now / (life.Full / state.Full) + 1;
        if (state.Now < 1) state.Now = 1;
        if (state.Now > state.Full) return;
        _spriteRenderer.sprite = allPics[state.Now];

        LevelManagerScript.Instance.ScoreAdd(6*damage);
        
    }

    private void ImStillMoving()
    {
        LevelManagerScript.Instance.BlockMove();
    }

    public Block()
    {
        BirdSensitivity.Black = 1;
        BirdSensitivity.Blue = 1;
        BirdSensitivity.Green = 1;
        BirdSensitivity.Red = 1;
        BirdSensitivity.White = 1;
        BirdSensitivity.Yellow = 1;
    }

    private void Update()
    {
        if (_rigidbody2D.velocity.magnitude >= 0.1f)
        {
            ImStillMoving();
        }
    }

    /// <summary>
    /// 初始化变量的值
    /// </summary>
    protected void InitializeValue()
    {
        state.Full = allPics.Count-1;
        state.Now = state.Full;
        life.Full = settingLife;
        life.Now = settingLife;
    }
    
    protected override void Disappear()
    {
        SpecialEffectsManager.Instance.BlockPieces(transform.position, p1, p2, p3,UsedDestroySound);
        Destroy(gameObject);

    }

    private void Start()
    {
        InitializeReferences();
        InitializeValue();
    }
}

public abstract class Pig : Block
{
    [SerializeField] private Sprite myScorePic = null;
    [SerializeField] private int myScore = 0;
     /// <summary>
    /// 像gamemanager汇报这只猪还活着
    /// </summary>
    protected void ImStillAlive()
    {
        LevelManagerScript.Instance.PigExist();
    }
     private void Update()
    {
        ImStillAlive();
    }
     protected new void OnCollisionEnter2D(Collision2D other)
     {
         ContactPoint2D point = other.GetContact(0);
         if(!_audioSource.isPlaying&&point.normalImpulse>0.5f)    _audioSource.PlayOneShot(UsedCollisionSound);
         Hurt((int) (point.normalImpulse*10) );
     }

    protected override void Disappear()
    {
        
        SpecialEffectsManager.Instance.BlockPieces(transform.position, p1, p2, p3,UsedDestroySound);
        SpecialEffectsManager.Instance.Score(transform.position,myScorePic);
        LevelManagerScript.Instance.ScoreAdd(myScore);
        Destroy(gameObject);
    }
}

public struct Tube//试管型，储存满状态和当前状态
{
    public int Full;//满状态是多少？（这由图片数量决定）
    public int Now;//当前状态
}

public struct AllBirdsFloat
{
    public float Yellow;
    public float Red;
    public float Black;
    public float Blue;
    public float White;
    public float Green;
}


