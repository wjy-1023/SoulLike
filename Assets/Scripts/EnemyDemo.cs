using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyDemo : MonoBehaviour
{
    //public enum EnemyCtr
    //{
    //    idle,
    //    run,
    //    attack,
    //    death
    //}
    private Coroutine atkRolRoutine;

    public Image Blood;
    public Image bloodLose;
    

    public float EnemyHp = 200.0f;
    public float CurrentHp;
    
    //public EnemyCtr CurrentState = EnemyCtr.idle;

    Transform m_transform;
    Vector3 atkVec;
    bool isDo = true;
    float backSpeed = 5.0f;
    //角色移动速度
    float m_moveSpeed = 0.05f;
    //角色旋转速度
    float m_rotSpeed = 120;
    float m_timer = 2;
    float distance;

    //private bool isMove = true;

    private Animator EnemyAni;
    private NavMeshAgent agent;
    private GameObject player;
    private GameObject Enemy;
    private Transform EnemyTrans;
    private Transform KnightTrans;
    private Collider EnemyBody;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    private string collidername = "BoxCollider";
    void Start()
    {
        //EnemyAni = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animation>();

        //agent = GameObject.FindGameObjectWithTag("Enemy").GetComponent<NavMeshAgent>();
        //player = GameObject.FindWithTag("Knight");
        //Enemy = GameObject.FindGameObjectWithTag("Enemy");
        //m_transform = GameObject.FindGameObjectWithTag("Enemy").transform;
        //EnemyBody = GameObject.FindGameObjectWithTag("EnemyBody").GetComponent<Collider>();
        //EnemyTrans = GameObject.FindWithTag("Enemy").GetComponent<Transform>();
        //disVec = new Vector3(0,0,-1);
        player = GameObject.FindWithTag("Knight");
        Enemy = this.gameObject;
        m_transform = this.transform;
        EnemyBody = this.transform.Find(collidername).GetComponent<BoxCollider>();
        EnemyAni = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        CurrentHp = EnemyHp;

    }

    // Update is called once per frame
    void Update()
    {

        


        Blood.fillAmount = CurrentHp / EnemyHp;
        

        if (bloodLose.fillAmount > Blood.fillAmount)
        {
            bloodLose.fillAmount -= 0.003f;
        }
        else
        {
            bloodLose.fillAmount = Blood.fillAmount;
        }

        //distance = Vector3.Distance(player.transform.position, EnemyTrans.position);

        //Quaternion beginAngle = transform.rotation;

        if (player && Enemy)
        {
           distance = Vector3.Distance(player.transform.position, m_transform.position);
        }
        

        
        //设定移动至主角附近的最小值
        //Transform atkTrans = GameObject.FindGameObjectWithTag("Knight").transform;

        if (distance > 20)
        {
            EnemyAni.SetBool("itow", false);
            EnemyAni.SetBool("itor", false);
            //agent.isStopped = true;
        }

        if (distance <= 20 && distance > 15)
        {
            //EnemyAni.Play("idle");
            EnemyAni.SetBool("itow", false);
            EnemyAni.SetBool("itor", false);
            rotate();
            agent.isStopped = true;
            //Debug.Log("rotate");
        }

        if (distance <= 15 && distance > 10)
        {
            EnemyAni.SetBool("itow", false);
            EnemyAni.SetBool("itor", true);
            agent.isStopped = false;
            //Debug.Log("run");
            runMove();
            //StartCoroutine(WalkRollDemo());

            rotateAtk();
            //Enemy.transform.Translate(Vector3.forward * Time.deltaTime * 4.0f);
            //agent.SetDestination(player.transform.position);
            ////    //定义敌人的移动量
            //float speed = m_moveSpeed * Time.deltaTime;

            ////    //通过寻路组件的Move()方法实现寻路移动
            //agent.Move(m_transform.TransformDirection(new Vector3(0, 0, speed)));

        }

        if (distance <= 10 && distance > 5)
        {
            EnemyAni.SetBool("itow", true);
            EnemyAni.SetBool("itor", false);

            //Debug.Log("walk");
            Move();
            rotateAtk();
            
            //Enemy.transform.Translate(Vector3.forward * Time.deltaTime * 2.0f);
            //StartCoroutine(RunRollDemo());

            if(atkRolRoutine != null)
            {
                StopCoroutine(atkRolRoutine);
            }
            

        }
        if (distance <= 5 && distance > 2)
        {
            EnemyAni.SetBool("itow", true);
            EnemyAni.SetBool("itor", false);
            

            if (CurrentHp <= 0)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }

            isDo = true;
            
            Move();
            //Debug.Log("ready");
            //Enemy.transform.Translate(Vector3.forward * Time.deltaTime * 2.0f);

            //StartCoroutine(RunRollDemo());
            //Debug.Log(isDo);

        }
        
        if (distance <= 2 && isDo)
        {
            EnemyAni.SetBool("itow", false);
            EnemyAni.SetBool("itor", false);

            agent.isStopped = true;

            isDo = false;
            rotateAtk();
            //m_transform.LookAt(player.transform.position);
            //InvokeRepeating("attack", 3.0f, 5.0f);



            attack();





        }
       
    }

    void Move()
    {
        //float distance = Vector3.Distance(player.transform.position, transform.position);
        //var disVec = new Vector3(0, 0, -5);
        //atkVec = player.transform.position - disVec;
        //agent.SetDestination(atkVec);
        
        agent.SetDestination(player.transform.position);
        //    //定义敌人的移动量
        float speed = m_moveSpeed * Time.deltaTime;

        //    //通过寻路组件的Move()方法实现寻路移动
        agent.Move(m_transform.TransformDirection(new Vector3(0, 0, speed)));
        //agent.SetDestination(player.transform.position);
        ////定义敌人的移动量
        //float speed = m_moveSpeed * Time.deltaTime;

        ////通过寻路组件的Move()方法实现寻路移动
        //agent.Move(m_transform.TransformDirection(new Vector3(0, 0, speed)));
        //rotate();
    }

    void runMove()
    {
        //float distance = Vector3.Distance(player.transform.position, transform.position);
        //var disVec = new Vector3(0, 0, -5);
        //atkVec = player.transform.position - disVec;
        //agent.SetDestination(atkVec);

        agent.SetDestination(player.transform.position);
        //    //定义敌人的移动量
        float runSpeed = m_moveSpeed * Time.deltaTime *2.0f;

        //    //通过寻路组件的Move()方法实现寻路移动
        agent.Move(m_transform.TransformDirection(new Vector3(0, 0, runSpeed)));
        //agent.SetDestination(player.transform.position);
        ////定义敌人的移动量
        //float speed = m_moveSpeed * Time.deltaTime;

        ////通过寻路组件的Move()方法实现寻路移动
        //agent.Move(m_transform.TransformDirection(new Vector3(0, 0, speed)));
        //rotate();
    }

    void rotate()
        {
        
        Vector3 playerVec = player.transform.position;
        //开始角度  敌人自身角度
        Quaternion beginAngle = transform.rotation;

            //需要旋转角度
            //通过 Quaternion.LookRotation 函数 传入 (玩家的位置) 和 (敌人的位置) 的差值 
            Quaternion needRotateAngle = Quaternion.LookRotation(playerVec - transform.position);

            transform.rotation = Quaternion.Slerp(beginAngle, needRotateAngle, 0.02f);
        //if (beginAngle != needRotateAngle)
        //{
        //    EnemyAni.Play("walk");
        //}
        //else
        //{
        //    EnemyAni.Play("idle");
        //}
        }
    void rotateAtk()
    {

        Vector3 playerVec = player.transform.position;
        //开始角度  敌人自身角度
        Quaternion beginAngle = transform.rotation;

        //需要旋转角度
        //通过 Quaternion.LookRotation 函数 传入 (玩家的位置) 和 (敌人的位置) 的差值 
        Quaternion needRotateAngle = Quaternion.LookRotation(playerVec - transform.position);

        transform.rotation = Quaternion.Slerp(beginAngle, needRotateAngle, 0.1f);
        //if (beginAngle != needRotateAngle)
        //{
        //    EnemyAni.Play("walk");
        //}
        //else
        //{
        //    EnemyAni.Play("idle");
        //}
    }
    void attack()
    {
        //Invoke("atkRoll", 2.0f);
        //EnemyAni.Play("standBite");
        if(atkRolRoutine == null)
        {
             atkRolRoutine = StartCoroutine(atkRoll());
        }
        

    }

    public void getHit()
    {

        
        if (EnemyHp <= 0)
        {
            return; 
        }

        EnemyAni.Play("getHit");
        CurrentHp = CurrentHp - 100.0f;
        
        

        if (CurrentHp <= 0)
        {
            EnemyAni.Play("death");

            agent.isStopped = true;

            if (atkRolRoutine != null)
            {
                StopCoroutine(atkRolRoutine);
            }

            Destroy(this.gameObject, 2.0f);
        }

        Debug.Log(CurrentHp);
    }

    public void getTik()
    {


        if (EnemyHp <= 0)
        {
            return;
        }
        if (distance <= 2.5)
        {
            EnemyAni.Play("getHit");
        }

        Debug.Log("Tik");
    }

    public void DamageOfAtk()
    {
        if (distance <= 2.5)
        {
            player.SendMessage("getHurt");
        }
    }

    public void DamageOfLittle()
    {
        if (distance <= 2.5)
        {
            player.SendMessage("getlittleHurt");
        }
    }

    public void DamageOfJump()
    {
        if (distance <= 2.5)
        {
            player.SendMessage("getlargeHurt");
        }
    }

    //IEnumerator RunRollDemo()
    //{
    //    for (float timer = 2; timer >= 0; timer -= Time.deltaTime)
    //        yield return 0;

    //    int enemyRunAtk = Random.Range(1, 5);

    //    EnemyAni.SetInteger("ratk", enemyRunAtk);
    //}

    //IEnumerator WalkRollDemo()
    //{
    //    for (float timer = 2; timer >= 0; timer -= Time.deltaTime)
    //        yield return 0;

    //    int enemyWalkAtk = Random.Range(1, 5);

    //    EnemyAni.SetInteger("ratk", enemyWalkAtk);
    //}

    //IEnumerator atkRoll()
    IEnumerator atkRoll()
        {
        
        float timer = 3.0f;
        //EnemyTrans = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
        //for (float timer = 3; timer >= 0; timer -= Time.deltaTime)
        //        yield return 0;
        while (true){
            timer -= Time.deltaTime;
            
            if (timer < 0)
            {
                timer = 3.0f;
            ///DO what you did
                int Roll = Random.Range(1, 5);
                if (Roll == 1){
                            //GameObject.Find("Knight").GetComponent<KnightControl>().getHurt();
                            //
                            EnemyAni.SetBool("atk", true);
                            EnemyAni.SetBool("combo", false);
                            EnemyAni.SetBool("jump", false);
                            StartCoroutine(Countdown());
                            Debug.Log("atking");
                    isDo = true;
                    //isDo = true;
                }
                if (Roll == 2)
                {
                    EnemyAni.SetBool("atk", false);
                    EnemyAni.SetBool("combo", true);
                    EnemyAni.SetBool("jump", false);

                    StartCoroutine(Countdown());
                    Debug.Log("comboing");
                    //EnemyAni.Play("idle");
                    isDo = true;
                }
                if (Roll == 3)
                {
                    EnemyAni.SetBool("jump", false);
                    EnemyAni.SetBool("atk", false);
                    EnemyAni.SetBool("combo", false);
                    Debug.Log("none");
                    //EnemyAni.Play("idle");
                    StartCoroutine(Countdown());
                    isDo = true;
                }
                if (Roll == 4)
                {
                    EnemyAni.SetBool("jump", true);
                    EnemyAni.SetBool("atk", false);
                    EnemyAni.SetBool("combo", false);
                    //EnemyAni.SetBool("back", true);
                    StartCoroutine(Countdown());
                    //EnemyAni.Play("idle");
                    Debug.Log("jump");

                    //EnemyTrans.Translate(Vector3.down*Time.deltaTime*backSpeed);
                    //Enemy.transform.Translate(Vector3.forward * Time.deltaTime * backSpeed);
                    //var disVec = new Vector3(0, 0, -2);
                    //atkVec = player.transform.position - disVec;
                    //transform.position = atkVec;
                    //Debug.Log(Time.deltaTime * backSpeed);
                    isDo = true;


                }
                
            }

            yield return null;

        }


    }
    IEnumerator Countdown()
    {
        //if (isRun == true)
        //{
        //    IfAtk = false;
        //}

        for (float time = 1; time >= 0; time -= Time.deltaTime)
            yield return 0;
        if(EnemyAni.GetBool("jump"))
        {
            EnemyAni.SetBool("jump", false);
        }
        if (EnemyAni.GetBool("combo"))
        {
            EnemyAni.SetBool("combo", false);
        }
        if (EnemyAni.GetBool("atk"))
        {
            EnemyAni.SetBool("atk", false);
        }
        //StartCoroutine(atkRoll());

    }
    //IEnumerator isMove()
    //{
    //    for (float timer = 2; timer >= 0; timer -= Time.deltaTime)
    //        yield return 0;

    //    agent.SetDestination(player.transform.position);
    //    //定义敌人的移动量
    //    float speed = m_moveSpeed * Time.deltaTime;

    //    //通过寻路组件的Move()方法实现寻路移动
    //    agent.Move(m_transform.TransformDirection(new Vector3(0, 0, speed)));
    //}

}
