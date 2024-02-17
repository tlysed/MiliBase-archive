using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class timeline : MonoBehaviour
{
    [SerializeField] private PlayableAsset goodEndClip;
    [SerializeField] private PlayableAsset badEndClip;
    private PlayableDirector pd;

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }
    public void goodEndStart()
    {
        pd.playableAsset = goodEndClip;
        pd.Play();
    }
    public void badEndStart()
    {
        pd.playableAsset = badEndClip;
        pd.Play();
    }
}
