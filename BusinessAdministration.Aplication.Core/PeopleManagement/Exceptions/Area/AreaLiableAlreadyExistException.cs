﻿using BusinessAdministration.Aplication.Core.Base.Exceptions;

namespace BusinessAdministration.Aplication.Core.PeopleManagement.Exceptions.Area
{
    public class AreaLiableAlreadyExistException : SypException
    {
        public AreaLiableAlreadyExistException() { }
        public AreaLiableAlreadyExistException(string message): base(message) { }
    }
}
