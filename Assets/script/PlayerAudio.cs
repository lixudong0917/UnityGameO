using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
   
    public AudioSource audio;
    private AudioClip forceAudio;
    private AudioClip hiccupAudio;
    private AudioClip ohhAudio;
    public bool moveFlag;

    private void Awake()
    {
        //给对象添加一个AudioSource组件
        audio = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        audio.playOnAwake = false;
        //加载音效文件
        forceAudio = Resources.Load<AudioClip>("Force");
        hiccupAudio = Resources.Load<AudioClip>("Hiccup");
        ohhAudio = Resources.Load<AudioClip>("Ohh");
    }
    void Update()
    {
        moveFlag = gameObject.GetComponent<PlayerControl>().isMoving; //获取人物运动状态
        //print(moveFlag);
        if (moveFlag)
        {
            int musicNum = Random.Range(0, 3);
            //print(musicNum);
            //随机播放
            switch (musicNum)
            {
                case 0:
                    audio.clip = forceAudio;
                    break;
                case 1:
                    audio.clip = hiccupAudio;
                    break;
                case 2:
                    audio.clip = ohhAudio;
                    break;
            }
            //print(audio.clip);
            audio.Play();
            gameObject.GetComponent<PlayerControl>().isMoving = false;
        }
    }
}
