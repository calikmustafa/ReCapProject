﻿using Core.Entities.Concrete;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IUserService
    {
        List< OperationClaim> GetClaims(User user);
         void Add(User user);

        User GetByMail(string mail);


    }
}
