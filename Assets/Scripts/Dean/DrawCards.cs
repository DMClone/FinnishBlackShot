using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public List<GameObject> hand = new List<GameObject>();
    public int count;
    public bool stand;
    public TextMeshProUGUI playerText, gameOverText;
    public GameObject aceValueChoice;

    private Deck deck;
    [SerializeField] private Transform handAnchor;
    [SerializeField] private float cardSpacing = 1.5f;
    [SerializeField] private TextMeshProUGUI currentValueText;

    public Card currentCard;

    public Canvas canvas;

    private void Start()
    {
        deck = Deck.Instance;
        //GameObject canvas = Instantiate(canvasPrefab, transform.position, Quaternion.identity);
        //GetPlayerCanvasValues canvasValues = canvas.GetComponent<GetPlayerCanvasValues>();
        //playerText = canvasValues.playerText;
        //currentValueText = canvasValues.CurrentValueText;
        //aceValueChoice = canvasValues.aceValueObject;
        aceValueChoice.SetActive(false);
    }

    private void Update()
    {
        currentValueText.text = "Current Value: " + count;
    }

    public void DrawCard()
    {
        if (deck.cards.Count == 0) return;

        GameObject topCard = deck.cards[0];
        GameObject newCard = Instantiate(topCard, handAnchor);

        Vector3 offset = new Vector3(cardSpacing * hand.Count, 0, 0);
        newCard.transform.localPosition = offset;

        if (handAnchor != null)
        {
            newCard.transform.localRotation = Quaternion.identity;
        }

        currentCard = newCard.GetComponent<Card>();
        if (currentCard.value == 0)
        {
            aceValueChoice.SetActive(true);
            hand.Add(newCard);
            deck.cards.RemoveAt(0);
            return;
        }
        count += currentCard.value;
        hand.Add(newCard);
        deck.cards.RemoveAt(0);
    }

    public void NewStart()
    {
        foreach (var card in hand)
        {
            if (card != null)
                Destroy(card);
        }

        hand.Clear();
        count = 0;
        stand = false;
    }
}
