using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Float Variable", fileName = " Float Variable")]
public class Variables : ScriptableObject
{
    public float Value;

   public void SetValue(float value)
   {
        Value = value;
   }

   public void ApplyChange(float changeAmount)
   {
        Value += changeAmount;
   }
}
