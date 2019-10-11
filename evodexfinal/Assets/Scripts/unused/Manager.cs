using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text UI;

    private void Update()
    {
        UI.text = Time.time.ToString();
    }
}
