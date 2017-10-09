using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSGameUnitActionCursorController : UnitySingleton<RTSGameUnitActionCursorController> {
    //
    private const string PREFAB_PATH_MOVE=@"prefab/ClickEffect/ClickMove";
    private const string PREFAB_PATH_ATTACK = @"prefab/ClickEffect/ClickAttack";
    //
    private RTSGameUnitActionClickEffect clickEffectMove;
    private RTSGameUnitActionClickEffect clickEffectAttack;
    //
    private void Awake()
    {
        //
        clickEffectMove = PrefabFactory.ShareInstance.createClone<RTSGameUnitActionClickEffect>(PREFAB_PATH_MOVE,Vector3.zero,Quaternion.identity);
        clickEffectAttack = PrefabFactory.ShareInstance.createClone<RTSGameUnitActionClickEffect>(PREFAB_PATH_ATTACK, Vector3.zero, Quaternion.identity);
        //
        hideAll();
    }
    //
    private void hideAll() {
        hideMove();
        hideAttack();
    }
    //
    private void hideMove()
    {
        if (clickEffectMove)
        {
            clickEffectMove.gameObject.SetActive(false);
        }
    }//
    private void hideAttack()
    {
        //
        if (clickEffectAttack)
        {
            clickEffectAttack.gameObject.SetActive(false);
        }
    }
    //
    public void showAttack()
    {
        if (clickEffectAttack)
        {
            clickEffectAttack.gameObject.SetActive(true);

        }
    }
    //
    public void showMove(Vector3 pos)
    {
        if (clickEffectMove)
        {
            clickEffectMove.gameObject.SetActive(true);
            clickEffectMove.gameObject.transform.position= pos;
            clickEffectMove.play();
        }
    }
}
