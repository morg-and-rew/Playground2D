using Playground2D.Game.Stats;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Playground2D.Canvas.ResourceExtractionWindows
{
    public class ResourceExtractionWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Text _resourceText;
        [SerializeField] private Text _confirmationText;
        [SerializeField] private Text _resourceNameText;
        [SerializeField] private Text _resourcePriceText;
        [SerializeField] private Text _stationNameText;
        [SerializeField] private Text _totalCostText;
        [SerializeField] private Image _stationIcon;

        [SerializeField] private Button _increaseButton;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _checkAntimatterButton;
        [SerializeField] private Button _confirmYesButton;
        [SerializeField] private Button _confirmNoButton;
        [SerializeField] private Button _buyMoreAntimatterButton;

        [SerializeField] private GameObject _confirmationPanel;
        [SerializeField] private GameObject _insufficientFundsPanel;

        // Ссылки на объекты станций
        [SerializeField] private GameObject _station1;
        [SerializeField] private GameObject _station2;
        [SerializeField] private GameObject _station3;
        [SerializeField] private GameObject _station4;

        private int _selectedAmount = 0;
        private bool _isIncreasePressed = false;
        private string _resourceName;
        private string _stationName;
        private int _resourcePrice;

        private void Start()
        {
            if (_increaseButton != null)
            {
                _increaseButton.onClick.AddListener(() => ChangeValue(ref _selectedAmount, _resourceText, true));
            }
            if (_decreaseButton != null)
            {
                _decreaseButton.onClick.AddListener(() => ChangeValue(ref _selectedAmount, _resourceText, false));
            }
            if (_checkAntimatterButton != null)
            {
                _checkAntimatterButton.onClick.AddListener(CheckAntimatter);
            }
            if (_confirmYesButton != null)
            {
                _confirmYesButton.onClick.AddListener(ConfirmSelection);
            }
            if (_confirmNoButton != null)
            {
                _confirmNoButton.onClick.AddListener(CancelSelection);
            }
            if (_buyMoreAntimatterButton != null)
            {
                _buyMoreAntimatterButton.onClick.AddListener(BuyMoreAntimatter);
            }
        }

        private void OnDisable()
        {
            ResetSelection();
        }

        public void Init(string stationName, string resourceName, int resourcePrice, Sprite icon)
        {
            _stationName = stationName;
            _resourceName = resourceName;
            _resourcePrice = resourcePrice;

            if (_resourceNameText != null)
            {
                _resourceNameText.text = $"Ресурс: {_resourceName}";
            }

            if (_resourcePriceText != null)
            {
                _resourcePriceText.text = $"Цена: {_resourcePrice}";
            }

            if (_stationNameText != null)
            {
                _stationNameText.text = $"Станция: {_stationName}";
            }

            if (_stationIcon != null)
            {
                _stationIcon.sprite = icon;
            }

            UpdateTexts();
        }

        private void ChangeValue(ref int value, Text text, bool increase)
        {
            if (increase)
            {
                value++;
            }
            else
            {
                value = Mathf.Max(value - 1, 0);
            }

            text.text = $"Получить {value} форм";
            UpdateTexts();
        }

        private void UpdateTexts()
        {
            if (_resourceText != null)
            {
                _resourceText.text = $"Получить {_selectedAmount} форм";
            }

            int totalCost = _selectedAmount * _resourcePrice;
            if (_totalCostText != null)
            {
                _totalCostText.text = $"Общая стоимость: {totalCost}";
            }
        }

        private void CheckAntimatter()
        {
            int totalCost = _selectedAmount * _resourcePrice;
            if (GameStats.Instance._antiMaterial >= totalCost)
            {
                _confirmationPanel.SetActive(true);
                _insufficientFundsPanel.SetActive(false);
            }
            else
            {
                _confirmationPanel.SetActive(false);
                _insufficientFundsPanel.SetActive(true);
            }
        }

        private void ConfirmSelection()
        {
            int totalCost = _selectedAmount * _resourcePrice;
            GameStats.Instance.SpendForm(antiMaterial: totalCost);

            switch (_resourceName)
            {
                case "Biofluid":
                    GameStats.Instance.CollectForm(biofluid: _selectedAmount);
                    break;
                case "DarkEnergy":
                    GameStats.Instance.CollectForm(darkEnergy: _selectedAmount);
                    break;
                case "PhotonFlow":
                    GameStats.Instance.CollectForm(photonFlow: _selectedAmount);
                    break;
                case "StarPlasma":
                    GameStats.Instance.CollectForm(starPlasma: _selectedAmount);
                    break;
                default:
                    Debug.LogWarning($"Unknown resource name: {_resourceName}");
                    break;
            }

            if (_confirmationText != null)
            {
                _confirmationText.text = "Добыча успешна!";
            }

            // Сбрасываем текст и состояние
            ResetSelection();

            _confirmationPanel.SetActive(false);
        }

        private void CancelSelection()
        {
            if (_confirmationText != null)
            {
                _confirmationText.text = "Добыча отменена.";
            }

            _confirmationPanel.SetActive(false);
        }

        private void BuyMoreAntimatter()
        {
            _insufficientFundsPanel.SetActive(false);
        }

        private void ResetSelection()
        {
            _selectedAmount = 0;
            UpdateTexts();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.pointerPress != null)
            {
                SetButtonPressed(eventData.pointerPress, true);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.pointerPress != null)
            {
                SetButtonPressed(eventData.pointerPress, false);
            }
        }

        private void SetButtonPressed(GameObject button, bool isPressed)
        {
            if (button == _increaseButton.gameObject)
            {
                _isIncreasePressed = isPressed;
            }
        }  
    }
}
