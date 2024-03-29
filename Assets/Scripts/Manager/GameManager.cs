using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool onDrag;
    public int ActualScore;
    [HideInInspector]public int CurrentCardAmount;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(UIManager.instance.cursorTexture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(UIManager.instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }
    }
    void Start()
    {
        AddCardToHand(3);
        AddCardToHand(3);
        AddCardToHand(3);
        AddCardToHand(9);
        AddCardToHand(9);
        AddCardToHand(9);
    }
    public void AddCardToHand(int cardNumber)
    {
        FindObjectOfType<CardDrop>().cardsQueue.Enqueue(cardNumber);
    }
}
