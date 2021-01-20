using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    //音源AudioSource相当于播放器，而音效AudioClip相当于磁带
    public AudioSource audio;
    public AudioClip forceAudio;//这里我要给主角添加跳跃的音效
    public bool moveFlag;
    private void Awake()
    {
        //给对象添加一个AudioSource组件
        audio = gameObject.AddComponent<AudioSource>();
        //设置不一开始就播放音效
        audio.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        forceAudio = Resources.Load<AudioClip>("Force");
        print(forceAudio);
    }
    void Update()
    {
        moveFlag = gameObject.GetComponent<PlayerControl>().isMoving;
        //print(moveFlag);
        if (moveFlag)
        {
            audio.clip = forceAudio;
            //播放音效
            audio.Play();
            gameObject.GetComponent<PlayerControl>().isMoving = false;
        }
    }
}
