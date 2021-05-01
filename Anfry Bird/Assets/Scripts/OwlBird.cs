using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBird : Bird
{
    public GameObject explosion;
    public bool _hasBoom = false;

    void Update()
    {
        if (State == BirdState.HitSomething && !_hasBoom )
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Vector2 temp = new Vector2(50,0);
            transform.position = temp;
            StartCoroutine(Wait(2));   
            _hasBoom = true;
            
        }
        
    }

    private IEnumerator Wait(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }
}
