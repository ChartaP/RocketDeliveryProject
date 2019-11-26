using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Receipt : MonoBehaviour
{
    public AudioClip Fisrt = null;
    public AudioClip Second = null;
    public AudioSource myAudio = null;

    public Transform paperTrans;
    public Transform nameTrans;
    public Transform timerTrans;
    public Transform scoreTrans;
    public Transform rewardTrans;
    public Transform toBeContinuedTrans;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        Vector3 Pos = toBeContinuedTrans.localPosition;
        Pos.x = 900f;
        toBeContinuedTrans.localPosition = Pos;
        Pos = nameTrans.localPosition;
        Pos.x = 124f;
        nameTrans.localPosition = Pos;
        nameTrans.GetComponent<Text>().enabled = false;
        Pos = timerTrans.localPosition;
        Pos.x = 124f;
        timerTrans.localPosition = Pos;
        timerTrans.GetComponent<Text>().enabled = false;
        Pos = scoreTrans.localPosition;
        Pos.x = 124f;
        scoreTrans.localPosition = Pos;
        scoreTrans.GetComponent<Text>().enabled = false;
        Pos = rewardTrans.localPosition;
        Pos.x = 124f;
        rewardTrans.localPosition = Pos;
        rewardTrans.GetComponent<Text>().enabled = false;

        StartCoroutine("ReceiptVisual");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ReceiptVisual()
    {
        myAudio.clip = Fisrt;
        myAudio.time = 133;
        myAudio.Play();
        StartCoroutine("TextVisual");
        yield return new WaitForSecondsRealtime(4.0f);
        StartCoroutine("TobeContinueVisual");
        yield break;
    }

    IEnumerator TextVisual()
    {
        Vector3 Pos;

        nameTrans.GetComponent<Text>().enabled = true;
        for (int n = 0; n < 25; n++)
        {
            Pos = nameTrans.localPosition;
            Pos.x -= 4;
            nameTrans.localPosition = Pos;
            yield return new WaitForSecondsRealtime(0.003f);
        }
        timerTrans.GetComponent<Text>().enabled = true;
        for (int n = 0; n < 25; n++)
        {
            Pos = timerTrans.localPosition;
            Pos.x -= 4;
            timerTrans.localPosition = Pos;
            yield return new WaitForSecondsRealtime(0.003f);
        }
        scoreTrans.GetComponent<Text>().enabled = true;
        for (int n = 0; n < 25; n++)
        {
            Pos = scoreTrans.localPosition;
            Pos.x -= 4;
            scoreTrans.localPosition = Pos;
            yield return new WaitForSecondsRealtime(0.003f);
        }
        rewardTrans.GetComponent<Text>().enabled = true;
        for (int n = 0; n < 25; n++)
        {
            Pos = rewardTrans.localPosition;
            Pos.x -= 4;
            rewardTrans.localPosition = Pos;
            yield return new WaitForSecondsRealtime(0.003f);
        }
        yield break;
    }

    IEnumerator TobeContinueVisual()
    {
        Vector3 Pos;
        myAudio.Stop();
        myAudio.clip = Second;
        myAudio.Play();
        for (int n = 0; n < 36; n++)
        {
            Pos = toBeContinuedTrans.localPosition;
            Pos.x -= 27;
            toBeContinuedTrans.localPosition = Pos;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield break;
    }

    public void ToBeContinued()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        Destroy(gameObject, 0.1f);
    }
}
