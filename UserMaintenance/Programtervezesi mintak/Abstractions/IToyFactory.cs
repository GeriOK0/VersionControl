using Programtervezesi_mintak.Abstactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programtervezesi_mintak.Abstractions
{
    public interface IToyFactory
    {
        Toy CreateNew();
    }
}
