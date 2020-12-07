using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAtk : MonoBehaviour
{
    // Start is called before the first frame update
    private Collider Sword;
    private Coroutine cd;

    private GameObject player;
    private Transform enemy;

    float distance;
    float timer = 0.3f;

    private void Awake()
    {
        Sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Collider>();
    }
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Knight");
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

        //if (enemy)
        //{
        //    float distance = Vector3.Distance(player.transform.position, enemy.transform.position);
        //}
        
    }

    public void getAtk()
    {
        
            //if (distance <= 2.5)
            //{
            //    GameObject.FindGameObjectWithTag("Enemy").SendMessage("getHit");
            //    Debug.Log(distance);
            //}
        
        
    }
    //private void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log("atk to enemy");
    //    }
    //}

    public void OnTriggerEnter(Collider EnemyCollider)
    {
        //Sword.enabled = false;
        if (EnemyCollider.gameObject.tag == "EnemyBody")
        {
            enemy = EnemyCollider.gameObject.transform.parent;
            Debug.Log(enemy);
            //Sword.gameObject.SendMessage("getHit");
            //Debug.Log("atk to enemy");
            enemy.SendMessage("getHit");
        }
    }

    //public void showSword()
    //{
    //    Sword.enabled = true;
    //    //Debug.Log(Sword.enabled);
    //    if (cd == null)
    //    {
    //        cd = StartCoroutine(AtkCountdown());
    //    }
        
    //}

    

    //IEnumerator AtkCountdown()
    //{
    //    //for (float timer = 0.3f; timer >= 0; timer -= Time.deltaTime)
    //    //    yield return 0;
    //    //Sword.enabled = false;
    //    while (true)
    //    {
    //        timer -= Time.deltaTime;

    //        if (timer < 0)
    //        {
    //            timer = 0.3f;
    //            ///DO what you did
    //            Sword.enabled = false;
    //            //Debug.Log(Sword.enabled);
    //        }
    //        yield return null;
    //    }

    //}
}
