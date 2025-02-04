using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UIShowHidePanel : MonoBehaviour
{

    Button b;
    
	public List<GameObject> panels;
	
	public int showIndex;

    public void Start()
    {
        b = GetComponent<Button>();

        b.onClick.AddListener(Show);
    }
	
	public void Show()
	{
		foreach(GameObject item in panels)
		{		
			item.SetActive(false);
		}
		panels[showIndex].SetActive(true);
	}
}
