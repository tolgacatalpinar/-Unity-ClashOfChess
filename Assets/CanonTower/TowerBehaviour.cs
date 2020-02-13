using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour, UnitInterface
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    GameObject bullet;
    GameObject target;

    public float bulletSpeed = 30f;
    public float lifeTime = 10f;
    [SerializeField] int capacity = 2;
    float distance;
    public string targetTag;

    public int fireDelay;
    private int count = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         target = FindClosestEnemy(out distance);

        fireController();
            
        rotateTower();
    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
    private void fireController()
    {
        if( Time.timeScale != 0.0f)
        {
            count++;
            if (count % fireDelay == 0)
            {
                fire();
            }
        }
        

        
    }
    public void fire()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = bulletSpawn.position;
        bullet.GetComponent<Rigidbody>().velocity = target.transform.position - bullet.transform.position;
        rotateBullet( bullet);
        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));
        
    }
    public void autoMove(GameObject bullet)
    {
        float step = bulletSpeed * Time.deltaTime;
        float distance;
        GameObject target = FindClosestEnemy(out distance);

        bullet.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        
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
    public void rotateTower()
    {
        Vector3 targetVector;

        targetVector = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
        transform.LookAt(targetVector);
    }
    public void rotateBullet(GameObject bullet)
    {
        Vector3 targetVector;

        targetVector = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
        bullet.transform.LookAt(targetVector);
    }
    public int getCapacity()
    {
        return capacity;
    }
}
