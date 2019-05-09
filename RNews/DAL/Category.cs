using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public enum Category
    {
        [Display(Description = "Java")]
        Java = 1,
        [Display(Description = "C#/.Net")]
        CSharpAndDotNet,
        [Display(Description = "C/C++")]
        CAndCPlusPlus,
        [Display(Description = "Web")]
        Web
    }
}
