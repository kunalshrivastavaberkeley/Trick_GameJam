using System;
using UnityEngine;
using Yarn.Unity;

public class ToiletClicker : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] int clicksNeeded = 500;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip flushSound;

    [SerializeField] int[] milestoneClicks = { 100, 250, 400, 499 };

    public void Click()
    {
        if (dialogueRunner.IsDialogueRunning) return;

        var storage = dialogueRunner.VariableStorage;

        storage.TryGetValue("$has_ring", out bool hasRing);
        if (hasRing)
        {
            dialogueRunner.StartDialogue("Toilet_Done");
            return;
        }

        storage.TryGetValue("$toilet_clicks", out float clicks);
        clicks++;
        storage.SetValue("$toilet_clicks", clicks);

        if (clicks == 1)
            dialogueRunner.StartDialogue("Toilet_Intro");
        else if (clicks >= clicksNeeded)
            dialogueRunner.StartDialogue("Toilet_Ring");
        else if (Array.IndexOf(milestoneClicks, (int)clicks) >= 0)
            dialogueRunner.StartDialogue("Toilet_Progress");

        audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(flushSound);
    }
}