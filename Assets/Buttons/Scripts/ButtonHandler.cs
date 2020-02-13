
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    #pragma warning disable 0649
    public GameObject coinPrefab;
    bool buttonEnabled;
    public float spawnHeightOffset = 5.3f;
    public int totalCapacity = 10;

    // Start is called before the first frame update
    void Start()
    {
        buttonEnabled = false;
        
    }
    
    public void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            bool isFull = totalCapacity <= calculateCurrentCapacity();
            if (!isFull && buttonEnabled && Input.GetButtonDown("Fire1"))
                PutCoin(Input.mousePosition);
        }

    }
    
    
    public void PutCoin(Vector2 mousePosition)
    {
        RaycastHit hit = RayFromCamera(mousePosition, 1000.0f);
        Vector3 spaceVector = hit.point;
        
        spaceVector.y = spaceVector.y + spawnHeightOffset;
        GameObject.Instantiate(coinPrefab, spaceVector, Quaternion.identity);
    }

    public RaycastHit RayFromCamera(Vector3 mousePosition, float rayLength)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        Physics.Raycast(ray, out hit, rayLength);
        return hit;
    }

    public void buttonClicked()
    {
        if (!buttonEnabled)
            buttonEnabled = true;
        else
            buttonEnabled = false;
    }
    public int calculateCurrentCapacity()
    {
        string tag = coinPrefab.tag;
        int sumCapacity = 0;
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(tag);
        foreach( GameObject go in gos)
        {
            if (go is UnitInterface)
            {
                //go implements UnitInterface;
                //UnitInterface ob = go;
            }
                //sumCapacity +=( (UnitInterface) (go)).getCapacity();
        }
        return gos.Length;
    }
}
