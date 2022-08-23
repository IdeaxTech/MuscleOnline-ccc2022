using Photon.Pun;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;

public class BossBattleScript : MonoBehaviourPunCallbacks
{
    [SerializeField] static int damage;
    public static GameObject ReadyBtn;

    void Start()
    {
        ReadyBtn = GameObject.FindWithTag("ReadyBtn");
    }

    public static async void CreateDelay(int delay)
    {
        await Task.Delay(delay);
        //Thread.Sleep(1000);
    }

    public static void BossBattle()
    {
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("TotalHP", 0);

        // クエスト情報、ボス情報、筋トレ時間、休憩時間を設定
        SetQuestInfo();

        // 味方HPを合算
        OperateCostomProperty.SetUserCustomProperty("MyHP", 100);

        // ボス登場アニメーション
        StartAnimation();

        OperateCostomProperty.SetUserCustomProperty("TotalCount", 0);

        // バトルの開始
        if (PhotonNetwork.IsMasterClient)
            OperateCostomProperty.SetRoomCustomProperty("isBattle", true);
    }

    public static void SetQuestInfo()
    {
        // TODO:データベースからクエスト情報を取得

        damage = 10;

        // カスタムプロパティに代入
        if (PhotonNetwork.IsMasterClient)
        {
            //攻撃力とボスの防御力によって与えるダメージを計算

            OperateCostomProperty.SetRoomCustomProperty("BossHP", 100);
            OperateCostomProperty.SetRoomCustomProperty("BossAttack", 10);
            OperateCostomProperty.SetRoomCustomProperty("QuestDiff", 5);
            OperateCostomProperty.SetRoomCustomProperty("QuestReward", 5);

            int difficulity = (int)OperateCostomProperty.GetRoomCustomProperty("QuestDiff");
            OperateCostomProperty.SetRoomCustomProperty("TrainingTime", 5);
            OperateCostomProperty.SetRoomCustomProperty("RestTime", 0);
        }
        Debug.Log("Finish SetQuestInfo");
    }


    public static void SetTotalHP()
    {
        
        // 合算HPをカスタムプロパティに保存
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    int TotalHP = 0;
        //    foreach (var player in PhotonNetwork.PlayerList)
        //    {
        //        TotalHP += (int)PhotonNetwork.LocalPlayer.CustomProperties["MyHP"];
               
        //    }

        //    OperateCostomProperty.SetRoomCustomProperty("TotalHP", 100);
        //    GameObject.FindWithTag("TotalHP").GetComponent<TMP_Text>().text = TotalHP.ToString();
        //}
        
        Debug.Log("Finish SetTotalHP");
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
