using Playground2D.Game.Stats;
using Playground2D.ÑonsumerOblect;
using UnityEngine;
using UnityEngine.UI;
using Playground2D.Tool.Time;

namespace Playground2D.Canvas.ÑonsumerWindow.Inactive
{
    public class ConsumerInactiveWindow : MonoBehaviour
    {
        [SerializeField] private Text _biofluidText;
        [SerializeField] private Text _darkEnergyText;
        [SerializeField] private Text _photonFlowText;
        [SerializeField] private Text _starPlasmaText;

        [SerializeField] private Text _biofluidPointsText;
        [SerializeField] private Text _darkEnergyPointsText;
        [SerializeField] private Text _photonFlowPointsText;
        [SerializeField] private Text _starPlasmaPointsText;

        [SerializeField] private Button _biofluidIncreaseButton;
        [SerializeField] private Button _biofluidDecreaseButton;
        [SerializeField] private Button _darkEnergyIncreaseButton;
        [SerializeField] private Button _darkEnergyDecreaseButton;
        [SerializeField] private Button _photonFlowIncreaseButton;
        [SerializeField] private Button _photonFlowDecreaseButton;
        [SerializeField] private Button _starPlasmaIncreaseButton;
        [SerializeField] private Button _starPlasmaDecreaseButton;
        [SerializeField] private Button _confirmButton;

        [SerializeField] private GameObject _objectToHide1;
        [SerializeField] private GameObject _objectToHide2;
        [SerializeField] private GameObject _objectToHide3;
        [SerializeField] private GameObject _objectToShow1;
        [SerializeField] private GameObject _objectToShow2;
        [SerializeField] private Text _timerText;

        private int _biofluidValue = 0;
        private int _darkEnergyValue = 0;
        private int _photonFlowValue = 0;
        private int _starPlasmaValue = 0;

        private Consumer _consumer;
        private Timer _timer;

        private void Start()
        {
            _consumer = FindObjectOfType<Consumer>();
            _timer = gameObject.AddComponent<Timer>();
            _timer.OnTimerUpdate += UpdateTimerText;
            _timer.OnTimerComplete += OnTimerComplete;
            _timer.duration = 10f; // Óñòàíîâèòå äëèòåëüíîñòü òàéìåðà

            // Set up button click listeners
            if (_biofluidIncreaseButton != null)
            {
                _biofluidIncreaseButton.onClick.AddListener(OnBiofluidIncrease);
            }
            if (_biofluidDecreaseButton != null)
            {
                _biofluidDecreaseButton.onClick.AddListener(OnBiofluidDecrease);
            }
            if (_darkEnergyIncreaseButton != null)
            {
                _darkEnergyIncreaseButton.onClick.AddListener(OnDarkEnergyIncrease);
            }
            if (_darkEnergyDecreaseButton != null)
            {
                _darkEnergyDecreaseButton.onClick.AddListener(OnDarkEnergyDecrease);
            }
            if (_photonFlowIncreaseButton != null)
            {
                _photonFlowIncreaseButton.onClick.AddListener(OnPhotonFlowIncrease);
            }
            if (_photonFlowDecreaseButton != null)
            {
                _photonFlowDecreaseButton.onClick.AddListener(OnPhotonFlowDecrease);
            }
            if (_starPlasmaIncreaseButton != null)
            {
                _starPlasmaIncreaseButton.onClick.AddListener(OnStarPlasmaIncrease);
            }
            if (_starPlasmaDecreaseButton != null)
            {
                _starPlasmaDecreaseButton.onClick.AddListener(OnStarPlasmaDecrease);
            }
            if (_confirmButton != null)
            {
                _confirmButton.onClick.AddListener(OnConfirm);
            }

            // Reset form values when the window is opened
            ResetFormValues();
            UpdateUI();
        }

        private void OnEnable()
        {
            // Reset form values when the window is closed
            ResetFormValues();
        }

        public void UpdateUI()
        {
            // Update the text fields with current values and max values
            _biofluidText.text = $"{_biofluidValue}/{GameStats.Instance._biofluidForm}";
            _darkEnergyText.text = $"{_darkEnergyValue}/{GameStats.Instance._darkEnergyForm}";
            _photonFlowText.text = $"{_photonFlowValue}/{GameStats.Instance._photonFlowForm}";
            _starPlasmaText.text = $"{_starPlasmaValue}/{GameStats.Instance._starPlasmaForm}";

            // Update the points text fields
            _biofluidPointsText.text = $"Points: {_consumer._pointForm.biofluidPoints}";
            _darkEnergyPointsText.text = $"Points: {_consumer._pointForm.darkEnergyPoints}";
            _photonFlowPointsText.text = $"Points: {_consumer._pointForm.photonFlowPoints}";
            _starPlasmaPointsText.text = $"Points: {_consumer._pointForm.starPlasmaPoints}";

            // Calculate total loaded points
            int totalLoaded = (int)(_biofluidValue * _consumer._pointForm.biofluidPoints +
                             _darkEnergyValue * _consumer._pointForm.darkEnergyPoints +
                             _photonFlowValue * _consumer._pointForm.photonFlowPoints +
                             _starPlasmaValue * _consumer._pointForm.starPlasmaPoints);

            // Update the progress bar and text
            _consumer.UpdateFormValues(_biofluidValue, _darkEnergyValue, _photonFlowValue, _starPlasmaValue);

            // Disable increase buttons if total points reach 100
            bool maxPointsReached = totalLoaded >= 100;
            _biofluidIncreaseButton.interactable = !maxPointsReached;
            _darkEnergyIncreaseButton.interactable = !maxPointsReached;
            _photonFlowIncreaseButton.interactable = !maxPointsReached;
            _starPlasmaIncreaseButton.interactable = !maxPointsReached;
        }

        private void ChangeValue(ref int value, Text text, int maxValue, bool increase, int points)
        {
            // Calculate total points before changing the value
            int totalPointsBefore = (int)(_biofluidValue * _consumer._pointForm.biofluidPoints +
                                   _darkEnergyValue * _consumer._pointForm.darkEnergyPoints +
                                   _photonFlowValue * _consumer._pointForm.photonFlowPoints +
                                   _starPlasmaValue * _consumer._pointForm.starPlasmaPoints);

            if (increase)
            {
                // Check if adding points exceeds 100
                if (totalPointsBefore + points <= 100)
                {
                    value = Mathf.Min(value + 1, maxValue);
                }
            }
            else
            {
                value = Mathf.Max(value - 1, 0);
            }

            text.text = $"{value}/{maxValue}";
            UpdateUI();
        }

        // Button click handlers for increasing/decreasing values
        public void OnBiofluidIncrease()
        {
            ChangeValue(ref _biofluidValue, _biofluidText, GameStats.Instance._biofluidForm, true, (int)_consumer._pointForm.biofluidPoints);
            Debug.Log($"Biofluid increased: {_biofluidValue}");
        }

        public void OnBiofluidDecrease()
        {
            ChangeValue(ref _biofluidValue, _biofluidText, GameStats.Instance._biofluidForm, false, (int)_consumer._pointForm.biofluidPoints);
            Debug.Log($"Biofluid decreased: {_biofluidValue}");
        }

        public void OnDarkEnergyIncrease()
        {
            ChangeValue(ref _darkEnergyValue, _darkEnergyText, GameStats.Instance._darkEnergyForm, true, (int)_consumer._pointForm.darkEnergyPoints);
            Debug.Log($"Dark Energy increased: {_darkEnergyValue}");
        }

        public void OnDarkEnergyDecrease()
        {
            ChangeValue(ref _darkEnergyValue, _darkEnergyText, GameStats.Instance._darkEnergyForm, false, (int)_consumer._pointForm.darkEnergyPoints);
            Debug.Log($"Dark Energy decreased: {_darkEnergyValue}");
        }

        public void OnPhotonFlowIncrease()
        {
            ChangeValue(ref _photonFlowValue, _photonFlowText, GameStats.Instance._photonFlowForm, true, (int)_consumer._pointForm.photonFlowPoints);
            Debug.Log($"Photon Flow increased: {_photonFlowValue}");
        }

        public void OnPhotonFlowDecrease()
        {
            ChangeValue(ref _photonFlowValue, _photonFlowText, GameStats.Instance._photonFlowForm, false, (int)_consumer._pointForm.photonFlowPoints);
            Debug.Log($"Photon Flow decreased: {_photonFlowValue}");
        }

        public void OnStarPlasmaIncrease()
        {
            ChangeValue(ref _starPlasmaValue, _starPlasmaText, GameStats.Instance._starPlasmaForm, true, (int)_consumer._pointForm.starPlasmaPoints);
            Debug.Log($"Star Plasma increased: {_starPlasmaValue}");
        }

        public void OnStarPlasmaDecrease()
        {
            ChangeValue(ref _starPlasmaValue, _starPlasmaText, GameStats.Instance._starPlasmaForm, false, (int)_consumer._pointForm.starPlasmaPoints);
            Debug.Log($"Star Plasma decreased: {_starPlasmaValue}");
        }

        public void OnConfirm()
        {
            // Calculate total points
            int totalPoints = (int)(_biofluidValue * _consumer._pointForm.biofluidPoints +
                             _darkEnergyValue * _consumer._pointForm.darkEnergyPoints +
                             _photonFlowValue * _consumer._pointForm.photonFlowPoints +
                             _starPlasmaValue * _consumer._pointForm.starPlasmaPoints);

            // If total points exceed 100, set them to 100
            if (totalPoints > 100)
            {
                totalPoints = 100;
            }

            _consumer.ConfirmFormValues(_biofluidValue, _darkEnergyValue, _photonFlowValue, _starPlasmaValue);

            // Subtract the selected forms from the available forms
            GameStats.Instance.SpendForm(_biofluidValue, _darkEnergyValue, _photonFlowValue, _starPlasmaValue);

            // Confirm the form values

            // Hide the specified objects
            _objectToHide1.SetActive(false);
            _objectToHide2.SetActive(false);
            _objectToHide3.SetActive(false);

            // Show the specified objects
            _objectToShow1.SetActive(true);
            _objectToShow2.SetActive(true);

            // Start the timer
            _timer.StartTimer();
        }

        private void ResetFormValues()
        {
            _biofluidValue = 0;
            _darkEnergyValue = 0;
            _photonFlowValue = 0;
            _starPlasmaValue = 0;

            // Reset the UI elements
            if (_biofluidText != null) _biofluidText.text = $"{_biofluidValue}/{GameStats.Instance._biofluidForm}";
            if (_darkEnergyText != null) _darkEnergyText.text = $"{_darkEnergyValue}/{GameStats.Instance._darkEnergyForm}";
            if (_photonFlowText != null) _photonFlowText.text = $"{_photonFlowValue}/{GameStats.Instance._photonFlowForm}";
            if (_starPlasmaText != null) _starPlasmaText.text = $"{_starPlasmaValue}/{GameStats.Instance._starPlasmaForm}";

            // Reset the points text fields
            if (_biofluidPointsText != null) _biofluidPointsText.text = $"Points: {_consumer._pointForm.biofluidPoints}";
            if (_darkEnergyPointsText != null) _darkEnergyPointsText.text = $"Points: {_consumer._pointForm.darkEnergyPoints}";
            if (_photonFlowPointsText != null) _photonFlowPointsText.text = $"Points: {_consumer._pointForm.photonFlowPoints}";
            if (_starPlasmaPointsText != null) _starPlasmaPointsText.text = $"Points: {_consumer._pointForm.starPlasmaPoints}";
        }

        private void UpdateTimerText(float currentTime)
        {
            float remainingTime = _timer.GetRemainingTime();
            _timerText.text = $"Îñòàëîñü âðåìåíè: {Mathf.CeilToInt(remainingTime)} ñåê.";
        }

        private void OnTimerComplete()
        {
            // Check and award antimatter
            _consumer.CheckAndAwardAntimatter();

            // Hide the specified objects
            _objectToShow1.SetActive(false);
            _objectToShow2.SetActive(false);

            // Show the specified objects
            _objectToHide1.SetActive(true);
            _objectToHide2.SetActive(true);
            _objectToHide3.SetActive(true);
        }
    }
}
