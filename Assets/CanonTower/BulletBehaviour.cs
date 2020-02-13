using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public float speed = 500f;
    public float RotationSpeed = 100f;
    public int delayAttackRate = 100;
    // Min Close Value
    public float minDistance = 100f;



    //for rotating
    private Quaternion _lookRotation;
    private Vector3 _direction;

    int count = 0;
    GameObject target;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision other)
    {
        GameObject ob = other.gameObject;
        HPBar hpBar = ob.GetComponentInChildren<HPBar>();
        if(hpBar != null)
        {
            print("Bullet hit: " + ob.name);
            hpBar.updateHealth(-5);
        }
        else
        {
            print("Bullet missed: " + ob.name);
        }
        Destroy(this.gameObject);

        
    }

    void Update()
    {

        //autoMove();
        //autoRotate();


    }

    private void hit()
    {
        if (target != null)
        {
            print("Hit");
            HPBar hpBar = target.GetComponentInChildren<HPBar>();
            hpBar.updateHealth(-5);
            /*
            HPBar targetHPBar = target.GetComponent<HPBar>();
            print("targetHPBar: " + targetHPBar.name);
            if ( targetHPBar != null)
                targetHPBar.updateHealth(-5);
                */
        }

    }

    
}
