﻿using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Scheduling.Domain;
using Scheduling.GraphQl.Types;
using Scheduling.Models;
using Scheduling.Services;
using Scheduling.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scheduling.GraphQl
{
    public class Querys : ObjectGraphType
    {
        public Querys(IHttpContextAccessor httpContext, UserRepository userRepository)
        {

            Name = "Query";
            Field<UserType>(
                "getUser",
                arguments: null,
                resolve: context =>
                {
                    string email = httpContext.HttpContext.User.Claims.First(claim => claim.Type == "Email").Value.ToString();
                    User user = userRepository.Get(email);
                    user.AddPermission(userRepository.GetPermission(email));
                    user.AddTimerHistory(userRepository.GetTimerHistory(email));
                    return user;
                }
            ).AuthorizeWith("Authenticated");

            FieldAsync<ListGraphType<TimerHistoryType>, IReadOnlyCollection<TimerHistory>>(
                "timeHistory",
                resolve: ctx =>
                {
                    return userRepository.GetTimerHistory();
                });
        }
    }
}
