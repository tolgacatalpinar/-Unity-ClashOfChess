﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour, UnitInterface
{
    // For debug
    [SerializeField] float attackPoint = 5;
    [SerializeField] string targetTag;
    [SerializeField] int capacity = 1;

    //Speed
    public float speed = 5f;
    public float RotationSpeed = 100f;

    // Min Close Value
    public float minDistance = 10f;



    //for rotating
    private Quaternion _lookRotation;
    private Vector3 _direction;

    //Animator
    private Animator anim;

    int count = 0;
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        autoMove();
        autoRotate();

        count++;
        if (count % 50 == 0)
            autoHit();


    }
    private void hit()
    {
        if (target != null)
        {
            print("Hit");
            HPBar hpBar = target.GetComponentInChildren<HPBar>();
            hpBar.updateHealth(-attackPoint);
            /*
            HPBar targetHPBar = target.GetComponent<HPBar>();
            print("targetHPBar: " + targetHPBar.name);
            if ( targetHPBar != null)
                targetHPBar.updateHealth(-5);
                */
        }

    }

    public GameObject FindClosestEnemy(out float distance)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(targetTag);
        GameObject closest = null;
        distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    public void autoMove()
    {
        float step = speed * Time.deltaTime;

        float distance;
        FindClosestEnemy(out distance);

        if (!(distance < minDistance))
        {
            transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy(out distance).transform.position, step);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }
    public void autoRotate()
    {
        float distance;
        _direction = (FindClosestEnemy(out distance).transform.position - transform.position);

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

    }
    public void autoHit()
    {
        float distance;
        target = FindClosestEnemy(out distance);
        if (distance <= minDistance)
        {
            // todo there is a bug where 2 characters spawn on top of each other (use rigidbody)
            if (distance != 0)
                anim.SetTrigger("Attack1Trigger");
        }
        else
        {
            anim.ResetTrigger("Attack1Trigger");
        }

    }
    public int getCapacity()
    {
        return capacity;
    }
}

