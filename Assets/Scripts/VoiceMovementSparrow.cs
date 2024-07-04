using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Voicemovementsparrow : MonoBehaviour
{
    public GameObject theModel;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("spinsparrow", SpinSparrow);
        actions.Add("walksparrow", WalkSparrow);
        actions.Add("jumpsparrow", JumpSparrow);

        keywordRecognizer  = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void SpinSparrow()
    {
        theModel.GetComponent<Animator>().Play("SpinSparrow");
    }

    public void WalkSparrow()
    {
        theModel.GetComponent<Animator>().Play("WalkSparrow");
    }

    public void JumpSparrow()
    {
        theModel.GetComponent<Animator>().Play("JumpSparrow");
    }

}