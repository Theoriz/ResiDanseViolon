using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrScene : MonoBehaviour
{
    [Header("MrScene Settings")]
    public string id;
    public float introDuration;
    public float outroDuration;

    private Coroutine outroCoroutine;

    public IEnumerator LaunchIntro()
    {
        MrSceneManager.launchingIntro = true;

        StartCoroutine(Intro());

        yield return new WaitForSeconds(introDuration);

        MrSceneManager.launchingIntro = false;
        MrSceneManager.playingScene = true;   
    }

    public IEnumerator LaunchOutro()
    {
        MrSceneManager.launchingOutro = true;
        MrSceneManager.playingScene = false;

        StartCoroutine(Outro());

        yield return new WaitForSeconds(outroDuration);

        MrSceneManager.launchingOutro = false;
    }

    public virtual IEnumerator Intro()
    {
        yield return 0;
    }

    public virtual IEnumerator Outro()
    {
        yield return 0;
    }
    /*
    protected void SetSceneVolume(float value)
    {
        if (SoundManager.instance)
        {
            SoundManager.instance.masterVolume = value;
        }
        else
        {
            AudioListener.volume = value;
        }
    }
    */
}
