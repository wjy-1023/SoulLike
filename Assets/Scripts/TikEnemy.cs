using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TikEnemy : MonoBehaviour
{
    Transform Enemy;
    Collider rFoot;

    private void Awake()
    {
        rFoot = GameObject.FindGameObjectWithTag("rFoot").GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider Collider)
    {
        if (Collider.gameObject.tag == "EnemyBody")
        {
            Enemy = Collider.transform.parent.root;

            Enemy.SendMessage("getTik");
        }
    }

}
