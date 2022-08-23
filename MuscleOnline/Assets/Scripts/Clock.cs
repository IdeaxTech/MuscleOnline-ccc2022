using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

    Image clockImage;

    // Start is called before the first frame update
    private void Start()
    {
        //時計の画像の取得
        clockImage = GetComponent<Image>();
    }

    //fillAmountの値を変更する関数
    public void UpdateClock(float time)
    {
        clockImage.fillAmount = time;
    }
}
