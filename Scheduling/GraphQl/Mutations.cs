using GraphQL;
using GraphQL.Types;
using Scheduling.Domain;
using Scheduling.GraphQl.Types;
using Scheduling.Services;
using Scheduling.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.GraphQl
{
    public class Mutations : ObjectGraphType
    {
        public Mutations(IdentityService identityService, UserRepository userRepository)
        {
            Name = "Mutation";

            Field<StringGraphType>(
                "authentication",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "Email", Description = "User email." },
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "Password", Description = "User password."}
                ),
                resolve: context =>
                {
                    string email = context.GetArgument<string>("Email");
                    string password = context.GetArgument<string>("Password");

                    return identityService.Authenticate(email, password);   
                },
                description: "Returns JWT."
            );

            Field<TimerHistoryType>(
                "addTimerStartValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "StartTime", Description = "Timer began" }
                ),
                resolve: context =>
                {
                    DateTime startTime = context.GetArgument<DateTime>("StartTime");

                    return userRepository.AddTimerStartValue(startTime);   
                },
                description: "Add start time"
            );

            Field<TimerHistoryType>(
                "addTimerFinishValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "StartTime", Description = "Timer began" },
                    new QueryArgument<NonNullGraphType<DateTimeGraphType>> { Name = "FinishTime", Description = "Timer finished"}
                ),
                resolve: context =>
                {
                    DateTime startTime = context.GetArgument<DateTime>("StartTime");
                    DateTime finishTime = (DateTime)context.GetArgument<DateTime?>("FinishTime");

                    return userRepository.AddTimerFinishValue(startTime, finishTime);   
                },
                description: "Update value: added finish time"
            );
        }
    }
}
