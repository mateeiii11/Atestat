using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerStatus : MonoBehaviour
{
    public int hp = 5;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hpText;
    public int score = 0;

    private void Update()
    {
        if (hp <= 0)
            SceneManager.LoadScene("SampleScene");
        scoreText.text = "Score: " + score.ToString();
        hpText.text = "Hp: " + hp.ToString();
    }
}
