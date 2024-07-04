using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Voicemovementcolobus : MonoBehaviour
{
    public GameObject theModel;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("spincolobus", SpinColobus);
        actions.Add("walkcolobus", WalkColobus);
        actions.Add("jumpcolobus", JumpColobus);

        keywordRecognizer  = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void SpinColobus()
    {
        theModel.GetComponent<Animator>().Play("SpinColobus");
    }

    public void WalkColobus()
    {
        theModel.GetComponent<Animator>().Play("WalkColobus");
    }

    public void JumpColobus()
    {
        theModel.GetComponent<Animator>().Play("JumpColobus");
    }

}