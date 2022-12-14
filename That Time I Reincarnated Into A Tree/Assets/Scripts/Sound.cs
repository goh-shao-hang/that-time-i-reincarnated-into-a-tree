using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaySoundOnStart: MonoBehaviour
{

    [SerializeField] private AudioClip clip;

    private void Start()
    {
        AudioManager.Instance.PlaySound(clip);
    }
}
