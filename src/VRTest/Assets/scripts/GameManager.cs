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

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool playerTurn = false;
    public GameObject failPanel;

    public GameObject winPanel; // Assign in Inspector
    private int roundCount = 0;
    public int maxRounds = 7;

    void Start()
    {
        AddStep();
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

        // Store original color
        Color originalColor = panel.color;

        // Create a brighter version of the current color
        Color flashColor = highlightColor * 1.5f;
        flashColor.a = 1f; // Keep full opacity

        // Set panel color to flash
        panel.color = flashColor;

        // Wait briefly
        yield return new WaitForSeconds(0.4f);

        // Restore original color
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
                roundCount++;

                if (roundCount >= maxRounds)
                {
                    Debug.Log("Victory! Game complete.");
                    StartCoroutine(HandleWin());
                }
                else
                {
                    AddStep();
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

        // Show fail panel
        if (failPanel != null)
            failPanel.SetActive(true);

        // Wait for 2 seconds (adjust as needed)
        yield return new WaitForSeconds(2f);

        // Hide fail panel
        if (failPanel != null)
            failPanel.SetActive(false);

        // Restart sequence
        sequence.Clear();
        AddStep();
        StartCoroutine(PlaySequence());
    }
    IEnumerator HandleWin()
    {
        playerTurn = false;

        if (winPanel != null)
            winPanel.SetActive(true);

        yield return null; // Optionally pause game logic or allow UI animation
    }


}
