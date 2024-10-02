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
}
