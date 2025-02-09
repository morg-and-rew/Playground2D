using Playground2D.Canvas.ResourceExtractionWindows;
using UnityEngine;

namespace Playground2D.StationObject.Biofluid
{
    public class DarkEnergyStation : Station
    {
        [SerializeField] private StationResourceExtraction _stationResourceExtraction;
        [SerializeField] private GameObject _resourceExtractionWindow;

        private void OnMouseDown()
        {
            if (_stationResourceExtraction != null && _resourceExtractionWindow != null)
            {
                OpenResourceExtractionWindow(
                    _stationResourceExtraction.items[3].nameStation,
                    _stationResourceExtraction.items[3].nameForm,
                    _stationResourceExtraction.items[3].price,
                    _stationResourceExtraction.items[3].icon
                );
            }
        }

        private void OpenResourceExtractionWindow(string stationName, string resourceName, int resourcePrice, Sprite icon)
        {
            ResourceExtractionWindow window = _resourceExtractionWindow.GetComponent<ResourceExtractionWindow>();

            if (window != null)
            {
                window.Init(stationName, resourceName, resourcePrice, icon);

                _resourceExtractionWindow.SetActive(true);
            }
        }
    }
}
