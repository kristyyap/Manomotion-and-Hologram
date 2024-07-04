using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Voicemovementmuskrat : MonoBehaviour
{
    public GameObject theModel;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("spinmuskrat", SpinMuskrat);
        actions.Add("walkmuskrat", WalkMuskrat);
        actions.Add("jumpmuskrat", JumpMuskrat);

        keywordRecognizer  = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void SpinMuskrat()
    {
        theModel.GetComponent<Animator>().Play("SpinMuskrat");
    }

    public void WalkMuskrat()
    {
        theModel.GetComponent<Animator>().Play("WalkMuskrat");
    }

    public void JumpMuskrat()
    {
        theModel.GetComponent<Animator>().Play("JumpMuskrat");
    }

}