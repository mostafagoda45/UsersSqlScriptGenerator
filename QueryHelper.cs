using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestAPi
{
    public class QueryHelper
    {
        public static void CreateQuery(User user)
        {
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = @"INSERT INTO [dbo].[User]
           ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash]
           ,[SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd]
           ,[LockoutEnabled], [AccessFailedCount], [Createdate], [Updatedate], [Creator], [Updator], [IsActive]
           ,[Firstname], [Lastname], [BirthDate], [RegisterDate], [Code], [IsForceResetPassword], [LoggedIn], [LastLogIn], [ExamId])
            VALUES(@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, 1, @PasswordHash
           , @SecurityStamp, @ConcurrencyStamp, @PhoneNumber, 0, @TwoFactorEnabled, @LockoutEnd
           , @LockoutEnabled, @AccessFailedCount, @Createdate, @Updatedate, @Creator, @Updator, @IsActive
           , @Firstname, @Lastname, @BirthDate, @RegisterDate, @Code, @IsForceResetPassword, @LoggedIn, @LastLogIn, @ExamId)";

            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            object obj;
            Type type;

            foreach(PropertyInfo pi in user.GetType().GetProperties()) {
                obj = pi.GetValue(user);
                type = pi.PropertyType;
                if (obj == null)
                    sqlParameters.Add(new SqlParameter($"@{pi.Name}", "Null"));
                else if (type == typeof(int))
                    sqlParameters.Add(new SqlParameter($"@{pi.Name}", obj));
                else if (type == typeof(bool))
                {
                    sqlParameters.Add(new SqlParameter($"@{pi.Name}", Convert.ToInt32((bool)obj)));
                }
                else sqlParameters.Add(new SqlParameter($"@{pi.Name}", $"'{obj}'"));
            }

            foreach (var p in sqlParameters)
            {
                sqlCommand.CommandText = sqlCommand.CommandText.Replace(p.ParameterName, p.Value.ToString());
            }

            StringBuilder stringBuilder = new StringBuilder(sqlCommand.CommandText);
            stringBuilder.Append(Environment.NewLine)
                .Append($"INSERT INTO [dbo].[UserRole] ([UserId], [RoleId]) VALUES ('{user.Id}', 'a2ece6fb-8609-422e-b8e4-ac93c47f7c32')")
                .Append(Environment.NewLine)
                .Append(Environment.NewLine);

            string path = @"C:\Users\mosta\Desktop\users.sql";
            File.AppendAllText(path, stringBuilder.ToString());
        }
    }
}
