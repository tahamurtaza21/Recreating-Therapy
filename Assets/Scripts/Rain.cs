using UnityEngine;
using System.Collections;

namespace DigitalRuby.RainMaker
{
    public class Rain : MonoBehaviour
    {
        public RainScript2D RainScript;

        private void Start()
        {
            RainScript.RainIntensity = 1f;
            RainScript.EnableWind = false;
        }
        
        public void RainSliderChanged(float val)
        {
            RainScript.RainIntensity = val;
        }

        public void ChangeRainIntensity()
        {
            RainScript.RainIntensity = RainScript.RainIntensity - 0.3f;
        }

        public void StopRain()
        {
            RainScript.RainIntensity = 0f;
        }
    }
}