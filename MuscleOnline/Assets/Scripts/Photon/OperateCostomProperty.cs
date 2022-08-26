using Photon.Pun;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class OperateCostomProperty : MonoBehaviourPunCallbacks
{
    private static readonly Hashtable propsToSet = new Hashtable();
    static object temp = null;

    public static void SetRoomCustomProperty<Generic>(string key, Generic value)
    {
        Hashtable RoomHash = PhotonNetwork.CurrentRoom.CustomProperties;
        string true_key = (string)(object)key;

        if (RoomHash.TryGetValue(true_key, out temp))
        {
            propsToSet[true_key] = value;
            //PhotonNetwork.CurrentRoom.SetCustomProperties(propsToSet);

        }
        else
        {
            propsToSet.Add(true_key, value);
            //PhotonNetwork.CurrentRoom.SetCustomProperties(RoomHash);
        }
        //propsToSet[true_key] = value;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propsToSet);


        propsToSet.Clear();
    }

    public static object GetRoomCustomProperty<Generic>(Generic key)
    {
        Hashtable roomhash = PhotonNetwork.CurrentRoom.CustomProperties;
        string true_key = (string)(object)key;
        return PhotonNetwork.CurrentRoom.CustomProperties[true_key];

        //ルームにキーがあるか
        //if (roomhash.TryGetValue(true_key, out temp))
        //{
        //    return (object)PhotonNetwork.CurrentRoom.CustomProperties[true_key];
        //}
        //else
        //{
        //    propsToSet.Add(true_key, 0);
        //    PhotonNetwork.CurrentRoom.SetCustomProperties(propsToSet);
        //    GetRoomCustomProperty(true_key);
        //    return (object)0;
        //}
    }

    public static void SetUserCustomProperty<Generic>(string key, Generic value)
    {
        Hashtable UserHash = PhotonNetwork.LocalPlayer.CustomProperties;
        if (UserHash.TryGetValue(key, out temp))
        {
            propsToSet[key] = value;
        } else
        {
            propsToSet.Add(key, value);
            //PhotonNetwork.LocalPlayer.SetCustomProperties(UserHash);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);


        propsToSet.Clear();
    }

    public static object GetUserCustomProperty<Generic>(Generic key)
    {
        string true_key = (string)(object)key;
        return PhotonNetwork.LocalPlayer.CustomProperties[true_key];
    }

    public static bool isExistUserCustomProperty(Player player, string key)
    {
        Hashtable UserHash = player.CustomProperties;
        object temp = null;

        if (UserHash.TryGetValue(key, out temp))
            return true;
        else
            return false;
    }

}
