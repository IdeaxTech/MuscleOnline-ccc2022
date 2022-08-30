using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Firestore;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GetRoom : MonoBehaviourPunCallbacks
{
    void Start()
    {
        DisplayRoom();
    }

    public static async void DisplayRoom()
    {
        string id = "1rrPh4Kl8N0U3FYEcPKv";
        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot RoomData = await db.Collection("rooms").GetSnapshotAsync();
        int count = 0;
        foreach (var document in RoomData.Documents)
        {
            Dictionary<string, object> DictionaryData = document.ToDictionary();
            if (DictionaryData["quest_id"].ToString() == id)
            {
                if ((bool)Convert.ChangeType(DictionaryData["is_open"], typeof(bool)) == true)
                {
                    count++;
                    GameObject CloneObject = (GameObject)Resources.Load("RoomObject");
                    CloneObject.name = "RoomObject-" + count.ToString();
                    GameObject CreateObject;

                    if (count == 1)
                    {
                        CreateObject = Instantiate(CloneObject, new Vector3(-190.0f, 70.0f, 0.0f), Quaternion.identity);
                    }
                    else if (count == 2)
                    {
                        CreateObject = Instantiate(CloneObject, new Vector3(282.0f, 70.0f, 0.0f), Quaternion.identity);
                    }
                    else if (count == 3)
                    {
                        CreateObject = Instantiate(CloneObject, new Vector3(751.0f, 70.0f, 0.0f), Quaternion.identity);
                    }
                    else if (count == 4)
                    {
                        CreateObject = Instantiate(CloneObject, new Vector3(-190.0f, -128.0f, 0.0f), Quaternion.identity);
                    }
                    else if (count == 5)
                    {
                        CreateObject = Instantiate(CloneObject, new Vector3(282.0f, -128.0f, 0.0f), Quaternion.identity);
                    }
                    else if (count == 6)
                    {
                        CreateObject = Instantiate(CloneObject, new Vector3(751.0f, -128.0f, 0.0f), Quaternion.identity);
                    }
                    else
                    {
                        CreateObject = Instantiate(CloneObject, new Vector3(-190.0f, -128.0f, 0.0f), Quaternion.identity);
                    }

                    //GameObject RoomObject = GameObject.Find("RoomObject-" + count.ToString() + "(Clone)");

                    GameObject RoomName = CreateObject.transform.Find("RoomName").gameObject;
                    GameObject RoomPlayers = CreateObject.transform.Find("RoomPlayers").gameObject;
                    GameObject RoomId = CreateObject.transform.Find("RoomId").gameObject;

                    RoomName.GetComponent<TMP_Text>().text = DictionaryData["room_name"].ToString();
                    RoomPlayers.GetComponent<TMP_Text>().text = DictionaryData["now_player"].ToString() + " / " + DictionaryData["max_player"].ToString();
                    RoomId.GetComponent<TMP_Text>().text = document.Id.ToString();

                    //Debug.Log((int)Convert.ChangeType(DictionaryData["now_player"], typeof(int)));
                    //Debug.Log((int)Convert.ChangeType(DictionaryData["max_player"], typeof(int)));
                    //Debug.Log(DictionaryData["room_name"].ToString());
                    //Debug.Log(document.Id);
                }

            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        GameObject[] RoomObjects = GameObject.FindGameObjectsWithTag("RoomObject");
        foreach (GameObject RoomObject in RoomObjects)
        {
            Destroy(RoomObject);
        }

        Dictionary<string, object> UpdateRoomData = new Dictionary<string, object>
        {
            { "is_open", false }
        };
        DatabaseOperation.UpdateData("rooms", RoomOperation.RoomId, UpdateRoomData);
        Debug.Log($"ルームへの参加に失敗しました: {message}");
        DisplayRoom();
    }

}
