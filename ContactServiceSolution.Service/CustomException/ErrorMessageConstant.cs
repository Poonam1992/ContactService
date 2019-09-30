using System;
using System.Collections.Generic;
using System.Text;

namespace ContactServiceSolution.Service.CustomException
{
   public  class ErrorMessageConstant
    {
        public const string _contactAlreadyExistsMsg= " Contact already exist in Database";
        public const string _contactNotFoundMsg = " Contact Not found";
        public const string _contactDataNotFoundMsg = " Contact data  Not found";
    }
}
