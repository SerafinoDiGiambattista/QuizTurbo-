using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : OverTime
{
    public CountDown(SComponent component, string param, int val) : base(component) 
    {
        parameter = param;
        valuePerSecond = val;
    }

    public override void Activate()
    {
        //Debug.Log("para : "+parameter+" second: "+valuePerSecond);
        c.ReduceComponent(parameter, valuePerSecond);
    }

    public void DoCountdown()
    {
        Activate();
    }
}
