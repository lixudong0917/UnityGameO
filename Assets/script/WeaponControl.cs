using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("物品拾取" + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name);
            Destroy(gameObject);
            //Debug.Log("物品拾取");
            //Debug.Log(collision.name);
            GameObject.Find("Player").GetComponent<PlayerControl>().weaponUse = true;

        }
    }
    
    //碰到怪物 玩家扣血
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Debug.Log("111111111111");
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        //Debug.Log("111111111111");
    //        GameObject.Find("Player").GetComponent<PlayerControl>().health--;
    //        Debug.Log(GameObject.Find("Player").GetComponent<PlayerControl>().health);
    //    }
    //}
}
