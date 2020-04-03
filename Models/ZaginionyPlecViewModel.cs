using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaginionki.Models
{
    public class ZaginionyPlecViewModel
    {
        public List<Zaginiony> Zaginieni { get; set; }
        public SelectList Plec { get; set; }
        public string Pleec { get; set; }
        public string SearchString { get; set; }
    }
}
