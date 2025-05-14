using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Button> buttons; // Assign buttons in inspector
    public List<Image> panels;   // Assign panels (UI background)
    public Color highlightColor = Color.yellow;
    public float delay = 0.6f;

    public GameObject failPanel;
    public GameObject winPanel;
    public GameObject itemSpawnner;

    public int sequenceLength = 5; // Maximum sequence length

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool playerTurn = false;

    void Start()
    {
        AddStep(); // Start with 1 step
        StartCoroutine(PlaySequence());
    }

    void AddStep()
    {
        sequence.Add(Random.Range(0, buttons.Count));
    }

    IEnumerator PlaySequence()
    {
        playerTurn = false;
        yield return new WaitForSeconds(1f);

        foreach (int index in sequence)
        {
            yield return StartCoroutine(FlashPanel(index));
            yield return new WaitForSeconds(delay);
        }

        playerIndex = 0;
        playerTurn = true;
    }

    IEnumerator FlashPanel(int index)
    {
        Image panel = panels[index];

        Color originalColor = panel.color;
        Color flashColor = highlightColor * 1.5f;
        flashColor.a = 1f;

        panel.color = flashColor;
        yield return new WaitForSeconds(0.4f);
        panel.color = originalColor;
    }

    public void OnButtonPressed(int index)
    {
        if (!playerTurn) return;

        if (index == sequence[playerIndex])
        {
            playerIndex++;

            if (playerIndex >= sequence.Count)
            {
                if (sequence.Count >= sequenceLength)
                {
                    Debug.Log("Victory! Game complete.");
                    StartCoroutine(HandleWin());
                }
                else
                {
                    AddStep(); // Add one more step and repeat
                    StartCoroutine(PlaySequence());
                }
            }
        }
        else
        {
            Debug.Log("Incorrect! Showing fail panel.");
            StartCoroutine(HandleFailure());
        }
    }

    IEnumerator HandleFailure()
    {
        playerTurn = false;

        if (failPanel != null)
            failPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (failPanel != null)
            failPanel.SetActive(false);

        sequence.Clear();     // Reset sequence
        AddStep();            // Start again from 1
        StartCoroutine(PlaySequence());
    }

    IEnumerator HandleWin()
    {
        playerTurn = false;

        if (winPanel != null)
            winPanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        if (winPanel != null)
            winPanel.SetActive(false);

        itemSpawnner.SetActive(true);
        Debug.Log(gameObject.name);
        gameObject.SetActive(false);
    }
}
