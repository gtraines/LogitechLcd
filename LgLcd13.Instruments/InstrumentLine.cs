using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LgLcd13.Instruments
{
    public class InstrumentLine
    {
        public int LineNumber { get; set; }
        public string LineScheme { get; set; }
        public List<int> Data { get; set; }

        public override string ToString()
        {
            var dataStrings = Data.Select(d => d.ToString("000")).ToArray();

            return string.Format(LineScheme, dataStrings);
        }
    }
}
