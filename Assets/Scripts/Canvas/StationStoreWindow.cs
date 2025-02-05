using Playground2D.Game.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Playground2D.Canvas.StationStoreWindows
{
    public class StationStoreWindow : MonoBehaviour
    {
        [SerializeField] private GameStats _gameStats;

        // Цены и названия для каждого предмета
        [SerializeField] private int _item1Price = 10;
        [SerializeField] private string _item1Name = "Item 1";
        [SerializeField] private int _item2Price = 20;
        [SerializeField] private string _item2Name = "Item 2";
        [SerializeField] private int _item3Price = 30;
        [SerializeField] private string _item3Name = "Item 3";
        [SerializeField] private int _item4Price = 40;
        [SerializeField] private string _item4Name = "Item 4";

        // Ссылки на кнопки в интерфейсе
        [SerializeField] private Button _buyItem1Button;
        [SerializeField] private Button _buyItem2Button;
        [SerializeField] private Button _buyItem3Button;
        [SerializeField] private Button _buyItem4Button;
        [SerializeField] private Button _enterStoreButton;

        // Ссылки на окна
        [SerializeField] private GameObject _insufficientFundsWindow;
        [SerializeField] private GameObject _confirmationWindow;
        [SerializeField] private GameObject _storingFormsWindow;

        // Ссылки на кнопки в окне подтверждения
        [SerializeField] private Button _confirmYesButton;
        [SerializeField] private Button _confirmNoButton;

        // Ссылка на текстовое поле в окне подтверждения
        [SerializeField] private Text _confirmationText;

        private int _selectedItemPrice;
        private Button _selectedItemButton;
        private string _selectedItemName;

        void Start()
        {
            // Назначаем методы для кнопок
            _buyItem1Button.onClick.AddListener(() => TryBuyItem(_item1Price, _item1Name, _buyItem1Button));
            _buyItem2Button.onClick.AddListener(() => TryBuyItem(_item2Price, _item2Name, _buyItem2Button));
            _buyItem3Button.onClick.AddListener(() => TryBuyItem(_item3Price, _item3Name, _buyItem3Button));
            _buyItem4Button.onClick.AddListener(() => TryBuyItem(_item4Price, _item4Name, _buyItem4Button));
            _enterStoreButton.onClick.AddListener(CloseInsufficientFundsWindow);
            _enterStoreButton.onClick.AddListener(EnterStore);

            _confirmYesButton.onClick.AddListener(ConfirmPurchase);
            _confirmNoButton.onClick.AddListener(CancelPurchase);

            // Скрываем окна и текст при старте
            _insufficientFundsWindow.SetActive(false);
            _confirmationWindow.SetActive(false);
            _storingFormsWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);
        }

        private void TryBuyItem(int price, string itemName, Button itemButton)
        {
            if (_gameStats._antiMaterial >= price)
            {
                // Показываем окно подтверждения
                _selectedItemPrice = price;
                _selectedItemButton = itemButton;
                _selectedItemName = itemName;
                _confirmationText.text = $"Купить {itemName} за {price}?";
                _confirmationText.gameObject.SetActive(true);
                _confirmationWindow.SetActive(true);
            }
            else
            {
                // Показываем окно с предложением купить монеты
                _insufficientFundsWindow.SetActive(true);
            }
        }

        private void ConfirmPurchase()
        {
            // Уменьшаем количество _antiMaterial
            _gameStats.SpendForm(antiMaterial: _selectedItemPrice);

            // Скрываем окно подтверждения и текст
            _confirmationWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);

            // Удаляем кнопку покупки
            Destroy(_selectedItemButton.gameObject);

            // Закрываем весь объект
            gameObject.SetActive(false);
        }

        private void CancelPurchase()
        {
            // Скрываем окно подтверждения и текст
            _confirmationWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);
        }

        public void CloseInsufficientFundsWindow()
        {
            // Скрываем окно с предложением купить монеты
            _insufficientFundsWindow.SetActive(false);
            gameObject.SetActive(false);
        }

        private void EnterStore()
        {
            // Показываем окно StoringFormsWindow
            _storingFormsWindow.SetActive(true);
        }
    }
}
