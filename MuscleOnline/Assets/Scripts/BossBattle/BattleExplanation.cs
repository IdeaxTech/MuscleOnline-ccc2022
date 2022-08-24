using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExplanation : MonoBehaviour
{
   
    [SerializeField] GameObject bossExplanationModal;
     

    void Start()
    {
        //BackToMenuメソッドを呼び出す
        OpenbossExplanationModal();
        
    }

    public void OpenbossExplanationModal()
    {
        bossExplanationModal.SetActive(true);
    }

    public void onClickOkButton()
    {
        bossExplanationModal.SetActive(false);
    }

}
