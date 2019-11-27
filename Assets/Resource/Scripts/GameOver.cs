using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public AudioClip Music = null;
    public AudioSource myAudio = null;

    public Image BGImage;
    public Text MSG;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Visual");
    }

    IEnumerator Visual()
    {
        myAudio.clip = Music;
        myAudio.Play();
        BGImage.color = new Color(128, 128, 128, 0);
        MSG.text = "";
        Color c;
        for (int n = 0; n < 10; n++)
        {
            Time.timeScale -= 0.1f;
            yield return new WaitForSecondsRealtime(0.08f);
        }
        BGImage.color = Color.white;
        yield return new WaitForSecondsRealtime(0.4f);
        for (int n = 0; n < 100; n++)
        {
            BGImage.color = Color.Lerp(BGImage.color, Color.grey, 0.1f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        for (int n = 0; n < 4; n++)
        {
            switch (n)
            {
                case 0:
                    MSG.text = "배";
                    break;
                case 1:
                    MSG.text = "배송";
                    break;
                case 2:
                    MSG.text = "배송 실";
                    break;
                case 3:
                    MSG.text = "배송 실패";
                    break;
            }
            yield return new WaitForSecondsRealtime(0.8f);
        }
        yield return new WaitForSecondsRealtime(2.0f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameScene");
    }
}
