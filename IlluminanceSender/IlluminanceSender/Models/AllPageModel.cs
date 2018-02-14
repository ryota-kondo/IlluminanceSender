using System;
using System.Collections.Generic;
using System.Text;
using IlluminanceSender.Interfaces;
using Prism.Mvvm;

namespace IlluminanceSender.Models
{
    class AllPageModel : BindableBase, IAllPageModel
    {
        public int Temp { get; set; }

        public AllPageModel()
        {
            Temp = 12;
        }
    }
}
