using Playground2D.Game.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Playground2D.Canvas.StationStoreWindows
{
    public class StationStoreWindow : MonoBehaviour
    {
        [SerializeField] private StationStore _stationStore;

        [SerializeField] private Button _buyItem1Button;
        [SerializeField] private Button _buyItem2Button;
        [SerializeField] private Button _buyItem3Button;
        [SerializeField] private Button _buyItem4Button;
        [SerializeField] private Button _enterStoreButton;

        [SerializeField] private Image _item2Image;
        [SerializeField] private Image _item3Image;
        [SerializeField] private Image _item4Image;

        [SerializeField] private GameObject _insufficientFundsWindow;
        [SerializeField] private GameObject _confirmationWindow;
        [SerializeField] private GameObject _storingFormsWindow;

        [SerializeField] private Button _confirmYesButton;
        [SerializeField] private Button _confirmNoButton;

        [SerializeField] private Text _confirmationText;

        [SerializeField] private GameObject _newItem1;
        [SerializeField] private GameObject _newItem2;
        [SerializeField] private GameObject _newItem3;
        [SerializeField] private GameObject _newItem4;

        [SerializeField] private GameObject _newItem5;//текст недоступен
        [SerializeField] private GameObject _newItem6;
        [SerializeField] private GameObject _newItem7;

        [SerializeField] private Text _item1PriceText;
        [SerializeField] private Text _item2PriceText;
        [SerializeField] private Text _item3PriceText;
        [SerializeField] private Text _item4PriceText;

        private int _selectedItemPrice;
        private Button _selectedItemButton;
        private StationStore.Item _selectedItem;

        private void Start()
        {
            if (_stationStore == null)
            {
                return;
            }

            if (_stationStore.items.Length >= 1 && _buyItem1Button != null && _item1PriceText != null)
            {
                _buyItem1Button.onClick.AddListener(() => TryBuyItem(_stationStore.items[0]));
                _item1PriceText.text = $"{_stationStore.items[0].price}";
            }
            if (_stationStore.items.Length >= 2 && _buyItem2Button != null && _item2PriceText != null)
            {
                _buyItem2Button.onClick.AddListener(() => TryBuyItem(_stationStore.items[1]));
                _item2PriceText.text = $"{_stationStore.items[1].price}";
            }
            if (_stationStore.items.Length >= 3 && _buyItem3Button != null && _item3PriceText != null)
            {
                _buyItem3Button.onClick.AddListener(() => TryBuyItem(_stationStore.items[2]));
                _item3PriceText.text = $"{_stationStore.items[2].price}";
            }
            if (_stationStore.items.Length >= 4 && _buyItem4Button != null && _item4PriceText != null)
            {
                _buyItem4Button.onClick.AddListener(() => TryBuyItem(_stationStore.items[3]));
                _item4PriceText.text = $"{_stationStore.items[3].price}";
            }

            if (_enterStoreButton != null)
            {
                _enterStoreButton.onClick.AddListener(CloseInsufficientFundsWindow);
                _enterStoreButton.onClick.AddListener(EnterStore);
            }

            if (_confirmYesButton != null)
            {
                _confirmYesButton.onClick.AddListener(ConfirmPurchase);
            }

            if (_confirmNoButton != null)
            {
                _confirmNoButton.onClick.AddListener(CancelPurchase);
            }

            if (_insufficientFundsWindow != null)
            {
                _insufficientFundsWindow.SetActive(false);
            }

            if (_confirmationWindow != null)
            {
                _confirmationWindow.SetActive(false);
            }

            if (_storingFormsWindow != null)
            {
                _storingFormsWindow.SetActive(false);
            }

            if (_confirmationText != null)
            {
                _confirmationText.gameObject.SetActive(false);
            }

            if (_item2Image != null)
            {
                _item2Image.gameObject.SetActive(true);
            }

            if (_item3Image != null)
            {
                _item3Image.gameObject.SetActive(true);
            }

            if (_item4Image != null)
            {
                _item4Image.gameObject.SetActive(true);
            }

            if (_newItem1 != null)
            {
                _newItem1.SetActive(false);
            }

            if (_newItem2 != null)
            {
                _newItem2.SetActive(false);
            }

            if (_newItem3 != null)
            {
                _newItem3.SetActive(false);
            }

            if (_newItem4 != null)
            {
                _newItem4.SetActive(false);
            }

            if (_newItem5 != null)
            {
                _newItem5.SetActive(true);
            }

            if (_newItem6 != null)
            {
                _newItem6.SetActive(true);
            }

            if (_newItem7 != null)
            {
                _newItem7.SetActive(true);
            }
        }

        private void TryBuyItem(StationStore.Item item)
        {
            if (item == null)
            {
                Debug.LogError("Item is null.");
                return;
            }

            if (GameStats.Instance._antiMaterial >= item.price)
            {
                _selectedItemPrice = item.price;
                _selectedItemButton = GetButtonForItem(item);
                _selectedItem = item;
                _confirmationText.text = $"Купить {item.name} за {item.price}?";
                _confirmationText.gameObject.SetActive(true);
                _confirmationWindow.SetActive(true);
            }
            else
            {
                _insufficientFundsWindow.SetActive(true);
            }
        }

        private Button GetButtonForItem(StationStore.Item item)
        {
            if (item == _stationStore.items[0])
            {
                return _buyItem1Button;
            }
            else if (item == _stationStore.items[1])
            {
                return _buyItem2Button;
            }
            else if (item == _stationStore.items[2])
            {
                return _buyItem3Button;
            }
            else if (item == _stationStore.items[3])
            {
                return _buyItem4Button;
            }
            return null;
        }

        private void ConfirmPurchase()
        {
            if (_selectedItem == null)
            {
                Debug.LogError("Selected item is null.");
                return;
            }

            GameStats.Instance.SpendForm(antiMaterial: _selectedItemPrice);

            _confirmationWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);

            Destroy(_selectedItemButton.gameObject);

            if (_selectedItem == _stationStore.items[0])
            {
                _item2Image.gameObject.SetActive(false);
                _newItem5.SetActive(false);
                _newItem1.SetActive(true);
            }
            else if (_selectedItem == _stationStore.items[1])
            {
                _item3Image.gameObject.SetActive(false);
                _newItem6.SetActive(false);
                _newItem2.SetActive(true);
            }
            else if (_selectedItem == _stationStore.items[2])
            {
                _item4Image.gameObject.SetActive(false);
                _newItem7.SetActive(false);
                _newItem3.SetActive(true);
            }
            else if (_selectedItem == _stationStore.items[3])
            {
                _newItem4.SetActive(true);
            }
        }

        private void CancelPurchase()
        {
            _confirmationWindow.SetActive(false);
            _confirmationText.gameObject.SetActive(false);
        }

        public void CloseInsufficientFundsWindow()
        {
            _insufficientFundsWindow.SetActive(false);
            gameObject.SetActive(false);
        }

        private void EnterStore()
        {
            _storingFormsWindow.SetActive(true);
        }
    }
}
