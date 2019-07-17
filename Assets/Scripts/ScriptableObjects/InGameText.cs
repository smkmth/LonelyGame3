using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InGameText")]
public class InGameText : Item
{
    [TextArea]
    public string TextToDisplay;
}
