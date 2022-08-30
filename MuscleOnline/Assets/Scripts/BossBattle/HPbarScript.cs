using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarScript : MonoBehaviour
{
    [SerializeField] Image BossHpImage;
    [SerializeField] Image AllyHpImage;
    int BossHP;
    int BossMaxHP;
    int BossHpDivision;

    int TotalHP;
    int TotalMaxHP;
    int AllyHpDivision;
    // Start is called before the first frame update
    void Start()
    {
        BossHpImage = GetComponent<Image>();
        AllyHpImage = GetComponent<Image>();
    }

    private void Update()
    {
        BossHP=(int)OperateCostomProperty.GetRoomCustomProperty("BossHP");
        BossMaxHP=(int)OperateCostomProperty.GetRoomCustomProperty("BossMaxHP");
        BossHpDivision = BossHP / BossMaxHP;
        UpdateBossHp(BossHpDivision);

        TotalHP=(int)OperateCostomProperty.GetRoomCustomProperty("TotalHP");
        TotalMaxHP=(int)OperateCostomProperty.GetRoomCustomProperty("AllyMaxHP");
        AllyHpDivision = TotalHP / TotalMaxHP;
        UpdateAllyHp(AllyHpDivision);
    }

    //fillAmountの値を変更する関数
    public void UpdateBossHp(int BossHp)
    {
        BossHpImage.fillAmount = BossHp;
    }

    //fillAmountの値を変更する関数
    public void UpdateAllyHp(int Allyhp)
    {
        AllyHpImage.fillAmount = Allyhp;
    }
}
