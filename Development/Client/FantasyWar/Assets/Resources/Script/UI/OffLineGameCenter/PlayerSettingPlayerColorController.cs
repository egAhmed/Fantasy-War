using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingPlayerColorController : MonoBehaviour
{
    public PlayerSettingPlayerBarController barController;
    public Text label;
    int SelectedDropListIndex
    {
        get;
        set;
    }
    public Color SelectedColor
    {
        get
        {
            //hard code here
            switch (SelectedDropListIndex)
            {
                case 1:
                    return Color.red;
                case 2:
                    return Color.blue;
                case 3:
                    return Color.green;
                case 4:
                    return Color.yellow;
                default:
                    return Color.white;
            }
        }
    }
    Dropdown dropdownList;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        dropdownList = GetComponentInChildren<Dropdown>();
        dropdownList.onValueChanged.AddListener((x) =>
        {
			//
            SelectedDropListIndex = x;
           // Debug.Log("SelectedDropListIndex:" + SelectedDropListIndex);
            //
            if (label != null) {
                label.color = SelectedColor;
            }
            //
        });
    }
}
