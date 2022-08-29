using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using TMPro;
using UnityEngine;

public class DatabaseOperation : MonoBehaviour
{
    
    public static async Task<List<Dictionary<string, object>>> GetData(string Collection, string id = "NoId")
    {
        var db = FirebaseFirestore.DefaultInstance;
        QuerySnapshot Data = await db.Collection(Collection).GetSnapshotAsync();
        List<Dictionary<string, object>> ReturnData = new List<Dictionary<string, object>>();

        foreach (var document in Data.Documents)
        {
            Dictionary<string, object> DictionaryData = document.ToDictionary();
            //条件指定
            id = "0";
            if (!id.Equals("NoId"))
            {
                if (document.Id.Equals(id))
                    ReturnData.Add(DictionaryData);
            } else
            {
                ReturnData.Add(DictionaryData);
            }

        }
        return ReturnData;
    }

    public static async void AddData(string Collection, string id, object AddData)
    {
        var db = FirebaseFirestore.DefaultInstance;

        // documentのなかを含めることでidを指定することが可能
        await db.Collection(Collection).Document(id).SetAsync(AddData);
        Debug.Log("Add Data");
    }



    public static async void UpdateData(string Collection, string id, object UpdateData)
    {
        var db = FirebaseFirestore.DefaultInstance;
        DocumentReference OriginData = db.Collection(Collection).Document(id);

        //データを更新できる以下の文で
        //await washingtonRef.UpdateAsync("Regions", FieldValue.Increment(50));

        await OriginData.UpdateAsync((IDictionary<string, object>)UpdateData);
        Debug.Log("Update Data");
    }

    public static async void DeleteData(string Collection, string id)
    {
        var db = FirebaseFirestore.DefaultInstance;
        DocumentReference DeleteData = db.Collection(Collection).Document(id);
        await DeleteData.DeleteAsync();

        Debug.Log("Delete Data");
    }
}
