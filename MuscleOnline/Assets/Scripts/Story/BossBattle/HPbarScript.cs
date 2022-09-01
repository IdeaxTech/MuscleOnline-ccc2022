using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarScript : MonoBehaviour
{
    [SerializeField] Image BossHpImage;
    [SerializeField] Image AllyHpImage;
    float BossHP;
    float BossMaxHP;
    float BossHpDivision;

    float TotalHP;
    float TotalMaxHP;
    float AllyHpDivision;
    // Start is called before the first frame update
    void Start()
    {
        //BossHpImage = GetComponent<Image>();
        //AllyHpImage = GetComponent<Image>();
    }

    private void Update()
    {
        BossHP=(float)Convert.ChangeType(OperateCostomProperty.GetRoomCustomProperty("BossHP"), typeof(float));
        BossMaxHP=(float)Convert.ChangeType(OperateCostomProperty.GetRoomCustomProperty("BossMaxHP"), typeof(float));
        BossHpDivision = BossHP / BossMaxHP;
        UpdateBossHp(BossHpDivision);

        TotalHP=(float)Convert.ChangeType(OperateCostomProperty.GetRoomCustomProperty("TotalHP"), typeof(float));
        TotalMaxHP=(float)Convert.ChangeType(OperateCostomProperty.GetRoomCustomProperty("AllyMaxHP"), typeof(float));
        AllyHpDivision = TotalHP / TotalMaxHP;
        UpdateAllyHp(AllyHpDivision);
    }

    //fillAmountの値を変更する関数
    public void UpdateBossHp(float BossHp)
    {
        BossHpImage.fillAmount = BossHp;
    }

    //fillAmountの値を変更する関数
    public void UpdateAllyHp(float Allyhp)
    {
        AllyHpImage.fillAmount = Allyhp;
    }
}
