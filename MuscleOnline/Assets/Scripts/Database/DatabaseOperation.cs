using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseOperation : MonoBehaviour
{
    
    string input;
    string id = "qvMC-hRHlKz1]<Fz";
    public static async void GetData()
    {
        
        var snapshot2 = await FirebaseFirestore.DefaultInstance.Collection("users").GetSnapshotAsync();
        

        foreach (var document in snapshot2.Documents)
        {
            //条件指定
            string id = "0";
            if (document.Id.Equals(id))
            {

            }

            Dictionary<string, object> data = document.ToDictionary();
            Debug.Log(data["username"]);
        }
    }

    public static async void AddData(string Collection, string id, object AddData)
    {
        var db = FirebaseFirestore.DefaultInstance;

        // documentのなかを含めることでidを指定することが可能
        await db.Collection(Collection).Document(id).SetAsync(AddData);
        Debug.Log("Add Data");
    }



    public async void UpdateData()
    {
        var db = FirebaseFirestore.DefaultInstance;
        input = GameObject.FindWithTag("InputName").GetComponent<TMP_InputField>().text;
        DocumentReference OriginData = db.Collection("users").Document(id);

        Dictionary<string, object> UpdateData = new Dictionary<string, object>()
        {
            {"username", input},
            {"update_at", Timestamp.GetCurrentTimestamp()}
        };

        //データを更新できる以下の文で
        //await washingtonRef.UpdateAsync("Regions", FieldValue.Increment(50));

        await OriginData.UpdateAsync(UpdateData);
        Debug.Log("Update Data");
    }

    public async void DeleteData()
    {
        var db = FirebaseFirestore.DefaultInstance;

        DocumentReference DeleteData = db.Collection("users").Document(id);
        await DeleteData.DeleteAsync();

        Debug.Log("Delete Data");
    }
}
