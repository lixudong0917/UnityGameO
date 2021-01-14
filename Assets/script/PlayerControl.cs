using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform hammerHead;
    public Transform body;
   

    public float maxRange = 2.0f;
    public float jump = 150.0f;

    private bool jumpState = true;
    private static GameObject redHammer;
    private static GameObject sword;

    public bool weaponUse = false;




    // Start is called before the first frame update
    void Start()
    {
        //忽略身体和🔨头部的碰撞
        Physics2D.IgnoreCollision(hammerHead.GetComponent<Collider2D>(),
                                  body.GetComponent<Collider2D>());
        redHammer = GameObject.Find("HammerSprite");
        sword = GameObject.Find("Sword");
        Debug.Log("sword " + sword);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        float depth = Mathf.Abs(Camera.main.transform.position.z);
        //屏幕中心坐标
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, depth);
        //鼠标当前坐标
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, depth);

        //转换为世界坐标系
        center = Camera.main.ScreenToWorldPoint(center);
        mouse = Camera.main.ScreenToWorldPoint(mouse);

        //限制🔨活动范围
        Vector3 mouseVec = Vector3.ClampMagnitude(mouse - center, maxRange);

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.useLayerMask = true;
        contactFilter.layerMask = LayerMask.GetMask("Default");
        Collider2D[] results = new Collider2D[5];

        // 🔨与场景物体碰撞 过滤不需要的对象
        if (hammerHead.GetComponent<Rigidbody2D>().OverlapCollider( contactFilter, results) > 0) 
        {
            // 更新body的坐标
            Vector3 targetBodyPos = hammerHead.position - mouseVec;
            
            Vector3 force = (targetBodyPos - body.position) * jump;
            body.GetComponent<Rigidbody2D>().AddForce(force);
            // 设置刚体速度
            body.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(
                body.GetComponent<Rigidbody2D>().velocity, 6);
        }

       

        Vector3 newHammerPos = body.position + mouseVec;
        Vector3 hammerMoveVec = newHammerPos - hammerHead.position;
        newHammerPos = hammerHead.position + hammerMoveVec * 0.2f;

        // 移动🔨
        hammerHead.GetComponent<Rigidbody2D>().MovePosition(newHammerPos);

        hammerHead.rotation = Quaternion.FromToRotation(
            Vector3.right, newHammerPos - body.position);
    }
    private void Update()
    {
        //Debug.Log(weaponUse + " weaponUse");
        if (weaponUse)
        {
            WeaponSwitch();
        }
    }



    public void WeaponSwitch()
    {

        //武器切换
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Debug.Log("jumpState " + jumpState);
            if (jumpState)
            {
                //Debug.Log("红色");
                redHammer.SetActive(true);
                sword.SetActive(false);
                jump = 80.0f;
                //Debug.Log(jump + " jump");
                jumpState = false;
                GetComponent<playerDemage>().playerDmgValue = 1;
            }
            else
            {
                //Debug.Log("🗡");
                redHammer.SetActive(false);
                sword.SetActive(true);
                //Debug.Log("layer " + sword.transform.position.z);
                // false sword.transform.position.z = 1;

                Vector3 newVector = sword.transform.position;
                newVector.z = 1;
                sword.transform.position = newVector;
                jump = 150.0f;
                //Debug.Log(jump + " jump");
                jumpState = true;
                GetComponent<playerDemage>().playerDmgValue = 3;
                GetComponent<playerDemage>().beatBackForce = 40.0f;
            }
        }
    }


}
