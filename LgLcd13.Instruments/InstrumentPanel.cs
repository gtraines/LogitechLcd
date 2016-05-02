using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcd13.Instruments
{
    public class InstrumentPanel
    {
        public Bitmap InstrumentImage { get; set; }
        public List<InstrumentLine> InstrumentLines { get; set; } 
    }
}
