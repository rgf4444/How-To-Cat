using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsoleBehavior : MonoBehaviour
{
    [SerializeField] private GameObject consoleUI;
    [SerializeField] private List<GameObject> requiredObjects;
    [SerializeField] private GameObject warningText;
    [SerializeField] private MonoBehaviour playerMovementScript;

    private bool playerNearby = false;
    private bool isLocked = false; 

    private void Start()
    {
        warningText.SetActive(false);
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !isLocked)
        {
            if (AllObjectsDestroyed())
            {
                ToggleConsole();
            }
            else
            {
                StartCoroutine(DenyAccess());
            }
        }
    }

    private bool AllObjectsDestroyed()
    {
        foreach (GameObject obj in requiredObjects)
        {
            if (obj != null)
                return false;
        }
        return true;
    }

    private void ToggleConsole()
    {
        bool isActive = consoleUI.activeSelf;
        consoleUI.SetActive(!isActive);
        playerMovementScript.enabled = !consoleUI.activeSelf;
    }

    private IEnumerator DenyAccess()
    {
        isLocked = true;
        warningText.SetActive(true); 
        yield return new WaitForSeconds(3f); 
        warningText.SetActive(false);
        yield return new WaitForSeconds(4f);
        isLocked = false; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}
