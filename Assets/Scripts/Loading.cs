using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Transform text;
    public Text textbox;
    public string[] lines;
    public ArrayList loadingLines = new ArrayList();
    void Start()
    {
        foreach(string i in lines){
            loadingLines.Add(i);
        }
        loadingLines.Add("Scene '" + PlayerPrefs.GetString("night", "Night1") + "' Initialised");
        StartCoroutine(addlines());
    }
    IEnumerator addlines()
    {
        yield return new WaitForSeconds(Random.Range(0.001f, 0.8f));
        string newline = loadingLines[Random.Range(0, loadingLines.Count)].ToString();
        textbox.text = textbox.text + "\n" + newline;
        text.localPosition = new Vector3(text.localPosition.x, text.localPosition.y + 14.9f, text.localPosition.z);
        loadingLines.Remove(newline);
        if(loadingLines.Count != 0){
            StartCoroutine(addlines());
        }else{
            yield return new WaitForSeconds(2f);
            SceneManager.LoadSceneAsync(PlayerPrefs.GetString("night", "Night1"));
        }
    }
}