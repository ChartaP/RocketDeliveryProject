using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        Camera.main.transform.GetComponent<AudioListener>().enabled = false;
    }

    private void OnDestroy()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.transform.GetComponent<AudioListener>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        Destroy(gameObject, 0.01f);
    }

    public void Mene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
