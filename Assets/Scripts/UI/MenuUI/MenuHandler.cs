using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject[] menus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}
