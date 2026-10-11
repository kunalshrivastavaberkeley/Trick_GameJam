using UnityEngine;
using Yarn.Unity;

public class DialogueStarter : MonoBehaviour
{
    public DialogueRunner runner;

    public void Begin(string node)
    {
        if (!runner.IsDialogueRunning)
            runner.StartDialogue(node);
    }
}
