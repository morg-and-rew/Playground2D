using Playground2D.Canvas.СonsumerWindow.Inactive;
using Playground2D.Game.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Playground2D.СonsumerOblect
{
    public class Consumer : MonoBehaviour
    {
        [SerializeField] private GameObject _consumerInactiveWindow;
        [SerializeField] private Image _progressImage;
        [SerializeField] public PointForm _pointForm;
        [SerializeField] private Text _currentProgressText;
        [SerializeField] private Text _expectedProgressText;

        public int _currentProgress = 0;
        private int _expectedProgress = 0;
        private const int _maxProgress = 100;

        private Texture2D _progressTexture;
        private Color[] _pixels;
        private int _textureWidth = 256;
        private int _textureHeight = 32;

        private RectTransform _progressRectTransform;
        private RectTransform _currentProgressRectTransform;
        private RectTransform _expectedProgressRectTransform;

        private bool _isSynthesisInProgress = false;

        private void Start()
        {
            _progressTexture = new Texture2D(_textureWidth, _textureHeight);
            _pixels = new Color[_textureWidth * _textureHeight];

            _progressRectTransform = _progressImage.GetComponent<RectTransform>();
            _currentProgressRectTransform = _currentProgressText.GetComponent<RectTransform>();
            _expectedProgressRectTransform = _expectedProgressText.GetComponent<RectTransform>();

            UpdateProgressImageWithExpectedProgress(_currentProgress, _expectedProgress);
        }

        private void OnMouseDown()
        {
            _consumerInactiveWindow.SetActive(true);

            ConsumerInactiveWindow window = _consumerInactiveWindow.GetComponent<ConsumerInactiveWindow>();
            if (window != null)
            {
                window.UpdateUI();
            }
        }

        public void UpdateFormValues(int biofluid, int darkEnergy, int photonFlow, int starPlasma)
        {
            if (_isSynthesisInProgress)
            {
                return;
            }

            int totalPoints = (int)(biofluid * _pointForm.biofluidPoints +
                             darkEnergy * _pointForm.darkEnergyPoints +
                             photonFlow * _pointForm.photonFlowPoints +
                             starPlasma * _pointForm.starPlasmaPoints);


            _expectedProgress = totalPoints;

            // Update the progress text
            _currentProgressText.text = $"{_currentProgress}%";
            _expectedProgressText.text = $"{Mathf.Min(_currentProgress + _expectedProgress, _maxProgress)}%";

            // Update the progress image
            UpdateProgressImage(biofluid, darkEnergy, photonFlow, starPlasma);

            // Update text positions
            UpdateTextPositions();
        }

        public void ConfirmFormValues(int biofluid, int darkEnergy, int photonFlow, int starPlasma)
        {
            if (_isSynthesisInProgress)
            {
                return;
            }

            // Calculate total points
            int totalPoints = (int)(biofluid * _pointForm.biofluidPoints +
                             darkEnergy * _pointForm.darkEnergyPoints +
                             photonFlow * _pointForm.photonFlowPoints +
                             starPlasma * _pointForm.starPlasmaPoints);

            // Check if total points exceed 100
            if (totalPoints > 100 && _currentProgress + totalPoints > 100)
            {
                Debug.LogWarning("Total points exceed 100 and it's not the last form. Cannot confirm.");
                return;
            }

            _currentProgressText.text = $"{_currentProgress}%";
            _expectedProgressText.text = $"{_currentProgress + _expectedProgress}%";

            UpdateProgressImageWithExpectedProgress(_currentProgress, _expectedProgress);

            UpdateTextPositions();

            // Start the synthesis process
            _isSynthesisInProgress = true;
        }

        public void CheckAndAwardAntimatter()
        {
            // Check if current progress is 100
            if (_expectedProgress >= 100)
            {
                // Award antimatter
                AwardAntimatter();

                // Reset current progress and expected progress
                _currentProgress = 0;
                _expectedProgress = 0;
            }
            else
            {
                // Save the synthesis progress
                _currentProgress += _expectedProgress;
                _expectedProgress = 0;
            }

            // Update the progress text
            _currentProgressText.text = $"{_currentProgress}%";
            _expectedProgressText.text = $"{_currentProgress + _expectedProgress}%";

            // Update the progress image
            UpdateProgressImageWithExpectedProgress(_currentProgress, _expectedProgress);

            // Update text positions
            UpdateTextPositions();

            // End the synthesis process
            _isSynthesisInProgress = false;
        }

        private void AwardAntimatter()
        {
            // Assuming you have a method to add antimatter
            GameStats.Instance.CollectForm(antiMaterial: 100);
        }

        private void UpdateProgressImage(int biofluid, int darkEnergy, int photonFlow, int starPlasma)
        {
            // Clear the texture
            for (int i = 0; i < _pixels.Length; i++)
            {
                _pixels[i] = Color.clear;
            }

            // Calculate fill amounts for each form
            int currentFillWidth = (_currentProgress * _textureWidth) / _maxProgress;

            // Fill the current progress with white
            FillTextureSection(0, currentFillWidth, Color.white);

            // Calculate the starting position for the expected progress
            int expectedStart = currentFillWidth;

            // Fill the expected progress with colors for each form
            expectedStart = FillTextureSection(expectedStart, (int)(expectedStart + (biofluid * _pointForm.biofluidPoints * _textureWidth) / _maxProgress), Color.red);
            expectedStart = FillTextureSection(expectedStart, (int)(expectedStart + (darkEnergy * _pointForm.darkEnergyPoints * _textureWidth) / _maxProgress), Color.blue);
            expectedStart = FillTextureSection(expectedStart, (int)(expectedStart + (photonFlow * _pointForm.photonFlowPoints * _textureWidth) / _maxProgress), Color.green);
            FillTextureSection(expectedStart, (int)(expectedStart + (starPlasma * _pointForm.starPlasmaPoints * _textureWidth) / _maxProgress), Color.yellow);

            // Apply the pixels to the texture
            _progressTexture.SetPixels(_pixels);
            _progressTexture.Apply();

            // Update the image
            _progressImage.sprite = Sprite.Create(_progressTexture, new Rect(0, 0, _textureWidth, _textureHeight), new Vector2(0.5f, 0.5f));
        }

        private void UpdateProgressImageWithExpectedProgress(int currentProgress, int expectedProgress)
        {
            // Clear the texture
            for (int i = 0; i < _pixels.Length; i++)
            {
                _pixels[i] = Color.clear;
            }

            // Calculate fill width for the current progress
            int currentFillWidth = (currentProgress * _textureWidth) / _maxProgress;

            // Fill the current progress with white
            FillTextureSection(0, currentFillWidth, Color.white);

            // Calculate fill width for the expected progress
            int expectedFillWidth = ((currentProgress + expectedProgress) * _textureWidth) / _maxProgress;

            // Fill the expected progress with a different color
            FillTextureSection(currentFillWidth, expectedFillWidth, Color.gray);

            // Apply the pixels to the texture
            _progressTexture.SetPixels(_pixels);
            _progressTexture.Apply();

            // Update the image
            _progressImage.sprite = Sprite.Create(_progressTexture, new Rect(0, 0, _textureWidth, _textureHeight), new Vector2(0.5f, 0.5f));
        }

        private int FillTextureSection(int startPixel, int endPixel, Color color)
        {
            for (int x = startPixel; x < endPixel; x++)
            {
                for (int y = 0; y < _textureHeight; y++)
                {
                    _pixels[y * _textureWidth + x] = color;
                }
            }
            return endPixel;
        }

        private void UpdateTextPositions()
        {
            // Calculate positions based on progress
            float currentProgressX = (_currentProgress / (float)_maxProgress) * _progressRectTransform.rect.width;
            float expectedProgressX = ((_currentProgress + _expectedProgress) / (float)_maxProgress) * _progressRectTransform.rect.width;

            // Set the position of the current progress text
            _currentProgressRectTransform.anchoredPosition = new Vector2(currentProgressX - 250, _currentProgressRectTransform.anchoredPosition.y);

            // Set the position of the expected progress text
            _expectedProgressRectTransform.anchoredPosition = new Vector2(expectedProgressX - 200, _expectedProgressRectTransform.anchoredPosition.y);

            // Add some space between the texts
            _expectedProgressRectTransform.anchoredPosition = new Vector2(_expectedProgressRectTransform.anchoredPosition.x + 20, _expectedProgressRectTransform.anchoredPosition.y);
        }
    }
}
