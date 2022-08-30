using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCount : MonoBehaviour
{
    [SerializeField] AudioClip Count_10;
    [SerializeField] AudioClip Count_20;
    [SerializeField] AudioClip Count_30;
    [SerializeField] AudioClip Count_40;
    [SerializeField] AudioClip Count_50;
    [SerializeField] AudioClip Count_60;
    [SerializeField] AudioClip Count_70;
    [SerializeField] AudioClip Count_80;
    [SerializeField] AudioClip Count_90;
    [SerializeField] AudioClip Count_100;
    [SerializeField] AudioClip Support_1;
    [SerializeField] AudioClip Support_2;

    [SerializeField] AudioSource Source;

    Animator TrainingAnimator;

    int NowCount;
    public void IncCount()
    {
        if ((bool)OperateCostomProperty.GetRoomCustomProperty("isTraining"))
        {
            NowCount = (int)OperateCostomProperty.GetUserCustomProperty("Count");
            OperateCostomProperty.SetUserCustomProperty("Count", NowCount + 1);
            OperateCostomProperty.SetUserCustomProperty("TotalCount", (int)OperateCostomProperty.GetUserCustomProperty("TotalCount") + 1);

            OperateCostomProperty.SetRoomCustomProperty("AllyAttackDamage", (int)OperateCostomProperty.GetRoomCustomProperty("AllyAttackDamage") + BossBattleScript.damage);

            // TODOアニメーションを流す
<<<<<<< HEAD
            DoAnimation.StartAnimation();
=======
            
>>>>>>> refs/remotes/origin/master

            // 音声を流す
            if (NowCount % 7 == 0)
            {
                int RandomInt = UnityEngine.Random.Range(1, 2);
                if (RandomInt == 1)
                    Source.PlayOneShot(Support_1);
                else if (RandomInt == 2)
                    Source.PlayOneShot(Support_2);
            }

            if (NowCount == 9)
                Source.PlayOneShot(Count_10);
            else if (NowCount == 19)
                Source.PlayOneShot(Count_20);
            else if (NowCount == 29)
                Source.PlayOneShot(Count_30);
            else if (NowCount == 39)
                Source.PlayOneShot(Count_40);
            else if (NowCount == 49)
                Source.PlayOneShot(Count_50);
            else if (NowCount == 59)
                Source.PlayOneShot(Count_60);
            else if (NowCount == 69)
                Source.PlayOneShot(Count_70);
            else if (NowCount == 79)
                Source.PlayOneShot(Count_80);
            else if (NowCount == 89)
                Source.PlayOneShot(Count_90);
            else if (NowCount == 99)
                Source.PlayOneShot(Count_100);
            TrainingAnimator.ResetTrigger("isActive");
        }
    }
}
