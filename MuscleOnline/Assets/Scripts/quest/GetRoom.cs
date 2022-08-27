using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using TMPro;

public class GetRoom : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        string id = "1rrPh4Kl8N0U3FYEcPKv";
        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot RoomData = await db.Collection("rooms").GetSnapshotAsync();
        Debug.Log("called!");
        int count = 0;
        foreach (var document in RoomData.Documents)
        {
            Dictionary<string, object> DictionaryData = document.ToDictionary();
            if (DictionaryData["quest_id"].ToString() == id)
            {
                Debug.Log("call2");
                if ((bool)Convert.ChangeType(DictionaryData["is_open"], typeof(bool)) == true)
                {
                    count++;
                    GameObject CloneObject = (GameObject)Resources.Load("RoomObject");
                    CloneObject.name = "RoomObject-" + count.ToString();
                    Instantiate(CloneObject, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);

                    GameObject RoomObject = GameObject.Find("RoomObject-" + count.ToString() + "(Clone)");

                    GameObject RoomName = RoomObject.transform.Find("RoomName").gameObject;
                    GameObject RoomPlayers = RoomObject.transform.Find("RoomPlayers").gameObject;

                    RoomName.GetComponent<TMP_Text>().text = DictionaryData["room_name"].ToString();
                    RoomPlayers.GetComponent<TMP_Text>().text = DictionaryData["now_player"].ToString() + " / " + DictionaryData["max_player"].ToString();

                    Debug.Log((int)Convert.ChangeType(DictionaryData["now_player"], typeof(int)));
                    Debug.Log((int)Convert.ChangeType(DictionaryData["max_player"], typeof(int)));
                    Debug.Log(DictionaryData["room_name"].ToString());
                    Debug.Log(document.Id);

                    //NowPlayers = (int)Convert.ChangeType(DictionaryData["now_player"], typeof(int));
                    //MaxPlayers = (int)Convert.ChangeType(DictionaryData["max_player"], typeof(int));
                    //RoomName = DictionaryData["room_name"].ToString();
                }

            }
        }
    }

}
