using System;
using UnityEngine;
using Yarn.Unity;

public class ToiletClicker : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] int clicksNeeded = 500;

    // Click counts that pop up a line of dialogue. Every other click is silent.
    [SerializeField] int[] milestoneClicks = { 100, 250, 400, 499 };

    void OnMouseDown()
    {
        // Don't count clicks while a conversation is on screen
        if (dialogueRunner.IsDialogueRunning) return;

        var storage = dialogueRunner.VariableStorage;

        // Already got the ring? Nothing left in there.
        storage.TryGetValue("$has_ring", out bool hasRing);
        if (hasRing)
        {
            dialogueRunner.StartDialogue("Toilet_Done");
            return;
        }

        // Count is stored in Yarn so it saves/loads with the rest of your variables
        storage.TryGetValue("$toilet_clicks", out float clicks);
        clicks++;
        storage.SetValue("$toilet_clicks", clicks);

        if (clicks == 1)
            dialogueRunner.StartDialogue("Toilet_Intro");
        else if (clicks >= clicksNeeded)
            dialogueRunner.StartDialogue("Toilet_Ring");
        else if (Array.IndexOf(milestoneClicks, (int)clicks) >= 0)
            dialogueRunner.StartDialogue("Toilet_Progress");

        // Optional: play a flush sound / wobble the toilet here for feedback on every click
    }
}