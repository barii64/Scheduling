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
                "editTimerFinishValue",
                arguments: new QueryArguments(
                    new QueryArgument<DateTimeGraphType> { Name = "StartTime", Description = "Timer started" },
                    new QueryArgument<DateTimeGraphType> { Name = "FinishTime", Description = "Timer finished" },
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Edit Timer finished" }
                ),
                resolve: context =>
                {
                    DateTime startTime = (DateTime)context.GetArgument<DateTime>("StartTime");
                    DateTime finishTime = (DateTime)context.GetArgument<DateTime>("FinishTime");


                    int id = context.GetArgument<int>("id");

                    return userRepository.EditTimerValue(id, startTime, finishTime);
                },
                description: "Update value: added finish time"
            );
            Field<TimerHistoryType>(
                "deleteTimerFinishValue",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Edit Timer finished" }
                ),
                resolve: context =>
                {

                    int id = context.GetArgument<int>("id");

                    return userRepository.DeteleTimerValue(id);
                },
                description: "Update value: added finish time"
            );
        }
    }
}
