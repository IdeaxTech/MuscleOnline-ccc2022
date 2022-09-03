using Photon.Pun;
using UnityEngine;
using TMPro;
using Firebase.Firestore;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

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

    [SerializeField] GameObject BattleBGM;

    [SerializeField] GameObject ReadyBtn;

    void Start()
    {
        
        PlayerNo.SetDisplayPlayerNo();
        GameObject.FindWithTag("MyName").GetComponent<TMP_Text>().text = UserInfo.UserName;
        Debug.Log("BossBattleScriptが呼ばれました");

        //筋トレの種類を設定
        SetTrainingOption();
        // 筋トレ内容を表示させ、準備をする
        ReadyBtn.SetActive(true);

        GameObject BGMObject = GameObject.Find("BattleBGM");
        if (!BGMObject)
        {
            BattleBGM.SetActive(true);
        }

        GameObject BossWaitBGM = GameObject.Find("BossWaitBGM");
        if (BossWaitBGM)
            Destroy(BossWaitBGM);

    }

    public static void BossBattle()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            OperateCostomProperty.SetRoomCustomProperty("isTrainingReady", 0);
        }


        //カウントを初期化
        OperateCostomProperty.SetUserCustomProperty("Count", 0);
        OperateCostomProperty.SetUserCustomProperty("TotalCount", 0);

        OperateCostomProperty.SetUserCustomProperty("isTrainingReady", false);

        // クエスト情報、ボス情報、筋トレ時間、休憩時間を設定
        SetQuestInfo();

        // 味方HPを合算
        OperateCostomProperty.SetUserCustomProperty("MyHP", UserInfo.UserHP);
        OperateCostomProperty.SetRoomCustomProperty("AllyMaxHP", UserInfo.UserHP);

        // バトルの開始
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
    }

    public static async void SetQuestInfo()
    {
        //　ボスのid
        // カスタムプロパティに代入
        if (PhotonNetwork.IsMasterClient)
        {
            string id = "FlfuY9qPnDJFZzN3tBDU";

            // データベースからクエスト情報を取得
            var db = FirebaseFirestore.DefaultInstance;
            QuerySnapshot BossData = await db.Collection("bosses").GetSnapshotAsync();
            foreach (var document in BossData.Documents)
            {
                Debug.Log("ボスデータベースが見つかりあmした");
                Dictionary<string, object> DictionaryData = document.ToDictionary();
                if (document.Id.Equals(id))
                {
                    Debug.Log("一致するデータが見つかりました");
                    BossName = DictionaryData["boss_name"].ToString();
                    BossHP = (int)Convert.ChangeType(DictionaryData["boss_hp"], typeof(int));
                    BossOffence = (int)Convert.ChangeType(DictionaryData["boss_attack"], typeof(int));
                    BossDefence = (int)Convert.ChangeType(DictionaryData["boss_defence"], typeof(int));

                    //GameObject.Find("BossName").GetComponent<Text>().text = BossName;
                }
                //BossName = DictionaryData["boss_name"].ToString();
                //BossHP = (int)Convert.ChangeType(DictionaryData["boss_hp"], typeof(int));
                //BossOffence = (int)Convert.ChangeType(DictionaryData["boss_attack"], typeof(int));
                //BossDefence = (int)Convert.ChangeType(DictionaryData["boss_defence"], typeof(int));

            }

            QuerySnapshot QuestData = await db.Collection("quests").GetSnapshotAsync();
            foreach (var document in QuestData.Documents)
            {
                Dictionary<string, object> DictionaryData = document.ToDictionary();
                foreach (KeyValuePair<string, object> i in DictionaryData)
                {
                    if(i.Key.Equals("boss_id") && i.Value.Equals(id))
                    {
                        QuestDiff = (int)Convert.ChangeType(DictionaryData["quest_difficult"], typeof(int));
                        QuestReward = (Dictionary<string, object>)Convert.ChangeType(DictionaryData["quest_reward"], typeof(Dictionary<string, object>));
                        break;
                    }
                    Debug.Log("Key is " + i.Key);
                    Debug.Log(i.Value);
                }
                //if (DictionaryData["boss_id"].ToString().Equals(id))
                //{
                //    QuestDiff = (int)Convert.ChangeType(DictionaryData["quest_difficult"], typeof(int));
                //    QuestReward = (Dictionary<string, object>)Convert.ChangeType(DictionaryData["quest_reward"], typeof(Dictionary<string, object>));
                //}
            }

            //ボスへのダメージを計算



            OperateCostomProperty.SetRoomCustomProperty("BossName", BossName);
            OperateCostomProperty.SetRoomCustomProperty("BossHP", BossHP);
            OperateCostomProperty.SetRoomCustomProperty("BossMaxHP", BossHP);
            OperateCostomProperty.SetRoomCustomProperty("BossAttack", BossOffence);
            OperateCostomProperty.SetRoomCustomProperty("BossDefence", BossDefence);
            OperateCostomProperty.SetRoomCustomProperty("QuestDiff", QuestDiff);
            OperateCostomProperty.SetRoomCustomProperty("QuestReward", QuestReward);

            //int difficulity = (int)OperateCostomProperty.GetRoomCustomProperty("QuestDiff");
            OperateCostomProperty.SetRoomCustomProperty("TrainingTime", 5);
            OperateCostomProperty.SetRoomCustomProperty("RestTime", 0);

        }
        Debug.Log("Finish SetQuestInfo");
    }
    
    public async static void SetTrainingOption()
    {
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("TrainingType", UnityEngine.Random.Range(0, 5));

        OperateCostomProperty.SetUserCustomProperty("Count", 0);

        //与えるダメージをリセット
        if (PhotonNetwork.IsMasterClient)
        {
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
            int BossHP = (int)OperateCostomProperty.GetRoomCustomProperty("BossHP") - (int)OperateCostomProperty.GetRoomCustomProperty("AllyAttackDamage");
            OperateCostomProperty.SetRoomCustomProperty("BossHP", BossHP);

            if (BossHP <= 0)
            {
                OperateCostomProperty.SetRoomCustomProperty("isBattle", false);
                Debug.Log("勝利しました");
                DefeatBoss();
                FinishBossBattle();
            }
        }
        Debug.Log("Finish AllyAttack");

    }

    // ボスの攻撃、味方のHPを減らす
    public static void BossAttack()
    {

        //カスタムプロパティを変更
        if (PhotonNetwork.IsMasterClient)
        {
            int AllyHP = (int)OperateCostomProperty.GetRoomCustomProperty("TotalHP") - BossOffence;
            OperateCostomProperty.SetRoomCustomProperty("TotalHP", AllyHP);
            if (AllyHP <= 0)
            {
                OperateCostomProperty.SetRoomCustomProperty("isBattle", false);
                Debug.Log("敗北しました");
                PhotonNetwork.LoadLevel("QuestResultLose");
            }
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
        PhotonNetwork.LoadLevel("QuestResultWin");
        //PhotonNetwork.Disconnect();
        Debug.Log("Finish FinishBossBattle");
        //SceneManager.LoadScene("QuestResult");

    }

}
