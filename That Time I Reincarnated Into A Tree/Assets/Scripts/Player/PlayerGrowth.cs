using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrowth : MonoBehaviour
{
    Animator anim;

    public AudioClip growSound;
    public ParticleSystem growParticles;

    public int growth = 0;
    public int growthPerClick;
    public int growthToWin = 50;

    public Slider GrowthMeter;

    private void Start()
    {
        anim = GetComponent<Animator>();
        GrowthMeter = GameObject.Find("GrowthMeter").GetComponent<Slider>();
        GrowthMeter.maxValue = growthToWin;
        GrowthMeter.value = 0;
    }

    public void Grow()
    {
        growth += growthPerClick;
        GrowthMeter.value = growth;
        AudioManager.Instance.PlaySound(growSound);
        growParticles.Play();
        anim.SetTrigger("Grow");
        Invoke(nameof(ResetTrigger), 0.15f);

        if (growth >= growthToWin)
        {
            GameManager.Instance.Victory();
        }
    }

    void ResetTrigger()
    {
        anim.ResetTrigger("Grow");
    }
}
