using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public Text text;

    public void SetTextWin() {
        text.text = "You Win !";
    }

    public void SetTextLose() {
        text.text = "You Lose !";
    }
}
