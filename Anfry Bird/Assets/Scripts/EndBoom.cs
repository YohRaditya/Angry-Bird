using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfter(0.1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }
}
