using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButtonHandler : MonoBehaviour
{
    [SerializeField] Button readyButton;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
        GetComponent<Button>().onClick.AddListener(TogglePause);
    }
    public void TogglePause()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
        readyButton.gameObject.SetActive(false);
        GameObject [] deleteObjects = GameObject.FindGameObjectsWithTag("ToBeDeleted");

        if (deleteObjects == null)
            deleteObjects = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (GameObject ob in deleteObjects)
        {
            Destroy(ob);
        }
    }
}
