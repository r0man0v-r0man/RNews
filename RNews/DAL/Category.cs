using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.DAL
{
    public enum Category
    {
        Java = 1,
        [Display(Name = "C#/.NET")]
        CSharpAndDotNet,
        CAndCPlusPlus,
        Web
    }
}
