using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;

public class GetUserData : MonoBehaviour
{
    // Start is called before the first frame update
    public async static void GetUser()
    {
        if (PlayerPrefs.HasKey("UserID"))
        {
            UserInfo.UserId = PlayerPrefs.GetString("UserID");
            var db = FirebaseFirestore.DefaultInstance;
            QuerySnapshot UserData = await db.Collection("users").GetSnapshotAsync();
            foreach (var document in UserData.Documents)
            {
                if (document.Id.Equals(UserInfo.UserId))
                {
                    Dictionary<string, object> DictionaryData = document.ToDictionary();

                    UserInfo.UserName = DictionaryData["username"].ToString();

                    UserInfo.UserExp = (int)Convert.ChangeType(DictionaryData["user_exp"], typeof(int));
                    UserInfo.UserMaxExp = (int)Convert.ChangeType(DictionaryData["user_maxexp"], typeof(int));
                    UserInfo.UserLevel = (int)Convert.ChangeType(DictionaryData["user_level"], typeof(int));

                    UserInfo.UserHP = (int)Convert.ChangeType(DictionaryData["user_hp"], typeof(int));
                    UserInfo.UserAttack = (int)Convert.ChangeType(DictionaryData["user_attack"], typeof(int));
                    UserInfo.UserDefence = (int)Convert.ChangeType(DictionaryData["user_defence"], typeof(int));

                }
            }
        }
        else
        {
            Debug.Log("ユーザ情報が見つかりません");
        }
    }

}
