using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class blInkIng_UI : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Update()
    {
        Blink();
    }

    void Blink()
    {
            float alpha = Mathf.PingPong(Time.time*255, 255);

            text.color = new Color32(255, 255, 255, (byte)alpha);
    }
}
