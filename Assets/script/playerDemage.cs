using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerDemage : MonoBehaviour
{
    public bool canDemage;
    public int health;
    public Text healthText;

    public int playerDmgValue;
    public float beatBackForce; 
    public Transform hammerHead;
    public Transform enemyBody;
    private float maxRange = 2.0f;

    // Start is called before the first frame update

    private void Awake()
    {
        canDemage = true;
        health = 3;
        playerDmgValue = 1;
        beatBackForce = 10.0f;
    }
    void Start()
    {
        Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        Vector3 center =
            new Vector3(Screen.width / 2, Screen.height / 2, depth);
        Vector3 mouse =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);

        //转换成WordPoint
        center = Camera.main.ScreenToWorldPoint(center);
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        //计算鼠标向量
        Vector3 mouseVec = Vector3.ClampMagnitude(mouse - center, maxRange);

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useLayerMask = true;
        contactFilter.layerMask = LayerMask.GetMask("Enemy");
        //print(contactFilter.ToString());
        Collider2D[] results = new Collider2D[5];

        if (hammerHead.GetComponent<Rigidbody2D>().OverlapCollider(
        contactFilter, results) > 0)  //锤子碰撞器和怪物身体碰撞
        {
            // 击退怪物
            enemyBody = results[0].GetComponent<Transform>();

            Vector3 force = mouseVec * beatBackForce;
            enemyBody.GetComponent<Rigidbody2D>().AddForce(force);
            enemyBody.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(
                enemyBody.GetComponent<Rigidbody2D>().velocity, 6);
            results[0].GetComponent<enemyController>().DealDamage(playerDmgValue);
            //攻击音效
            gameObject.GetComponent<PlayerAudio>().Attack();
        }
    }
    private void FixedUpdate()
    {
        
    }

    public void DealDamage(int demageValue)
    {
        if (canDemage)
        {
            //收到攻击音效
            gameObject.GetComponent<PlayerAudio>().GetDemage();
            health -= demageValue;
            if (health > 0)
            {
                healthText.text = "Health:" + health;
                print(healthText.text);
            }
            else
            {
                StartCoroutine(EndGame());
                
            }
            canDemage = false; 
            StartCoroutine(WaitForDemage());
        }
    }

    private IEnumerator EndGame()
    {
        yield return new  WaitForSeconds(1f);
        Time.timeScale = 0f;
        SceneManager.LoadScene("TestSence");

    }

    private IEnumerator WaitForDemage()
    {
        yield return new WaitForSeconds(1f);
        canDemage = true;
    }
}
