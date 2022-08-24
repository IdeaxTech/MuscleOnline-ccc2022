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
    public static GameObject ReadyBtn;

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

        //ReadyBtn = GameObject.FindWithTag("ReadyBtn");
    }

    public static async void CreateDelay(int delay)
    {
        await Task.Delay(delay);
    }

    public static void BossBattle()
    {
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("TotalHP", 0);

        //カウントを初期化
        OperateCostomProperty.SetUserCustomProperty("Count", 0);
        OperateCostomProperty.SetUserCustomProperty("TotalCount", 0);

        // クエスト情報、ボス情報、筋トレ時間、休憩時間を設定
        SetQuestInfo();

        // 味方HPを合算
        OperateCostomProperty.SetUserCustomProperty("MyHP", UserInfo.UserHP);

        // ボス登場アニメーション
        StartAnimation();

        

        // バトルの開始
        //if (PhotonNetwork.IsMasterClient)
        //    OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
    }

    public static async void SetQuestInfo()
    {
        //　ボスのid
        string id = "FlfuY9qPnDJFZzN3tBDU";

        // TODO:データベースからクエスト情報を取得
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
        Debug.Log("BossName" + BossName);
        Debug.Log("BossHP" + BossHP);


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

        damage = UserInfo.UserAttack - BossDefence;

        // カスタムプロパティに代入
        if (PhotonNetwork.IsMasterClient)
        {
            //攻撃力とボスの防御力によって与えるダメージを計算
            damage = UserInfo.UserAttack - BossDefence;

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
    
    public static void SetTrainingOption()
    {
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("TrainingType", UnityEngine.Random.Range(0, 5));

        OperateCostomProperty.SetUserCustomProperty("Count", 0);

        //与えるダメージをリセット
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("AllyAttackDamage", 0);

        // TODO:データベースからトレーニング情報を取得
        string id = "rIFhBoYhBpRX74L9othN";
        List<Dictionary<string, object>> training_data = DatabaseOperation.GetData("trainings", id).Result;
        foreach (var data in training_data)
        {
            TrainingName = data["training_name"].ToString();
        }


        // TODO:ラベルにトレーニング情報を記載
        Debug.Log("Finish SetTrainingOption");
    }

    public static void SetStartTime()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("StartTime", PhotonNetwork.Time);
        }

        // TODO:ラベルを変更
        Debug.Log("Finish SetStartTime");
    }

    public static void DisplayTrainingInfo()
    {
        // トレーニング情報をカスタムプロパティから取得
        // ラベルにその情報を表示
        ReadyBtn.SetActive(true);

        Debug.Log("Finish DisplayTrainingInfo");
    }

    // トレーニング前のボタンが押された時
    public void ReadyTraining()
    {
        OperateCostomProperty.SetUserCustomProperty("isTrainingReady", true);
    }

    public static void StartTraining()
    {
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isTraining", true);

        Debug.Log("Finish StartTraining");
    }

    //4. パーティーの場合別のユーザーのカウントを受け取り、受け取ったら筋トレしてるアニメーションを動かす


    public static void AllyAttack()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("BossHP", (int)OperateCostomProperty.GetRoomCustomProperty("BossHP") - (int)OperateCostomProperty.GetRoomCustomProperty("AllyAttackDamage"));                
        }
        Debug.Log("Finish AllyAttack");

        //アニメーションを流す
    }

    // ボスの攻撃、味方のHPを減らす
    public static void BossAttack()
    {
        //アニメーションを流す

        //カスタムプロパティを変更
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("TotalHP", (int)OperateCostomProperty.GetRoomCustomProperty("TotalHP") - 20);
        }

            Debug.Log("Finish BossAttack");
    }

    //休憩時間
    public static void RestTime()
    {
        SetStartTime();

    }

    //6. ボスのhpが0になれば戦闘終了、経験値、ドロップ品を獲得
    public static void DefeatBoss()
    {
        //ボスが倒れたアニメーション

        //報酬の受け渡し
        //データベースの更新
        Debug.Log("Finish DefeatBoss");
    }


    // ループ

    //7. ボス戦の終了
    public static void FinishBossBattle()
    {
        //元の画面に遷移


        //ルームから離脱
        PhotonNetwork.Disconnect();
        Debug.Log("Finish FinishBossBattle");
        PhotonNetwork.LoadLevel("four");
    }


}
