using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVPAddCount : MonoBehaviour
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
    int NowCount;
    public void IncCount()
    {
        if ((bool)OperateCostomProperty.GetRoomCustomProperty("isTraining"))
        {
            NowCount = (int)OperateCostomProperty.GetUserCustomProperty("Count");
            OperateCostomProperty.SetUserCustomProperty("Count", NowCount + 1);

            // TODOアニメーションを流す
            Animator AvatarAnimation = GameObject.Find("UserAvatar-1").GetComponent<Animator>();
            AvatarAnimation.SetTrigger("isActive");
            //AvatarAnimation.ResetTrigger("isActive");

            // TODO音声を流す
            if (NowCount % 11 == 0)
            {
                int RandomInt = UnityEngine.Random.Range(1, 2);
                if (RandomInt == 1)
                    Source.PlayOneShot(Support_1);
                else if (RandomInt == 2)
                    Source.PlayOneShot(Support_2);
            }

            if (NowCount == 10)
                Source.PlayOneShot(Count_10);
            else if (NowCount == 20)
                Source.PlayOneShot(Count_20);
            else if (NowCount == 30)
                Source.PlayOneShot(Count_30);
            else if (NowCount == 40)
                Source.PlayOneShot(Count_40);
            else if (NowCount == 50)
                Source.PlayOneShot(Count_50);
            else if (NowCount == 60)
                Source.PlayOneShot(Count_60);
            else if (NowCount == 70)
                Source.PlayOneShot(Count_70);
            else if (NowCount == 80)
                Source.PlayOneShot(Count_80);
            else if (NowCount == 90)
                Source.PlayOneShot(Count_90);
            else if (NowCount == 100)
                Source.PlayOneShot(Count_100);
        }
    }
}
