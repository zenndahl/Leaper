using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButterflyController : ButterflyController
{
    public int bonusTime = 5;

    protected override void SetButterflyType()
    {
        butterflyType = Random.Range(3, 4);
    }

    protected override void Pontuate()
    {
        FindObjectOfType<SoundManager>().Play("BonusTime");
        EventManager.Instance.AddTime(bonusTime);
        EventManager.Instance.onTargetCaptured -= Pontuate;
        Destroy(gameObject);
    }
}
