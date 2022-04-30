using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform  cam;
    void Start()
    {
        cam =GameObject.FindGameObjectWithTag ("MainCamera"). GetComponent<Transform>();
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
