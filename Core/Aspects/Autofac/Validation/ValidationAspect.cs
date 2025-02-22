﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    //burası controller de yukarısına yazdığımız attribute olarak kullanacağımız clasdır.
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        //burda da validatorumuzun ismini ver demek
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //burda carvalidatordeki AbstractValidator=Basetypedır <Car>=getgenericargumentsdir.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //ilgili metodun parametlerini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
