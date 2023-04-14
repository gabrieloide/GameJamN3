using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Texture2D cursorDefault, cursorTexture;
    [Space]
    public TMP_Text waveText, scoreText;
    [Space]
    public bool ShowTowerSlot;
    public GameObject TowerSlotAnimation;
    public Vector3 offset;
    [Space]
    public GameObject CardStats;
    [HideInInspector] public GameObject cardInstantiate;
    public GameObject Canvas2;
    [Space]
    [Header("Last Card")]
    public GameObject LastPosCard;
    public float TimeLastPosCard;
    [Space]
    [Header("Life Turret")]
    [SerializeField] Slider LifeSlider;
    [Space]
    [Header("Deck")]
    public LeanTweenType TweenDeckIn;
    public LeanTweenType TweenDeckOut;
    public GameObject Deck;
    public float posInCamera;
    public float TimeMovement;
    float currentLife()
    {
        float ActualLife = TowerPlayer.instance.life/100;
        return ActualLife;
    }
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        int score = GameManager.instance.ActualScore;
        string scoreString = score.ToString();
        scoreString = scoreString.Substring(Mathf.Max(0, scoreString.Length - 6)).PadLeft(6, '0');

        scoreText.text = $"Score: {scoreString}";
        waveText.text = $"Wave: {WaveManager.instance.Wave.ToString()}";
        LifeSlider.value = currentLife();
        showTowerSlotAnimation();
        ShowDeck();
    }
    void showTowerSlotAnimation()
    {
        if (ShowTowerSlot)
        {
            TowerSlotAnimation.SetActive(true);
            TowerSlotAnimation.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - offset;
        }
        else
            TowerSlotAnimation.SetActive(false);
    }
    public void ShowCardBox(string _name, string _description, Vector3 TC)
    {
        if (cardInstantiate == null)
        {
            //Al hacer click derecho mostrar descripcion y nombre de la carta

            cardInstantiate = Instantiate(CardStats, Canvas2.transform);
            cardInstantiate.transform.position = TC;
            FindObjectOfType<ChangeCardText>().instantiateStats(_name, _description);
        }
    }
    public void ShowLastCardPosition(Vector3 CardPos, bool show)
    {
        if (show)
        {
            LastPosCard.SetActive(show);
            //Mostrar Ultima posicion de la carta al agarrarla

            Vector3 offset = new Vector3(0, 0.01f,0);
            LastPosCard.transform.position = CardPos - offset;
            LeanTween.scale(LastPosCard, Vector3.one * 15, TimeLastPosCard).setEaseOutBack();
        }
        else
        {
            LastPosCard.SetActive(show);
            //No mostrar ultima posicion de la carta al agarrarla
            LastPosCard.transform.localScale = Vector3.zero;
        }
    }
    public void ShowDeck()
    {
        if (CardDrop.instance.cardsQueue.Count > 0)
        {
            //Mostrar deck para tomar carta
            this.Deck.SetActive(true);
        }
    }
}
