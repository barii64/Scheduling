using Microsoft.EntityFrameworkCore;
using Scheduling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.Domain
{
    interface IUserRepository
    {
        IEnumerable<User> Get();
        User Get(string email);

        List<Permission> GetPermission(string email);
        List<TimerHistory> GetTimerHistory(string email);
    }

    public class UserRepository : IUserRepository
    {
        readonly UserDBContext Context;

        public UserRepository(UserDBContext context)
        {
            Context = context;
        }

        public IEnumerable<User> Get()
        {
            return Context.Users;
        }

        public User Get(string email)
        {
            return Context.Users.FirstOrDefault(user => user.Email == email);
        }

        public List<Permission> GetPermission(string email)
        {
            User user = Context.Users.FirstOrDefault(user => user.Email == email);
            if (user == null)
                return new List<Permission>();

            List<UserPermission> userPermissions = Context.UserPermissions.Where(permission => permission.UserId == user.Id).ToList<UserPermission>();
            List<Permission> permissions = new List<Permission>();

            foreach (UserPermission userPermission in userPermissions)
            {
                permissions.Add(Context.Permissions.Single(permission => permission.Id == userPermission.PermisionId));
            }
            return permissions;
        }
        public List<TimerHistory> GetTimerHistory(string email)
        {
            //return Context.TimerHistory.ToList();

            User user = Context.Users.FirstOrDefault(user => user.Email == email);

            if (user == null)
                return new List<TimerHistory>();

            List<UserTimerHistory> userTimerHistories = Context.UserTimerHistories.Where(timerHistory => timerHistory.UserId == user.Id).ToList<UserTimerHistory>();

            List<TimerHistory> timerHistories = new List<TimerHistory>();

            foreach (UserTimerHistory userTimerHistory in userTimerHistories)
            {
                timerHistories.Add(Context.TimerHistories.SingleOrDefault(timerHistory => timerHistory.Id == userTimerHistory.TimerHistoryId));
            }
            return timerHistories;
        }
        public async Task<TimerHistory> GetTimerHistoryById(int timerHistoryId)
        {
            return await Context.TimerHistories.FindAsync(timerHistoryId);
        }
        public async Task<User> GetUserById(int userId)
        {
            return await Context.Users.FindAsync(userId);
        }
        public async Task<IReadOnlyCollection<TimerHistory>> GetTimerHistory()
        {
            return await Context.TimerHistories.AsNoTracking().ToListAsync();
        }
        public TimerHistory AddTimerStartValue(DateTime? startTimeArg)
        {
            DateTime? startTime = null;

            if (startTimeArg != null)
            {
                startTime = startTimeArg.Value;
            }
            

            var TimerValues = new TimerHistory()
            {
                StartTime = startTime
            };
            Context.Add(TimerValues);
            Context.SaveChanges();

            return TimerValues;
        }
        public TimerHistory EditTimerValue(int id, DateTime? startTime, DateTime? finishtTime)
        {

            var dbRecord = Context.TimerHistories.Single(timerHistory => timerHistory.Id == id);

            if (finishtTime == new DateTime())
            {
                finishtTime = dbRecord.FinishTime;
            }

            if (startTime == new DateTime())
            {
                startTime = dbRecord.StartTime;
            }
            dbRecord.FinishTime = finishtTime;

            dbRecord.StartTime = startTime;

            var TimerValues = new TimerHistory()
            {
                Id = id,
                StartTime = startTime,
                FinishTime = finishtTime
            };
            //Context.TimerHistories.Single(timerHistory => timerHistory.Id == id).FinishTime = finishtTime;

            //Context.Update(TimerValues);
            Context.SaveChanges();

            return TimerValues;
        }
        public TimerHistory DeteleTimerValue(int id)
        {
            var dbRecord = Context.TimerHistories.Single(timerHistory => timerHistory.Id == id);

            Context.Remove(dbRecord);

            var TimerValues = new TimerHistory()
            {
                Id = id,
                StartTime = dbRecord.StartTime,
                FinishTime = dbRecord.FinishTime
            };
            //Context.TimerHistories.Single(timerHistory => timerHistory.Id == id).FinishTime = finishtTime;

            //Context.Update(TimerValues);
            Context.SaveChanges();

            return TimerValues;
        }
    }
}
