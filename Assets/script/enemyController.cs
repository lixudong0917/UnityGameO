using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed = 1.0f;
    public LayerMask layer;

    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private Transform playerPos;

    public bool canDemage;
    public int enemyHealth;
    public int enemyDmgValue = 1;
    // Start is called before the first frame update
    public Animator enemyAnimtor;

    private bool stopFlag;
    private void Awake()
    {
        canDemage = true;
        enemyHealth = 3;
        stopFlag = false;
    }
    void Start()
    {
        _rigidbody2D = GetComponentInChildren<Rigidbody2D>();
        _capsuleCollider2D = GetComponentInChildren<CapsuleCollider2D>();
        playerPos = GameObject.Find("Body").transform;
        enemyAnimtor = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //print("isColid"+isColide);
        //敌人移动
        if (!stopFlag)
        {
            float dist = Vector3.Distance(transform.position, playerPos.position);
            if (dist < 3.0f) //距离小于3时开始移动
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
                enemyAnimtor.SetInteger("Animation_Int", 1);
            }
            else
            {
                enemyAnimtor.SetInteger("Animation_Int", 0);
            }
        }
        Vector3 EandPVec = transform.position - playerPos.position;
        if(EandPVec.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //print(transform.position - playerPos.position);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            stopFlag = true;
            enemyAnimtor.SetInteger("Animation_Int", 2);
            GameObject.Find("Player").GetComponent<playerDemage>().DealDamage(enemyDmgValue);
            StartCoroutine(WaitForAttack());
        }
    }

    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(2f);
        speed = 1;
        enemyAnimtor.SetInteger("Animation_Int", 0);
        stopFlag = false;
    }

    public void DealDamage(int demageValue)
    {
        //print(canDemage);
        if (canDemage)
        {
            //print(demageValue);
            enemyHealth -= demageValue;
            if (enemyHealth > 0)
            {
                stopFlag = true;
                print("Goblin health:" + enemyHealth);
                enemyAnimtor.SetInteger("Animation_Int", 3);
                StartCoroutine(WaitOne());
            }
            else
            {
                stopFlag = true;
                enemyAnimtor.SetInteger("Animation_Int", 4);
                StartCoroutine(WaitForDie());
                //print("Goblin is die");
            }
            canDemage = false;
            StartCoroutine(WaitForDemage());
        }

    }

    private IEnumerator WaitOne()
    {
        yield return new WaitForSeconds(1f);
        stopFlag = false;
    }

    private IEnumerator WaitForDie()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private IEnumerator WaitForDemage()
    {
        yield return new WaitForSeconds(1f);
        canDemage = true;
    }
}
