using Playground2D.Game.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Playground2D.Canvas.StationStoreWindows
{
    public class StationStoreWindow : MonoBehaviour
    {
        [SerializeField] private GameStats _gameStats;

        // ���� � �������� ��� ������� ��������
        [SerializeField] private int _item1Price = 10;
        [SerializeField] private string _item1Name = "Item 1";
        [SerializeField] private int _item2Price = 20;
        [SerializeField] private string _item2Name = "Item 2";
        [SerializeField] private int _item3Price = 30;
        [SerializeField] private string _item3Name = "Item 3";
        [SerializeField] private int _item4Price = 40;
        [SerializeField] private string _item4Name = "Item 4";

        // ������ �� ������ � ����������
        [SerializeField] private Button _buyItem1Button;
        [SerializeField] private Button _buyItem2Button;
        [SerializeField] private Button _buyItem3Button;
        [SerializeField] private Button _buyItem4Button;
        [SerializeField] private Button _enterStoreButton;

        // ������ �� ����
        [SerializeField] private GameObject _insufficientFundsWindow;
        [SerializeField] private GameObject _confirmationWindow;
        [SerializeField] private GameObject _storingFormsWindow;

        // ������ �� ������ � ���� �������������
        [SerializeField] private Button _confirmYesButton;
        [SerializeField] private Button _confirmNoButton;

        // ������ �� ��������� ���� � ���� �������������
        [SerializeField] private Text _confirmationText;

        private int _selectedItemPrice;
        private Button _selectedItemButton;
        private string _selectedItemName;

        void Start()
        {
            // ��������� ������ ��� ������
            _buyItem1Button.onClick.AddListener(() => TryBuyItem(_item1Price, _item1Name, _buyItem1Button));
            _buyItem2Button.onClick.AddListener(() => TryBuyItem(_item2Price, _item2Name, _buyItem2Button));
            _buyItem3Button.onClick.AddListener(() => TryBuyItem(_item3Price, _item3Name, _buyItem3Button));
            _buyItem4Button.onClick.AddListener(() => TryBuyItem(_item4Price, _item4Name, _buyItem4Button));
            _enterStoreButton.onClick.AddListener(CloseInsufficientFundsWindow);
            _enterStoreButton.onClick.AddListener(EnterStore);

            _confirmYesButton.onClick.AddListener(ConfirmPurchase);
            _confirmNoButton.onClick.AddListener(CancelPurchase);

            // �������� ���� � ����� ��� ������
            _insufficientFundsWindow.SetActive(false);
            _confirmationWindow.SetActive(false);
            _storingFormsWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);
        }

        private void TryBuyItem(int price, string itemName, Button itemButton)
        {
            if (_gameStats._antiMaterial >= price)
            {
                // ���������� ���� �������������
                _selectedItemPrice = price;
                _selectedItemButton = itemButton;
                _selectedItemName = itemName;
                _confirmationText.text = $"������ {itemName} �� {price}?";
                _confirmationText.gameObject.SetActive(true);
                _confirmationWindow.SetActive(true);
            }
            else
            {
                // ���������� ���� � ������������ ������ ������
                _insufficientFundsWindow.SetActive(true);
            }
        }

        private void ConfirmPurchase()
        {
            // ��������� ���������� _antiMaterial
            _gameStats.SpendForm(antiMaterial: _selectedItemPrice);

            // �������� ���� ������������� � �����
            _confirmationWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);

            // ������� ������ �������
            Destroy(_selectedItemButton.gameObject);

            // ��������� ���� ������
            gameObject.SetActive(false);
        }

        private void CancelPurchase()
        {
            // �������� ���� ������������� � �����
            _confirmationWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);
        }

        public void CloseInsufficientFundsWindow()
        {
            // �������� ���� � ������������ ������ ������
            _insufficientFundsWindow.SetActive(false);
            gameObject.SetActive(false);
        }

        private void EnterStore()
        {
            // ���������� ���� StoringFormsWindow
            _storingFormsWindow.SetActive(true);
        }
    }
}
