using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedKnight : Enemy
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float attackCD = 2;
    private bool isAttacking = false;

    Animator anim;

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        transform.position = transform.position + new Vector3(0, Random.Range(1, 5), 0);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleApproaching()
    {
        base.HandleApproaching();
        anim.SetBool("Attacking", false);
    }

    protected override void HandleAttacking()
    {
        base.HandleAttacking();
        anim.SetBool("Attacking", true);
        if (!isAttacking)
            StartCoroutine(Attack());
    }

    protected override void HandleRunning()
    {
        base.HandleRunning();
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(attackCD);
        isAttacking = false;
    }

    void DamagePlayer()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}
