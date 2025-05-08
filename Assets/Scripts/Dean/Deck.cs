using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck Instance { get; private set; } // Singleton instance

    public List<GameObject> cards = new List<GameObject>();

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Debug.LogError("Multiple Deck instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        ShuffleDeck();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShuffleDeck();
        }
    }

    public void ShuffleDeck()
    {
        cards = cards.OrderBy(x => Random.value).ToList();
    }
}