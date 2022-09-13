using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetName : MonoBehaviour
{
    public TMP_Text me;
    public void Set(string set) 
    {
        me.text = set;
    }
}
