using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
{
    private int tag = 0;

    [SerializeField]private Image image1;
    [SerializeField]private Image image2;
    [SerializeField]private Image image3;
    [SerializeField] private AudioSource audioSource;
    public void OnLoadNextScene()
    {
        TimelineManager.Instance.endTimeline.Play();
        StartCoroutine(WaitForAction(LoadNextScene,2));
    }
    
    public void OnApplicationQuit()
    {
        TimelineManager.Instance.endTimeline?.Play();
        StartCoroutine(WaitForAction(ApplicationQuit,2));
    }

    private void LoadNextScene()
    {
        if ((SceneManager.GetActiveScene().buildIndex+1) == 5)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void ApplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private IEnumerator WaitForAction(Action myAction ,float time)
    {
        yield return new WaitForSeconds(time);
        myAction.Invoke();
    }

    public void BeginAudioPlay() => AudioSystem.Instance.FileBookAuidoPlay();

    public void EndPlay()
    {
        if (tag == 0)
        {
            image1.GetComponent<Animator>().SetTrigger("Disappear");
            ++tag;
        }
        else if (tag == 1)
        {
            image2.GetComponent<Animator>().SetTrigger("Disappear");
            ++tag;
        }
        else if (tag == 2)
        {
            image3.GetComponent<Animator>().SetTrigger("Disappear");
            StartCoroutine(WaitForAction(LoadNextScene,1));
            StartCoroutine(AudioIEnum());
            ++tag;
        }
    }

    private IEnumerator AudioIEnum()
    {
        yield return new WaitForSeconds(0.9f);
        AudioSystem.Instance.bgm1.Play();
        AudioSystem.Instance.bgm2.Play();
        AudioSystem.Instance.bgm3.Play();
        AudioSystem.Instance.endBgm.Stop();
    }

    public void AudioPlay() => AudioSystem.Instance.FileBookAuidoPlay();
}
