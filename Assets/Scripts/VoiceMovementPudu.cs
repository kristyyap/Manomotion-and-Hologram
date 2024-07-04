using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Voicemovementpudu : MonoBehaviour
{
    public GameObject theModel;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("spinpudu", SpinPudu);
        actions.Add("walkpudu", WalkPudu);
        actions.Add("jumppudu", JumpPudu);

        keywordRecognizer  = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void SpinPudu()
    {
        theModel.GetComponent<Animator>().Play("SpinPudu");
    }

    public void WalkPudu()
    {
        theModel.GetComponent<Animator>().Play("WalkPudu");
    }

    public void JumpPudu()
    {
        theModel.GetComponent<Animator>().Play("JumpPudu");
    }

}