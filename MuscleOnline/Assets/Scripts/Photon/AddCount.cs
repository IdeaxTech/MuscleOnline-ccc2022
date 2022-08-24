using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCount : MonoBehaviour
{
    public void IncCount()
    {
        if ((bool)OperateCostomProperty.GetRoomCustomProperty("isTraining"))
        {
            OperateCostomProperty.SetUserCustomProperty("Count", (int)OperateCostomProperty.GetUserCustomProperty("Count") + 1);
            OperateCostomProperty.SetUserCustomProperty("TotalCount", (int)OperateCostomProperty.GetUserCustomProperty("TotalCount") + 1);

            OperateCostomProperty.SetRoomCustomProperty("AllyAttackDamage", (int)OperateCostomProperty.GetRoomCustomProperty("AllyAttackDamage") + BossBattleScript.damage);

            // TODOアニメーションを流す

            // TODO音声を流す
        }
    }
}
