using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTestProjectDomainLayer.BaseClasses
{
    public class ValidateRequest 
    {
        public String ValidationMode { get; set; }
        public const string const_Field_ValidationMode = "ValidationMode";
        public ValidateRequest()
        {
            ValidationMode = String.Empty;
        }
    }
}
