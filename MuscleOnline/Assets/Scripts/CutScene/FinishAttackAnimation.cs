using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishAttackAnimation : MonoBehaviour
{
    public void FinishAttackAnimationToAnotherScene()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("isTraining", false);
            //カウントが0になったらカウント、攻撃力等を用いてダメージ量を計算
            BossBattleScript.AllyAttack();

            //ボスのターン
            //BossBattleScript.BossAttack();
        }
    }
}
