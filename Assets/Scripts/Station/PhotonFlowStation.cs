using UnityEngine;
using Playground2D.Canvas.ResourceExtractionWindows;

namespace Playground2D.StationObject.PhotonFlow
{
    public class PhotonFlowStation : Station
    {
        [SerializeField] private StationResourceExtraction _stationResourceExtraction;
        [SerializeField] private GameObject _resourceExtractionWindow; 

        private void OnMouseDown()
        {
            if (_stationResourceExtraction != null && _resourceExtractionWindow != null)
            {
                OpenResourceExtractionWindow(
                    _stationResourceExtraction.items[0].nameStation,
                    _stationResourceExtraction.items[0].nameForm,
                    _stationResourceExtraction.items[0].price,
                    _stationResourceExtraction.items[0].icon
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
