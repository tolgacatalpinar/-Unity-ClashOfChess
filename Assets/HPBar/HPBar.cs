using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public float maxHealth = 100;
    private float health;
    int count = 0;
    private Transform bar;
    private GameObject my_camera;



    void Start()
    {
        my_camera = GameObject.Find("Main Camera");
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        count++;
        if( count % 10 == 0)
        {
            health--;
            float value = health / 100f;
            if( value >= 0)
                SetSize(health / 100f);
        }
        */
        SetSize(health / maxHealth);
        transform.LookAt(transform.position + my_camera.transform.rotation * Vector3.back, my_camera.transform.rotation * Vector3.down);
        transform.Rotate(new Vector3(0, 180, 0));

        if( health <= 0)
        {
            Destroy(this.transform.parent.gameObject);
        }




        //updateHealth(-1);

    }
    public void updateHealth( float value)
    {
        if( health > 0)
        {
            if (health + value < 0)
                health = 0;
            else
                health += value;
        }
    }

    private void Awake()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color)
    {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
