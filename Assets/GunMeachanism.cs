using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMeachanism : MonoBehaviour
{
    [SerializeField] int damage = 10;

    private bool isHit;
    [SerializeField] Animator anim;
    [SerializeField] int range = 100;
    [SerializeField] float impactForce = 10f;
    [SerializeField] GameObject fpsCamera;
    [SerializeField] GameObject impactEffect;

    [Header("Ammo")]
    public float reloadTIme;
    private int currentAmmo;
    protected bool isReloading = false;
    [SerializeField] int maxAmmo = 5;

    [Header("Muzzle Effect ")]
    public Transform muzzlePoint;
    [SerializeField] ParticleSystem muzzleFlash;

    [SerializeField] Scope scopeScript;
    void Start()
    {
        currentAmmo = maxAmmo;
    }
    void Update()
    {
        if (isReloading) return;

        if (Input.GetButtonDown("Fire1"))
        {
            currentAmmo--;
            Shoot();
        }
        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }
    }
    private void Shoot()
    {
        RaycastHit hitInfo;
        isHit = Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hitInfo, range);
        if (isHit)
        {
            Debug.Log(hitInfo.transform.name);
            Instantiate(impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            EnemyHealth  enemyHealth =  hitInfo.transform.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Rigidbody rb = hitInfo.transform!.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-(transform.position - hitInfo.transform.position) * impactForce);
            }
        }
        if (scopeScript.isScoped == false)
        {
            Instantiate(muzzleFlash, muzzlePoint.position, muzzlePoint.rotation);
        }
    }
    IEnumerator Reload()
    {
        // if he is scoping while reloading the gun 
        if (scopeScript!.isScoped)
        {
            scopeScript.OnUnScope();
        }

        isReloading = true;
        anim.SetBool("reload", true);
        yield return new WaitForSeconds(reloadTIme - 0.25f);
        anim.SetBool("reload", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;

        if (scopeScript!.isScoped)
        {
            StartCoroutine(scopeScript.OnScope());
        }
    }
}
