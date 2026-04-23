using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextwaveBtn : MonoBehaviour
{
   public void Click()
    {
        GameManager.Instance.StartNextWave();
    }
}
