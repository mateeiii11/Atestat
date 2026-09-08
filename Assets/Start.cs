using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    public TextMeshProUGUI score, hp;
    public GameObject enemySpawner;
    public GameObject player;

    private void Awake()
    {
        score.gameObject.SetActive(false);
        hp.gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerShooting>().enabled = false;
        player.GetComponent<playerStatus>().enabled = false;
        enemySpawner.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad >= 3f)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<PlayerShooting>().enabled = true;
            player.GetComponent<playerStatus>().enabled = true;

        }
        if (Time.timeSinceLevelLoad >= 4f)
        {
            enemySpawner.gameObject.SetActive(true);
            score.gameObject.SetActive(true);
            hp.gameObject.SetActive(true);

        }
    }

}
