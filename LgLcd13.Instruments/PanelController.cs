using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LgLcdG13.Adapter;

namespace LgLcd13.Instruments
{
    public class PanelController
    {
        private Properties.Settings Settings { get; set; }

        public G13Device G13Device { get; set; }
        public PanelController()
        {
            G13Device = new G13Device();
            Settings = Properties.Settings.Default;
        }
        public bool RunTest()
        {
            InitializePanels();
            SetLcdButtons();
            G13Device.SaveCurrentLighting();
            for (var i = 0; i <= Settings.TestCount; i++)
            {

                UpdateInstrumentPanelData(CurrentPanel, i);
                Thread.Sleep(Settings.RefreshInterval);
                G13Device.CheckButtons();
                UpdatePanelDisplay(CurrentPanel);
                
            }

            return true;
        }

        public void InitializePanels()
        {
            Panels.Add(InstrumentBuilder.CreateInstrumentPanel(Settings.Panel1));
            Panels.Add(InstrumentBuilder.CreateInstrumentPanel(Settings.Panel2));
            Panels.Add(InstrumentBuilder.CreateInstrumentPanel(Settings.Panel3));
            Panels.Add(InstrumentBuilder.CreateInstrumentPanel(Settings.Panel4));

            CurrentPanel = Panels[0];
        }

        public InstrumentPanel CurrentPanel { get; set; }

        private List<InstrumentPanel> _panels;
        public List<InstrumentPanel> Panels
        {
            get
            {
                if (_panels == null)
                {
                    _panels = new List<InstrumentPanel>();
                }
                return _panels;
            }
        }

        public bool UpdateInstrumentPanelData(InstrumentPanel panel, int i)
        {
            try
            {
                foreach (var line in panel.InstrumentLines)
                {
                    for (int counter = 0; counter < line.Data.Count; counter++)
                    {
                        line.Data[counter] = i;
                    }
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public bool UpdatePanelDisplay(InstrumentPanel panel)
        {
            try
            {
                var lineStrings = new List<string>();
                panel.InstrumentLines.ForEach(l =>
                {
                    try
                    {
                        lineStrings.Add(l.ToString());
                    }
                    catch (Exception)
                    {
                        lineStrings.Add(string.Empty);
                    }
                    
                });

                G13Device.Display.UpdateDisplay(lineStrings);
                return true;
            }
            catch (Exception)
            {
                    
                return false;
            }
        }

        public void RedLedFlashEffect()
        {
            var color = new Color(100, 0, 0);
            G13Device.LedEffect(color, EffectType.FLASH, Settings.RedFlashLength, Settings.RedFlashInterval);
        }

        public void GreenLedFlashEffect()
        {
            var color = new Color(0, 100, 0);
            G13Device.LedEffect(color, EffectType.FLASH, Settings.GreenFlashLength, Settings.GreenFlashInterval);
        }

        public void BlueLedFlashEffect()
        {
            var color = new Color(0, 25, 65);
            G13Device.LedEffect(color, EffectType.FLASH, Settings.GreenFlashLength, Settings.GreenFlashInterval);
        }

        public void BlueLedPulseEffect()
        {
            var color = new Color(0, 25, 65);
            G13Device.LedEffect(color, EffectType.PULSE, Settings.BluePulseLength, Settings.BluePulseInterval);
        }

        public void SetLcdButtons()
        {
            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_0].Add(() =>
            {
                RedLedFlashEffect();
                return 1;
            });
            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_0].Add(() =>
            {
                CurrentPanel = Panels[0];
                return 1;
            });

            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_1].Add(() =>
            {
                GreenLedFlashEffect();
                return 1;
            });
            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_1].Add(() =>
            {
                CurrentPanel = Panels[1];
                return 1;
            });

            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_2].Add(() =>
            {
                BlueLedFlashEffect();
                return 1;
            });
            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_2].Add(() =>
            {
                CurrentPanel = Panels[2];
                return 1;
            });

            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_3].Add(() => 
            {
                BlueLedPulseEffect();
                return 1;
            });
            G13Device.ButtonHandlers[Constants.LOGI_LCD_MONO_BUTTON_3].Add(() =>
            {
                CurrentPanel = Panels[3];
                return 1;
            });
        }
    }
}
