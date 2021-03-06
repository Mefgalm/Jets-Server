﻿using System;

namespace SendModels.Responses
{
    [Serializable]
    public class Response<T>
    {
        public Response()
        {
            Status = Status.Ok;
        }

        public Response(Status status)
        {
            Status = status;
        }

        public Response(Status status, string errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public Status Status { get; set; }

        public string ErrorMessage { get; set; }

        public T DataObject { get; set; }

        public override string ToString()
        {
            return $"Staus : {Status}, ErrorMessage: {ErrorMessage}, DataObject: {DataObject?.ToString()}";
        }
    }
}
