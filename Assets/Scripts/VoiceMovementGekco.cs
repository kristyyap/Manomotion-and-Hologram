using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Voicemovementgekco : MonoBehaviour
{
    public GameObject theModel;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("spingekco", SpinGekco);
        actions.Add("walkgekco", WalkGekco);
        actions.Add("jumpgekco", JumpGekco);

        keywordRecognizer  = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech){
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void SpinGekco()
    {
        theModel.GetComponent<Animator>().Play("SpinGekco");
    }

    public void WalkGekco()
    {
        theModel.GetComponent<Animator>().Play("WalkGekco");
    }

    public void JumpGekco()
    {
        theModel.GetComponent<Animator>().Play("JumpGekco");
    }

}