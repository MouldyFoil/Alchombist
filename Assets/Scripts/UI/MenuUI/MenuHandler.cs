using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
    [SerializeField] UnityEvent escapeButtonFunctionalities;
    bool disableEscape = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && disableEscape == false)
        {
            escapeButtonFunctionalities.Invoke();
        }
    }
    public void SwitchMenu(string menuName)
    {
        bool menuInArrayCheck = false;
        foreach(GameObject menu in menus)
        {
            if(menu.name == menuName)
            {
                menuInArrayCheck = true;
            }
        }
        if(menuInArrayCheck == false)
        {
            Debug.Log("Menu either doesnt exist or isnt in array");
            return;
        }
        foreach(GameObject menu in menus)
        {
            if(menu.name == menuName)
            {
                menu.SetActive(true);
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }
    public void ActivateMenu(string menuName)
    {
        foreach (GameObject menu in menus)
        {
            if (menu.name == menuName)
            {
                menu.SetActive(true);
            }
        }
    }
    public void DeactivateMenu(string menuName)
    {
        foreach (GameObject menu in menus)
        {
            if (menu.name == menuName)
            {
                menu.SetActive(false);
            }
        }
    }
    public void FlipMenuEnabled(string menuName)
    {
        foreach (GameObject menu in menus)
        {
            if (menu.name == menuName)
            {
                if(menu.active == false)
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
    public void FlipPauseOrUnpause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void DisableEscapeButton()
    {
        disableEscape = true;
    }
}
