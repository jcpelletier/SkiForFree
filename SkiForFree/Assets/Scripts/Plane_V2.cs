using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane_V2 : MonoBehaviour {

    void Start()
    {
    }
    
    void Update()
    {
        //StartCoroutine("DestroyTerrain");
    }
    
    //StartCoroutine("DestroyTerrain"); // play this after player coordinate goes past certain value

    public IEnumerator DestroyTerrain()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
