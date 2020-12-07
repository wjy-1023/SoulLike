using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnightControl : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator KnightAnimator;
    public GameObject Knight;
    public GameObject Enemy;
    private Collider Sword;
    private Coroutine Cd;
    private Coroutine valueRec;
    private Coroutine swordCd;

    //private Rigidbody KnightBody;

    public float KnightMaxHp = 1000.0f;
    public float CurrentHp = 100.0f;
    public float pValue = 100.0f;
    public float currentValue;
    //float distance;

    public Image HealthBar;
    public Image HealthBarClean;
    public Image value;
    public BoxCollider SwordCol;

    public float WalkSpeed;
    public float RunSpeed;
    public float BackSpeed;
    float rotationSpeed = 70.0f;

    public bool IfAtk = false;
    public bool IfDef = false;
    public bool isMove = true;
    public bool isRun = true;
    public bool Up = true;
    
    void Start()
    {
        Knight = GameObject.FindGameObjectWithTag("Knight");
        //Enemy = GameObject.FindGameObjectWithTag("Enemy");
        KnightAnimator = Knight.GetComponent<Animator>();
        Sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Collider>();
        //KnightBody = Knight.GetComponent<Rigidbody>();

        CurrentHp = KnightMaxHp;
        currentValue = pValue;
        

        //StartCoroutine(Countdown());
    }
    //IEnumerator Countdown()
    //{
    //    for (floattimer = 3; timer >= 0; timer -= Time.deltaTime)
    //        Yield return 0;
    //    Debug.Log("This message appears after 3 seconds!");
    //    yield return new WaitForSeconds(3);
    //}

    // Update is called once per frame
    void Update()
    {
        //if (Enemy)
        //{
        //    float distance = Vector3.Distance(Knight.transform.position, Enemy.transform.position);
        //}
        
        HealthBar.fillAmount = CurrentHp / KnightMaxHp;
        value.fillAmount = currentValue / pValue;

        if (IfDef && currentValue <= 0)
        {
            KnightAnimator.SetBool("dtoi", false);

            //CurrentHp -= 200.0f;
        }

        //体力测试
        ValueUp();

        if(isRun == true && IfAtk == false)
        {
            if(valueRec == null)
            {
                valueRec = StartCoroutine(valueCd());
            }
        }

        //if (Input.GetKey(KeyCode.G))
        //{
        //    Up = false;
        //    currentValue -= 1.0f;

        //}
        //if (Input.GetKeyUp(KeyCode.G))
        //{
        //    Up = true;
        //}


        if (HealthBarClean.fillAmount > HealthBar.fillAmount)
        {
            HealthBarClean.fillAmount -= 0.003f;
        }
        else
        {
            HealthBarClean.fillAmount = HealthBar.fillAmount;
        }

        //骑士走路移动部分
        if (Input.GetKey(KeyCode.W) && IfAtk == false  && isMove == true)
        {
            KnightAnimator.SetBool("itow", true);

            transform.Translate(Vector3.forward * Time.deltaTime * WalkSpeed);

            /*if (IfAtk = false)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * WalkSpeed);
            }*/
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            KnightAnimator.SetBool("itow", false);
        }
        if (Input.GetKey(KeyCode.S) && IfAtk == false  && isMove == true)
        {
            KnightAnimator.SetBool("itob", true);

            transform.Translate(Vector3.back * Time.deltaTime * BackSpeed);

            /*if (IfAtk = false)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * WalkSpeed);
            }*/
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            KnightAnimator.SetBool("itob", false);
        }

        //骑士转向部分
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        //骑士跑动移动部分
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && IfAtk == false && isMove == true && isRun == true && currentValue >0)
        {
            KnightAnimator.SetBool("wtor", true);

            transform.Translate(Vector3.forward * Time.deltaTime * RunSpeed);

            Up = false;

            currentValue -= 0.1f;

            if(currentValue <= 0)
            {
                isMove = false;
                isRun = false;

                KnightAnimator.SetBool("itow", false);
                KnightAnimator.SetBool("wtor", false);
                KnightAnimator.Play("idle break 1");

                if (Cd == null)
                {
                    Cd = StartCoroutine(Countdown());
                }

                if (valueRec == null)
                {
                    valueRec = StartCoroutine(valueCd());
                }

            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Up = false;

            KnightAnimator.SetBool("wtor", false);

            if (valueRec == null)
            {
                valueRec = StartCoroutine(valueCd());
            }


            //isMove = true;
            //isRun = false;
            //StartCoroutine(Countdown());
        }
        if ((KnightAnimator.GetBool("wtor") == true && Input.GetKeyUp(KeyCode.W)))
        {
            Up = false;

            KnightAnimator.SetBool("wtor", false);
            KnightAnimator.SetBool("itow", false);
            //isMove = false;
            //isRun = false;
            //StartCoroutine(Countdown());
            if (valueRec == null)
            {
                valueRec = StartCoroutine(valueCd());
            }
        }

        //骑士攻防部分
        if (Input.GetMouseButtonDown(0) && IfAtk == false && IfDef == false && currentValue > 0)
        {
            KnightAnimator.Play("attack");

            //Sword.enabled = false;
            
            IfAtk = true;
            //KnightAnimator.SetBool("itow", false);
            //KnightAnimator.SetBool("wtor", false);

            if (currentValue <= 0)
            {
                isMove = false;
                isRun = false;

                KnightAnimator.SetBool("itow", false);
                KnightAnimator.SetBool("wtor", false);
                //KnightAnimator.Play("idle break 1");

                if (Cd == null)
                {
                    Cd = StartCoroutine(Countdown());
                }

            }

            //体力测试
            Up = false;
            

        }
        if (Input.GetMouseButtonUp(0))
        {

            IfAtk = false;
            if (Cd == null)
            {
                Cd = StartCoroutine(Countdown());
            }
            
            //GameObject.FindGameObjectWithTag("Sword").SendMessage("showSword");
            //StartCoroutine(AtkCountdown());

            
            //KnightAnimator.SetBool("itow", false);
            //KnightAnimator.SetBool("wtor", false);
        }
        if (Input.GetKeyDown(KeyCode.F) && IfAtk == false && IfDef == false)
        {
            KnightAnimator.Play("attack2");

            IfAtk = true;

            isMove = false;

            Up = false;
            //valueTik();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            IfAtk = false;

            if (valueRec == null)
            {
                valueRec = StartCoroutine(valueCd());
            }

            if (Cd == null)
            {
                Cd = StartCoroutine(Countdown());
            }

        }
        if (Input.GetMouseButtonDown(1) && currentValue >0)
        {
            IfDef = true;
            //KnightAnimator.Play("defense");
            KnightAnimator.SetBool("dtoi", true);
            //KnightAnimator.Play("defense");
            //KnightAnimator.SetBool("itow", false);
            //KnightAnimator.SetBool("wtor", false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            KnightAnimator.SetBool("dtoi", false);
            IfDef = false;
            //KnightAnimator.Play("idle");
            //KnightAnimator.Play("defense");
            //KnightAnimator.SetBool("itow", false);
            //KnightAnimator.SetBool("wtor", false);
        }

        ////骑士跳跃部分
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    KnightAnimator.Play("jump");
        //}
    }
    public void ValueUp()
    {
        if (currentValue < 100 && Up == true && IfDef)
        {
            //Debug.Log("def valueup");
            currentValue += 0.08f;
        }
        else if (currentValue < 100 && Up == true)
        {
            //Debug.Log("valueup");
            currentValue += 0.3f;
        }
        
    }

    public void valueDis()
    {
        currentValue -= 20.0f;
    }

    public void valueTik()
    {
        currentValue -= 10.0f;
    }

    //public void Tik()
    //{
        
    //}
    //public void KnightDef()
    //{
        
    //}
    public void showSword()
    {
        SwordCol.enabled = true;
        //Debug.Log(Sword.enabled);
        if (swordCd == null)
        {
            swordCd = StartCoroutine(AtkCountdown());
        }
    }
     
    public void Hurt()
    {
        

        if (!IfDef)
        {
            if (CurrentHp <= 0)
            {
                return;
            }
            KnightAnimator.Play("hurt");
            //KnightAnimator.SetBool("htoi", false);

            CurrentHp -= 200.0f;

            /*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
            {
                HealthBarClean.fillAmount -= 200.0f;
            }
            else
            {
                HealthBarClean.fillAmount = HealthBar.fillAmount;
            }*/

            if (CurrentHp <= 0)
            {
                KnightAnimator.Play("death");
            }
            Debug.Log("currentHp is " + CurrentHp);
        }

        if (IfDef && currentValue >0)
        {
            Up = false;

            if (valueRec == null)
            {
                valueRec = StartCoroutine(valueCd());
            }

            if (CurrentHp <= 0 || currentValue <=0)
            {
                KnightAnimator.SetBool("dtoi", false);
                Debug.Log(KnightAnimator.GetBool("dtoi"));
                return;
            }
            //KnightAnimator.SetBool("htoi", false);
            currentValue -= 20.0f;

            Debug.Log("currentValue is " + currentValue);
            /*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
            {
                HealthBarClean.fillAmount -= 200.0f;
            }
            else
            {
                HealthBarClean.fillAmount = HealthBar.fillAmount;
            }*/

            if (CurrentHp <= 0)
            {
                KnightAnimator.Play("death");
            }
            Debug.Log("currentHp is " + CurrentHp);
        }

        

    }

    public void littleHurt()
    {
        
        if (!IfDef)
        {
            if (CurrentHp <= 0)
            {
                return;
            }
            KnightAnimator.Play("hurt");
            //KnightAnimator.SetBool("htoi", false);

            CurrentHp -= 50.0f;

            /*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
            {
                HealthBarClean.fillAmount -= 200.0f;
            }
            else
            {
                HealthBarClean.fillAmount = HealthBar.fillAmount;
            }*/


            if (CurrentHp <= 0)
            {
                KnightAnimator.Play("death");
            }
            Debug.Log("currentHp is " + CurrentHp);
        }
        if (IfDef && currentValue > 0)
        {
            Up = false;

            if (valueRec == null)
            {
                valueRec = StartCoroutine(valueCd());
            }
            if (CurrentHp <= 0 || currentValue <= 0)
            {
                KnightAnimator.SetBool("dtoi", false);
                Debug.Log(KnightAnimator.GetBool("dtoi"));
                return;
            }
            //KnightAnimator.SetBool("htoi", false);
            currentValue -= 10.0f;
            Debug.Log("currentValue is " + currentValue);
            /*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
            {
                HealthBarClean.fillAmount -= 200.0f;
            }
            else
            {
                HealthBarClean.fillAmount = HealthBar.fillAmount;
            }*/

            if (CurrentHp <= 0)
            {
                KnightAnimator.Play("death");
            }
            Debug.Log("currentHp is " + CurrentHp);
        }

    }

    public void largeHurt()
    {
        
        if (!IfDef)
        {
            if (CurrentHp <= 0)
            {
                return;
            }
            KnightAnimator.Play("hurt");
            //KnightAnimator.SetBool("htoi", false);

            CurrentHp -= 500.0f;

            /*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
            {
                HealthBarClean.fillAmount -= 200.0f;
            }
            else
            {
                HealthBarClean.fillAmount = HealthBar.fillAmount;
            }*/

            if (CurrentHp <= 0)
            {
                KnightAnimator.Play("death");
            }
            Debug.Log("currentHp is " + CurrentHp);
        }

        if (IfDef && currentValue > 0)
        {
            Up = false;

            if (valueRec == null)
            {
                valueRec = StartCoroutine(valueCd());
            }
            if (CurrentHp <= 0 || currentValue <= 0)
            {
                KnightAnimator.SetBool("dtoi", false);
                Debug.Log(KnightAnimator.GetBool("dtoi"));
                return;
            }
            //KnightAnimator.SetBool("htoi", false);
            currentValue -= 40.0f;
            Debug.Log("currentValue is " + currentValue);
            /*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
            {
                HealthBarClean.fillAmount -= 200.0f;
            }
            else
            {
                HealthBarClean.fillAmount = HealthBar.fillAmount;
            }*/

            if (CurrentHp <= 0)
            {
                KnightAnimator.Play("death");
            }
            Debug.Log("currentHp is " + CurrentHp);
        }

    }
    //public void getHurt()
    //{

    //        KnightAnimator.Play("hurt");
    //        //KnightAnimator.SetBool("htoi", false);

    //        CurrentHp -= 200.0f;

    //        /*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
    //        {
    //            HealthBarClean.fillAmount -= 200.0f;
    //        }
    //        else
    //        {
    //            HealthBarClean.fillAmount = HealthBar.fillAmount;
    //        }*/

    //        if (CurrentHp <= 0)
    //        {
    //            KnightAnimator.Play("death");
    //        }
    //        Debug.Log("currentHp is " + CurrentHp);

    //}





    IEnumerator Countdown()
    {
        //if (isRun == true)
        //{
        //    IfAtk = false;
        //}
        float timer = 2.5f;
        while (true)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = 2.5f;
                ///DO what you did
                if (isMove == false)
                {
                    isMove = true;
                }

                if (isRun == false)
                {
                    isRun = true;
                }

                KnightAnimator.SetBool("break", false);

                //Up = true;
            }
            yield return null;
        }
        
        

        

        //Sword.enabled = false;

       
    }

    IEnumerator valueCd()
    {
        //if (isRun == true)
        //{
        //    IfAtk = false;
        //}
        float timer = 3.0f;
        while (true)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = 3.0f;
                ///DO what you did
                //KnightAnimator.SetBool("break", false);

                Up = true;
            }
            yield return null;
        }





        //Sword.enabled = false;


    }

    IEnumerator AtkCountdown()
    {
        //for (float timer = 0.3f; timer >= 0; timer -= Time.deltaTime)
        //    yield return 0;
        float cdTime = 0.3f;
        //Sword.enabled = false;
        while (true)
        {
            cdTime -= Time.deltaTime;

            if (cdTime < 0)
            {
                cdTime = 0.3f;
                ///DO what you did
                SwordCol.enabled = false;
                //Debug.Log(Sword.enabled);
            }
            yield return null;
        }

    }

    //IEnumerator AtkCountdown()
    //{
    //    for (float timer = 1.5f; timer >= 0; timer -= Time.deltaTime)
    //        yield return 0;

    //    IfAtk = false;

    ////    Debug.Log(IfAtk);

    //}
}
