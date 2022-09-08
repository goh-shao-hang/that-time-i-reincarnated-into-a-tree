using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkApple : MonoBehaviour
{
    public int damage = 2;
    public AudioClip explosionSfx;
    public GameObject explosionParticles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySound(explosionSfx);
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1.5f);
            foreach (Collider2D enemy in hitEnemies)
            {
                var enemyHealth = enemy.GetComponent<Enemy>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
