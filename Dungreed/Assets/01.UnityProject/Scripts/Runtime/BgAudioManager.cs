using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BgAudioManager : MonoBehaviour
{
    // 배경 오디오를 전부 넣을 배열
    public AudioClip[] audioClips = default;
    // 현재 재생할 노래
    public AudioSource bgAudio = default;

    // 현재 씬 이름
    public string nowScene = default;

    // Start is called before the first frame update
    void Start()
    {
        audioClips = Resources.LoadAll<AudioClip>("Sound/BgSound");

        bgAudio = gameObject.GetComponentMust<AudioSource>();

        bgAudio.loop = true;

        nowScene = SceneManager.GetActiveScene().name;

        switch (nowScene)
        {
            case GData.SCENE_NAME_TITLE:
                bgAudio.clip = audioClips[0];
                bgAudio.Play();
                break;

            case GData.SCENE_NAME_TOWN:
                bgAudio.clip = audioClips[0];
                bgAudio.Play();
                break;

            case GData.SCENE_NAME_PLAY:
                bgAudio.clip = audioClips[1];
                bgAudio.Play();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 보스방에 진입하면 브금이 바뀌는 함수
    public void ChangeBgSoundBoss()
    {
        bgAudio.clip = audioClips[2];
        bgAudio.Play();
    }

    // 보스가 죽으면 다시 원래대로 브금이 바뀌는 함수
    public void EndBossBgSound()
    {
        bgAudio.clip = audioClips[1];
        bgAudio.Play();
    }



}
