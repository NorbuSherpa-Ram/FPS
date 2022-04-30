using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scope : MonoBehaviour  
{
    Animator anim;

   public  bool isScoped = false ;

    public GameObject weaponCamera;
    public GameObject scopeOverLay;

   [SerializeField ] Camera mainCam;
    public float  scopedFOV = 20;
    float normalFOV;

    void Start()
    {
        anim = GetComponent<Animator>();
        weaponCamera = GameObject.FindGameObjectWithTag("WeaponCamera");
    }

    void Update()
    {
        if(Input.GetButtonDown ("Fire2"))
        {
            isScoped = !isScoped;
            anim.SetBool("scoped", isScoped);
            if(isScoped)
            {
               StartCoroutine ( OnScope());
            }
            else
            {
                OnUnScope();
            }
        }
    }
    // calling from gunMeachanish while reloading 
    public void NeedToScope()
    {
        StartCoroutine(OnScope());
    }
  public  IEnumerator OnScope()
    {
        yield return new WaitForSeconds(0.11f);
        scopeOverLay.SetActive(true);
        weaponCamera.SetActive(false);
        normalFOV = mainCam.fieldOfView;
        mainCam.fieldOfView = scopedFOV;
    }
    public void OnUnScope()
    {
        scopeOverLay.SetActive(false);
        weaponCamera.SetActive(true);
        mainCam.fieldOfView = normalFOV;
    }
}
