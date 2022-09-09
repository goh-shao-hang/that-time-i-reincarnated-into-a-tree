using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAudio : MonoBehaviour
{
    //This script is created to bypass a WebGL audio issue.

    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playSound());
    }

    IEnumerator playSound()
    {
        yield return new WaitForSeconds(7.6f);
        AudioManager.Instance.PlaySound(clip);
    }
}
