using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButterflyController : ButterflyController
{
    public int bonusTime = 5;
    private int debuffTime = 0;
    
    private void Update()
    {
        //if (debuffTime == 0 && playerController.ReturnPoints() > 50)
        //{
        //    bonusTime--;
        //    debuffTime++;
        //}

        //if (debuffTime == 1 && playerController.ReturnPoints() > 100)
        //{
        //    bonusTime--;
        //    debuffTime++;
        //}

        //if(debuffTime == 2 && playerController.ReturnPoints() > 150)
        //{
        //    bonusTime--;
        //    debuffTime++;
        //}

        //if (debuffTime == 3 && playerController.ReturnPoints() > 200)
        //{
        //    bonusTime--;
        //    debuffTime++;
        //}
    }

    protected override void SetButterflyType()
    {
        butterflyType = Random.Range(3, 4);
    }

    protected override void Pontuate()
    {
        EventManager.Instance.AddTime(bonusTime);
        EventManager.Instance.onTargetCaptured -= Pontuate;

        SoundManager.PlaySound(SoundManager.Sound.BonusTime);
    }
}
