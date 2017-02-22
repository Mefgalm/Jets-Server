﻿using Jets.Services.Context;
using SendModels;
using SendModels.Requests;
using SendModels.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Jets.Services
{
    public class ClientService
    {


        public Response<SignUpResponse> SignUp(Request<SignUpRequest> request)
        {
            //validate model
            var errorList = new List<ValidationResult>();
            if (!Validator.TryValidateObject(request.DataObject, new ValidationContext(request.DataObject), errorList, true))
            {
                return new Response<SignUpResponse>(Status.ModelIsInvalid)
                {
                    ErrorMessage = errorList.Select(x => x.ErrorMessage).Aggregate((x, y) => $"{x}, {y}"),
                };
            }

            using (var dbContext = new JetsDatabaseContext())
            {
                //check if username already taken
                if (dbContext.Clients.Any(x => x.Username == request.DataObject.Username))
                {
                    return new Response<SignUpResponse>(Status.UsernameAlreadyTaken, "Username already taken");
                }

                //create client
                Database.Client client = CreateClient(Guid.NewGuid(), request.DataObject.Username, request.DataObject.Password,
                    request.DataObject.DateOfBirth, Guid.NewGuid(), DateTime.UtcNow.AddDays(10));

                dbContext.Clients.Add(client);

                dbContext.SaveChanges();

                return new Response<SignUpResponse>()
                {
                    DataObject = new SignUpResponse()
                    {
                        AccessToken = client.AccessToken,
                        Username = client.Username,
                    }
                };
            }
        }

        #region private methods

        private Database.Client CreateClient(Guid id, string username, string password, DateTime? dateOfBirth, Guid accessToken, DateTime tokenExpire)
        {
            return new Database.Client()
            {
                Id = id,
                Password = password,
                Username = username,
                DateOfBirth = dateOfBirth,
                TokenExpire = tokenExpire,
                AccessToken = accessToken,
            };
        }

        #endregion
    }
}
