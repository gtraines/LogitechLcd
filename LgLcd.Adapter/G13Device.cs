using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcdG13.Adapter
{
    public class G13Device
    {
        public Color DefaultColor { get; set; }
        public Connection Connection { get; set; }
        public Display Display = new Display();

        public G13Device(string appFriendlyName = "")
        {
            Connection = new Connection(appFriendlyName);
            DefaultColor = new Color(Constants.DEFAULT_RED_PCT, Constants.DEFAULT_GREEN_PCT, Constants.DEFAULT_BLUE_PCT);
            SetLightingToDefault();
        }

        public bool UpdateDisplay(List<string> displayLines)
        {
            return Display.UpdateDisplay(displayLines);
        }

        public void LedEffect(Color color, EffectType effectType, int effectLength, int effectInterval)
        {
            Display.LedEffect(color, effectType, effectLength, effectInterval);
        }

        public void SetLightingToDefault()
        {
            LedProxy.LogiLedSetLighting(DefaultColor.Red, DefaultColor.Green, DefaultColor.Blue);
        }

        private Dictionary<int, List<Func<int>>> _buttonHandlers;
        public Dictionary<int, List<Func<int>>> ButtonHandlers
        {
            get
            {
                if (_buttonHandlers == null)
                {
                    _buttonHandlers = new Dictionary<int, List<Func<int>>>();
                    foreach (var button in MonoButtons)
                    {
                        _buttonHandlers.Add(button, new List<Func<int>>());
                    }
                }
                return _buttonHandlers;
            }
        }
        private List<int> _monoButtons;
        public List<int> MonoButtons
        {
            get
            {
                if (_monoButtons == null)
                {
                    _monoButtons = GetMonoButtons();
                }
                return _monoButtons;
            }
        }

        private List<int> GetMonoButtons()
        {
            var buttons = new List<int>();
            buttons.Add(Constants.LOGI_LCD_MONO_BUTTON_0);
            buttons.Add(Constants.LOGI_LCD_MONO_BUTTON_1);
            buttons.Add(Constants.LOGI_LCD_MONO_BUTTON_2);
            buttons.Add(Constants.LOGI_LCD_MONO_BUTTON_3);

            return buttons;
        }

            
        public int CheckButtons()
        {
            foreach(var button in ButtonHandlers)
            {
                if(LcdProxy.LogiLcdIsButtonPressed(button.Key))
                {
                    foreach(var func in button.Value)
                    {
                        func();
                    }
                    return button.Key;
                }
            }
            return -1;
        }

        public bool SaveCurrentLighting()
        {
            return LedProxy.LogiLedSaveCurrentLighting();
        }
    }
}
