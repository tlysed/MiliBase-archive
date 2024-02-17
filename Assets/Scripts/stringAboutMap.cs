using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stringAboutMap : MonoBehaviour
{
    [TextArea(3,10)][SerializeField] public string TextAbout;
    [TextArea(3, 10)][SerializeField] public string NeedAbout;
    [SerializeField] public bool need;
    [SerializeField] public int LoadInt;
    [SerializeField] public int needPoints;
}
