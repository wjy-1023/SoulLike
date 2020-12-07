using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHurt : MonoBehaviour
{
    // Start is called before the first frame update
    //public float KnightMaxHp = 1000.0f;
    //public float CurrentHp = 100.0f;

    //public Image HealthBar;
    //public Image HealthBarClean;

    private Animator KnightAnimator;
    public GameObject Knight;

    void Start()
    {
        KnightAnimator = Knight.GetComponent<Animator>();
        //CurrentHp = KnightMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //HealthBar.fillAmount = CurrentHp / KnightMaxHp;

        //if (HealthBarClean.fillAmount > HealthBar.fillAmount)
        //{
        //    HealthBarClean.fillAmount -= 200.0f;
        //}
        //else
        //{
        //    HealthBarClean.fillAmount = HealthBar.fillAmount;
        //}
        //if (HealthBarClean.fillAmount > HealthBar.fillAmount)
        //{
        //    HealthBarClean.fillAmount -= 0.003f;
        //}
        //else
        //{
        //    HealthBarClean.fillAmount = HealthBar.fillAmount;
        //}
    }

    public void getHurt()
    {

        //KnightAnimator.Play("hurt");
        ////KnightAnimator.SetBool("htoi", false);

        //CurrentHp -= 200.0f;

        ///*if (HealthBarClean.fillAmount > HealthBar.fillAmount)
        //{
        //    HealthBarClean.fillAmount -= 200.0f;
        //}
        //else
        //{
        //    HealthBarClean.fillAmount = HealthBar.fillAmount;
        //}*/

        //if (CurrentHp <= 0)
        //{
        //    KnightAnimator.Play("death");
        //}
        //Debug.Log("currentHp is " + CurrentHp);


        GameObject.FindGameObjectWithTag("Knight").SendMessage("Hurt");

    }

    public void getlittleHurt()
    {

        GameObject.FindGameObjectWithTag("Knight").SendMessage("littleHurt");

    }

    public void getlargeHurt()
    {

        GameObject.FindGameObjectWithTag("Knight").SendMessage("largeHurt");

    }
}
