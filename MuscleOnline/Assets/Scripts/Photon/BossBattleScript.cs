using Photon.Pun;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Collections;

public class BossBattleScript : MonoBehaviourPunCallbacks
{
    [SerializeField] static int damage;
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

        // クエスト情報、ボス情報、筋トレ時間、休憩時間を設定
        SetQuestInfo();

        // 味方HPを合算
        //OperateCostomProperty.SetUserCustomProperty("MyHP", 100);

        //// ボス登場アニメーション
        //StartAnimation();

        //OperateCostomProperty.SetUserCustomProperty("TotalCount", 0);

        //// バトルの開始
        //if (PhotonNetwork.IsMasterClient)
        //    OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
    }

    public static void SetQuestInfo()
    {
        //　ボスのid
        string id = "FlfuY9qPnDJFZzN3tBDU";

        // TODO:データベースからクエスト情報を取得
        //IEnumerable loadingData()
        //{
        //    Task<List<Dictionary<string, object>>> BossDataTask = firebaseController.DatabaseOperation.GetData("bosses", id);
        //    while (!BossDataTask.IsCompleted)
        //        yield return null;

        //    BossData = BossDataTask.Result;
        //    foreach (var data in BossData)
        //    {
        //        BossName = data["boss_name"].ToString();
        //        BossHP = (int)data["boss_hp"];
        //        BossOffence = (int)data["boss_attack"];
        //        BossDefence = (int)data["boss_defence"];
        //    }
        //}

        //StartCoroutine(loadingData());


        //List<Dictionary<string, object>> quest_data = DatabaseOperation.GetData("quests").Result;
        //foreach (var data in quest_data)
        //{
        //    if (data["boss_id"].ToString() == id)
        //    {
        //        QuestDiff = (int)data["quest_difficult"];
        //        QuestReward = (Dictionary<string, object>)data["quest_reward"];
        //    }
        //}

        //damage = 10;

        //// カスタムプロパティに代入
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    //攻撃力とボスの防御力によって与えるダメージを計算
        //    damage = UserInfo.UserAttack - BossDefence;

        //    OperateCostomProperty.SetRoomCustomProperty("BossName", BossName);
        //    OperateCostomProperty.SetRoomCustomProperty("BossHP", BossHP);
        //    OperateCostomProperty.SetRoomCustomProperty("BossAttack", BossOffence);
        //    OperateCostomProperty.SetRoomCustomProperty("QuestDiff", QuestDiff);
        //    OperateCostomProperty.SetRoomCustomProperty("QuestReward", QuestReward);

        //    int difficulity = (int)OperateCostomProperty.GetRoomCustomProperty("QuestDiff");
        //    OperateCostomProperty.SetRoomCustomProperty("TrainingTime", 5);
        //    OperateCostomProperty.SetRoomCustomProperty("RestTime", 0);
        //}
        //Debug.Log("Finish SetQuestInfo");
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
    public void AddCount()
    {
        OperateCostomProperty.SetUserCustomProperty("Count", (int)OperateCostomProperty.GetUserCustomProperty("Count") + 1);
        OperateCostomProperty.SetUserCustomProperty("TotalCount", (int)OperateCostomProperty.GetUserCustomProperty("TotalCount") + 1);

        OperateCostomProperty.SetRoomCustomProperty("AllyAttackDamage", (int)OperateCostomProperty.GetRoomCustomProperty("AllyAttackDamage") + damage);

        // TODOアニメーションを流す

    }

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
