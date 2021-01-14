using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{

    public float speed ;
    private const int UP = 0;
    private const int LEFT = 1;
    private const int RIGHT = 2;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveByWad();
    }

    void MoveByWad()
    {
        if (Input.GetKey("w"))
        {
            SetState(UP);
            GameObject.Find("Body").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
        if (Input.GetKeyUp("w"))
        {

            GameObject.Find("Body").GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        if (Input.GetKey("a"))
        {
            SetState(LEFT);
        }
        else if (Input.GetKey("d"))
        {
            SetState(RIGHT);
        }
    }
    void SetState(int currState)
    {

        Vector2 transformValue = new Vector2();
        switch (currState)
        {
            case 0:
                transformValue = Vector2.up * Time.deltaTime * speed;
                break;
            case 1:
                transformValue = Vector2.left * Time.deltaTime * speed;
                break;
            case 2:
                transformValue = Vector2.right * Time.deltaTime * speed;
                break;
        }

        transform.Translate(transformValue, Space.Self);

    }
}
