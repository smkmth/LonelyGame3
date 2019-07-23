using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("GameEvents/Conditional Game Event Trigger")]
public class ConditionalGameEventTrigger : AbstractGameEventTrigger
{


    public AbstractGameEventTrigger trueEventTrigger;
    public AbstractGameEventTrigger falseEventTrigger;

    private Condition conditionToTrigger;
    public bool conditionIsTrue =false;

    

    public override void Start()
    {
        base.Start();
        conditionToTrigger = GetComponent<Condition>();
    }


    public override void TriggerEvent()
    {
        conditionIsTrue =conditionToTrigger.CheckCondition();

        if (conditionIsTrue)
        {

            trueEventTrigger.TriggerEvent();
        }
        else
        {
            falseEventTrigger.TriggerEvent();

        }
    }
}
