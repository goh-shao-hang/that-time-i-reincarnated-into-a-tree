using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GrowButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    ParticleSystem particles;
    Animator anim;

    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerShoot.canFire = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerShoot.canFire = true;
    }

    public void PlayPressedAnimation()
    {
        particles.Play();
        anim.SetTrigger("Pressed");
        Invoke(nameof(ResetTrigger), 0.15f);
    }

    void ResetTrigger()
    {
        anim.ResetTrigger("Pressed");
    }
}
