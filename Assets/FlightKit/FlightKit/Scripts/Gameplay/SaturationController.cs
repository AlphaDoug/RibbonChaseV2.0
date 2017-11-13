using System.Collections;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace FlightKit
{
    /// <summary>
    /// Listens to the take-off events and tweens saturation on the ColorCorrectionCurves 
    /// effect if one is found on the camera.
    /// </summary>
    public class SaturationController : MonoBehaviour
    {
        private ColorCorrectionCurves _colorCorrectionFx;
        private ImageEffect_Mosaic _imageEffect_Mosaic;
        private float _saturationTweenStartTime;

        void Start()
        {
            //_colorCorrectionFx = GameObject.FindObjectOfType<ColorCorrectionCurves>();
            _imageEffect_Mosaic = GameObject.FindObjectOfType<ImageEffect_Mosaic>();
            //if (_colorCorrectionFx)
            //{
            //    TakeOffPublisher.OnTakeOffEvent += OnTakeOff;
            //}
            if (_imageEffect_Mosaic)
            {
                // TakeOffPublisher.OnTakeOffEvent += OnTakeOff;
                OnTakeOff();
            }
        }

        void OnDisable()
        {
            //TakeOffPublisher.OnTakeOffEvent -= OnTakeOff;
        }

        private void OnTakeOff()
        {
            StartCoroutine(OnTakeOffCore());
        }

        private IEnumerator OnTakeOffCore()
        {
            // Make a short pause to build suspense.
            yield return new WaitForSeconds(0.5f);

            // Play camera saturation animation.
            _saturationTweenStartTime = Time.time;
            var wait = new WaitForEndOfFrame();
            while (_imageEffect_Mosaic.MosaicSize > 1)
            {
                float deltaTime = Time.time - _saturationTweenStartTime;
                _imageEffect_Mosaic.MosaicSize -= 1;
                yield return new WaitForSeconds(0.05f);
                yield return wait;
            }

            _imageEffect_Mosaic.MosaicSize = 0;
        }
    }

}
