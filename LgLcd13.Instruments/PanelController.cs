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
        public G13Device G13Device { get; set; }
        public PanelController()
        {
            G13Device = new G13Device();
        }
        public bool RunTest()
        {
            var scheme1 = Properties.Settings.Default.Panel1;
            var panel = InstrumentBuilder.CreateInstrumentPanel(scheme1);

            for (var i = 0; i <= Properties.Settings.Default.TestCount; i++)
            {
                foreach (var line in panel.InstrumentLines)
                {
                    for (int counter = 0; counter < line.Data.Count; counter++)
                    {
                        line.Data[counter] = i;
                    }
                }
                
                Thread.Sleep(Properties.Settings.Default.RefreshInterval);
                UpdatePanelDisplay(panel);
            }

            return true;
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
    }
}
