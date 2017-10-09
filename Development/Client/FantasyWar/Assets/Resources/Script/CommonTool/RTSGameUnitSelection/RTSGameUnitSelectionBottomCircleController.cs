using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitSelectionBottomCircleController : MonoBehaviour
{
    public static string prefabPath = @"Prefab/SelectedEffect/RTSGameUnitSelectionBottomCircle";
    //
    private GameObject circleRed;
    private GameObject circleGreen;
    private GameObject circleYellow;
    //
    //
    public void hide()
    {
        circleRed.SetActive(false);
        circleYellow.SetActive(false);
        circleGreen.SetActive(false);
        gameObject.SetActive(false);
    }
    public void showRedCircle()
    {
        circleRed.SetActive(true);
        circleYellow.SetActive(false);
        circleGreen.SetActive(false);
        gameObject.SetActive(true);
    }

    public void showGreenCircle()
    {
        circleRed.SetActive(false);
        circleYellow.SetActive(false);
        circleGreen.SetActive(true);
        gameObject.SetActive(true);
    }

    public void showYellowCircle()
    {
        circleRed.SetActive(false);
        circleYellow.SetActive(true);
        circleGreen.SetActive(false);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        circleRed = transform.Find("CircleRed").gameObject;
        circleGreen = transform.Find("CircleGreen").gameObject;
        circleYellow = transform.Find("CircleYellow").gameObject;
        //
        hide();
        //
    }

    public void show(RTSGameUnitBelongSide side)
    {
        switch (side)
        {
            case RTSGameUnitBelongSide.Player:
                showGreenCircle();
                break;
            case RTSGameUnitBelongSide.FriendlyGroup:
                showYellowCircle();
                break;
            case RTSGameUnitBelongSide.EnemyGroup:
                showRedCircle();
                break;
            default:
                break;
        }
    }
}
