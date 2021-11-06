using PocketGym.Application.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PocketGym.Application.Extensions
{
    public static class InsertValidationExtension
    {
        private const string DefaultReasonToInsert = "The user's field is invalid: ";
        public static void ValidateToInsert(this UserDto user)
        {            
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                throw new ArgumentNullException(nameof(user.Name), DefaultReasonToInsert);
            }

            if (string.IsNullOrWhiteSpace(user.Username))
            {
                throw new ArgumentNullException(nameof(user.Username), DefaultReasonToInsert);
            }
            
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentNullException(nameof(user.Password), DefaultReasonToInsert);
            }

            if (user.Age < 1)
            {
                throw new ArgumentException(DefaultReasonToInsert, nameof(user.Age));
            }
        }
    }
}
