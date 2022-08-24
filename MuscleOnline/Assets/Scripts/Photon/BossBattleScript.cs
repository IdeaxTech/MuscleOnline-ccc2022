using Photon.Pun;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Collections;
using System;

public class BossBattleScript : MonoBehaviourPunCallbacks
{
    public static int damage;

    static string BossName;
    static int BossHP;
    static int BossOffence;
    static int BossDefence;

    static int QuestDiff;
    static object QuestReward;

    static string TrainingName;

    void Start()
    {
        PlayerNo.SetDisplayPlayerNo();
        GameObject.FindWithTag("MyName").GetComponent<TMP_Text>().text = UserInfo.UserName;
        Debug.Log("BossBattleScriptが呼ばれました");
    }

    public static void BossBattle()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //OperateCostomProperty.SetRoomCustomProperty("TotalHP", 0);
            OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", 0);
        }


        //カウントを初期化
        OperateCostomProperty.SetUserCustomProperty("Count", 0);
        OperateCostomProperty.SetUserCustomProperty("TotalCount", 0);

        // クエスト情報、ボス情報、筋トレ時間、休憩時間を設定
        SetQuestInfo();

        //TODOデバッグ用
        UserInfo.UserHP = 100;
        UserInfo.UserAttack = 10;

        // 味方HPを合算
        OperateCostomProperty.SetUserCustomProperty("MyHP", UserInfo.UserHP);

        // ボス登場アニメーション
        StartAnimation();

        // バトルの開始
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
    }

    public static async void SetQuestInfo()
    {
        //　ボスのid
        string id = "FlfuY9qPnDJFZzN3tBDU";

        // データベースからクエスト情報を取得
        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot BossData = await db.Collection("bosses").GetSnapshotAsync();
        foreach (var document in BossData.Documents)
        {
            Dictionary<string, object> DictionaryData = document.ToDictionary();
            if (document.Id.Equals(id))
            {
                BossName = DictionaryData["boss_name"].ToString();
                BossHP = (int)Convert.ChangeType(DictionaryData["boss_hp"], typeof(int));
                BossOffence = (int)Convert.ChangeType(DictionaryData["boss_attack"], typeof(int));
                BossDefence = (int)Convert.ChangeType(DictionaryData["boss_defence"], typeof(int));
            }

        }

        QuerySnapshot QuestData = await db.Collection("quests").GetSnapshotAsync();
        foreach (var document in QuestData.Documents)
        {
            Dictionary<string, object> DictionaryData = document.ToDictionary();
            if (DictionaryData["boss_id"].ToString() == id)
            {
                QuestDiff = (int)Convert.ChangeType(DictionaryData["quest_difficult"], typeof(int));
                QuestReward = (Dictionary<string, object>)Convert.ChangeType(DictionaryData["quest_reward"], typeof(Dictionary<string, object>));
            }
        }

        //ボスへのダメージを計算
        damage = UserInfo.UserAttack - BossDefence;

        // カスタムプロパティに代入
        if (PhotonNetwork.IsMasterClient)
        {

            OperateCostomProperty.SetRoomCustomProperty("BossName", BossName);
            OperateCostomProperty.SetRoomCustomProperty("BossHP", BossHP);
            OperateCostomProperty.SetRoomCustomProperty("BossAttack", BossOffence);
            OperateCostomProperty.SetRoomCustomProperty("QuestDiff", QuestDiff);
            OperateCostomProperty.SetRoomCustomProperty("QuestReward", QuestReward);

            int difficulity = (int)OperateCostomProperty.GetRoomCustomProperty("QuestDiff");
            OperateCostomProperty.SetRoomCustomProperty("TrainingTime", 5);
            OperateCostomProperty.SetRoomCustomProperty("RestTime", 0);
        }
        Debug.Log("Finish SetQuestInfo");
    }

    // TODO アニメーションの制御 => 江崎くんへ
    public static void StartAnimation()
    {

    }

    public static void StopAnimation()
    {

    }
    
    public async static void SetTrainingOption()
    {
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("TrainingType", UnityEngine.Random.Range(0, 5));

        OperateCostomProperty.SetUserCustomProperty("Count", 0);

        //与えるダメージをリセット
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("AllyAttackDamage", 0);

        // データベースからトレーニング情報を取得
        string id = "rIFhBoYhBpRX74L9othN";
        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot TrainingData = await db.Collection("trainings").GetSnapshotAsync();
        foreach (var document in TrainingData.Documents)
        {
            Dictionary<string, object> DictionaryData = document.ToDictionary();
            if (document.Id.Equals(id))
            {
                TrainingName = DictionaryData["training_name"].ToString();
            }
        }

        Debug.Log("Finish SetTrainingOption");
    }

    public static void StartTraining()
    {
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isTraining", true);

        Debug.Log("Finish StartTraining");
    }

    public static void AllyAttack()
    {
        Debug.Log("与えるダメージは" + OperateCostomProperty.GetRoomCustomProperty("AllyAttackDamage").ToString());
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("BossHP", (int)OperateCostomProperty.GetRoomCustomProperty("BossHP") - (int)OperateCostomProperty.GetRoomCustomProperty("AllyAttackDamage"));                
        }
        Debug.Log("Finish AllyAttack");

        // TODOアニメーションを流す
    }

    // ボスの攻撃、味方のHPを減らす
    public static void BossAttack()
    {
        // TODOアニメーションを流す

        //カスタムプロパティを変更
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("TotalHP", (int)OperateCostomProperty.GetRoomCustomProperty("TotalHP") - BossOffence);
        }

            Debug.Log("Finish BossAttack");
    }

    // TODO6. ボスのhpが0になれば戦闘終了、経験値、ドロップ品を獲得
    public static void DefeatBoss()
    {
        // ボスが倒れたアニメーション

        //報酬の受け渡し
        //データベースの更新
        Debug.Log("Finish DefeatBoss");
    }

    //7. ボス戦の終了
    public static void FinishBossBattle()
    {

        //ルームから離脱
        PhotonNetwork.Disconnect();
        Debug.Log("Finish FinishBossBattle");
        PhotonNetwork.LoadLevel("QuestResult");
    }


}
