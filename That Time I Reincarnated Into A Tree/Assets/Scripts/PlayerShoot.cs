using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public static bool canFire = true;

    public float projectileSpeed;

    private Camera mainCam;
    [SerializeField] private Transform firePoint;

    private Quaternion aimRotation;
    private Vector3 mousePos;
    
    public GameObject projectile;
    public AudioClip shootSound;

    //public Button growButton;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        AimAtLocation();

        if (Input.GetMouseButtonDown(0))
        {
            if (canFire)
                FireProjectile();
        }
    }

    void AimAtLocation()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        aimRotation = Quaternion.Euler(0, 0, rotz);
        firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, aimRotation, 10 * Time.deltaTime);
    }

    void FireProjectile()
    {
        AudioManager.Instance.PlaySound(shootSound);
        var proj = Instantiate(projectile, firePoint.position, aimRotation);
        if (proj)
        {
            proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * projectileSpeed;
            Destroy(proj, 5);
        }
    }
}
