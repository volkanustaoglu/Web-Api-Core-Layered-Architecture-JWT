using AppointmentApp.API.AppConfig;
using AppointmentApp.Data.Enums;
using AppointmentApp.Data.MainParameters;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Methods
{
    public class UserManager
    {
        private static readonly object Lock = new object();
        private static volatile UserManager _instance;

        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public async Task<ProjectResult<List<User>>> RegisterUser(User entity)
        {
            SqlConnection sqlConnection = null;
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommandCheck = new SqlCommand("[dbo].[GetUserCheck]", sqlConnection))
                    {
                        sqlCommandCheck.CommandType = CommandType.StoredProcedure;
                        sqlCommandCheck.Parameters.Clear();

                        sqlCommandCheck.Parameters.AddWithValue("@Username", entity.Username);
                        sqlCommandCheck.Parameters.AddWithValue("@Email", entity.Email);
                        int results = Convert.ToInt32(sqlCommandCheck.ExecuteScalar());
                        sqlCommandCheck.Dispose();

                        if (results == 0)
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("[dbo].[CreateUser]", sqlConnection))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.Clear();

                                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                                sqlCommand.Parameters.AddWithValue("Surname", entity.Surname);
                                sqlCommand.Parameters.AddWithValue("Email", entity.Email);
                                sqlCommand.Parameters.AddWithValue("Phone", entity.Phone);
                                sqlCommand.Parameters.AddWithValue("Username", entity.Username);
                                sqlCommand.Parameters.AddWithValue("Password", entity.Password);
                                sqlCommand.Parameters.AddWithValue("Img", entity.Img);
                                sqlCommand.Parameters.AddWithValue("EmailConfirmToken", entity.EmailConfirmToken);
                                sqlCommand.Parameters.Add("@ReturnId", SqlDbType.Int).Direction = ParameterDirection.Output;

                                await sqlCommand.ExecuteNonQueryAsync();
                                await sqlCommand.DisposeAsync();

                                result.ReturnId = Convert.ToInt32(sqlCommand.Parameters["@ReturnId"].Value);
                              

                            }
                        }
                        else
                        {
                            result.IsSuccess = false;
                        }
                    }

                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();

                }

            }
            catch (Exception ex)
            {

                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<UserParameter>>> LoginUserCheck(UserParameter entity)
        {
            List<UserParameter> users = new List<UserParameter>();
            ProjectResult<List<UserParameter>> result = new ProjectResult<List<UserParameter>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand("[dbo].[LoginUserCheck]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Username", entity.Username);
                        sqlCommand.Parameters.AddWithValue("@Email", entity.Email);
                        sqlCommand.Parameters.AddWithValue("@Password", entity.Password);

                        int results = Convert.ToInt32(sqlCommand.ExecuteScalar());
                        sqlCommand.Dispose();

                        if (results != 0)
                        {
                            using (SqlDataReader read = await sqlCommand.ExecuteReaderAsync())
                            {
                                if (read.HasRows)
                                {
                                    while (await read.ReadAsync())
                                    {
                                        UserParameter user = new UserParameter();
                                        user.Id = Convert.ToInt32(read["Id"]);
                                        user.Username = read["UserName"].ToString();
                                        user.Email = read["Email"].ToString();

                                        users.Add(user);
                                    }
                                }
                                await read.CloseAsync();
                            }
                            result.Data = users;
                        }
                        else
                        {
                            result.IsSuccess = false;
                        }
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.DisposeAsync();
                    await sqlConnection.CloseAsync();

                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            result = await Task.Run(() => result);
            return result;
        }



        public async Task<ProjectResult<List<User>>> GetAll()
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            List<User> datas = new List<User>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Users_GetUsers]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        result.IsSuccess = Convert.ToBoolean(sqlCommand.ExecuteScalar());

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    User data = new User();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Name = Convert.ToString(reader["Name"]);
                                    data.Surname = Convert.ToString(reader["Surname"]);
                                    data.Email = Convert.ToString(reader["Email"]);
                                    data.Phone = Convert.ToString(reader["Phone"]);
                                    data.UserType = (UserTypes)reader["UserType"];
                                    data.Username = Convert.ToString(reader["Username"]);
                                    data.Img = Convert.ToString(reader["Img"]);
                                    

                                    datas.Add(data);
                                }
                            }
                            await reader.CloseAsync();
                        }
                        result.Data = datas;
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<User>>> GetById(int id)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            List<User> datas = new List<User>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Users_GetUser]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@Id", id);
                        result.IsSuccess = Convert.ToBoolean(sqlCommand.ExecuteScalar());

                        

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    User data = new User();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Name = Convert.ToString(reader["Name"]);
                                    data.Surname = Convert.ToString(reader["Surname"]);
                                    data.Email = Convert.ToString(reader["Email"]);
                                    data.Phone = Convert.ToString(reader["Phone"]);
                                    data.UserType = (UserTypes)reader["UserType"];
                                    data.Username = Convert.ToString(reader["Username"]);
                                    data.Img = Convert.ToString(reader["Img"]);
                                    data.EmailConfirmToken = Convert.ToString(reader["EmailConfirmToken"]);

                                    datas.Add(data);
                                }
                            }
                            await reader.CloseAsync();
                        }
                        result.Data = datas;
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<User>>> UpdateConfirmEmail(string userId,string token)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Users_UpdateEmailConfirm]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Id",Convert.ToInt32(userId));
                        sqlCommand.Parameters.AddWithValue("@EmailConfirmToken", token);
                        await sqlCommand.ExecuteNonQueryAsync();

                        await sqlCommand.DisposeAsync();

                    }
                    await sqlConnection .DisposeAsync();
                    await sqlConnection.CloseAsync();
                }
            }
            catch (Exception ex)
            {

                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            result = await Task.Run(() => result);
            return result;

        }
        public async Task<ProjectResult<List<User>>> UpdateEmailConfirmToken(string userId, string token, string newToken)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Users_UpdateEmailConfirmToken]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Id", Convert.ToInt32(userId));
                        sqlCommand.Parameters.AddWithValue("@EmailConfirmToken", token);
                        sqlCommand.Parameters.AddWithValue("@EmailConfirmTokenNew", newToken);
                        await sqlCommand.ExecuteNonQueryAsync();

                        await sqlCommand.DisposeAsync();

                    }
                    await sqlConnection.DisposeAsync();
                    await sqlConnection.CloseAsync();
                }
            }
            catch (Exception ex)
            {

                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            result = await Task.Run(() => result);
            return result;

        }

        public async Task<ProjectResult<List<User>>> GetByEmail(string email)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            List<User> datas = new List<User>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Users_GetUserByEmail]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        sqlCommand.Parameters.AddWithValue("@Email", email);
                        result.IsSuccess = Convert.ToBoolean(sqlCommand.ExecuteScalar());



                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    User data = new User();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Name = Convert.ToString(reader["Name"]);
                                    data.Surname = Convert.ToString(reader["Surname"]);
                                    data.Email = Convert.ToString(reader["Email"]);
                                    data.Phone = Convert.ToString(reader["Phone"]);
                                    data.UserType = (UserTypes)reader["UserType"];
                                    data.Username = Convert.ToString(reader["Username"]);
                                    data.Img = Convert.ToString(reader["Img"]);
                                    data.EmailConfirmToken = Convert.ToString(reader["EmailConfirmToken"]);

                                    datas.Add(data);
                                }
                            }
                            await reader.CloseAsync();
                        }
                        result.Data = datas;
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<User>>> GetUser(User user)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            List<User> datas = new List<User>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Users_GetUser]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        result.IsSuccess = Convert.ToBoolean(sqlCommand.ExecuteScalar());

                        sqlCommand.Parameters.AddWithValue("@Id", user.Id);

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    User data = new User();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Name = Convert.ToString(reader["Name"]);
                                    data.Surname = Convert.ToString(reader["Surname"]);
                                    data.Email = Convert.ToString(reader["Email"]);
                                    data.Phone = Convert.ToString(reader["Phone"]);
                                    data.UserType = (UserTypes)reader["UserType"];
                                    data.Username = Convert.ToString(reader["Username"]);
                                    data.Img = Convert.ToString(reader["Img"]);

                                    datas.Add(data);
                                }
                            }
                            await reader.CloseAsync();
                        }
                        result.Data = datas;
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<User>>> Create(User user)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Users_AddUser]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        result.IsSuccess = Convert.ToBoolean(sqlCommand.ExecuteScalar());

                        sqlCommand.Parameters.AddWithValue("@Name", user.Name);
                        sqlCommand.Parameters.AddWithValue("@Surname", user.Surname);
                        sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                        sqlCommand.Parameters.AddWithValue("@Phone", user.Phone);
                        sqlCommand.Parameters.AddWithValue("@UserType", (int)user.UserType);
                        sqlCommand.Parameters.AddWithValue("@Username", user.Username);
                        sqlCommand.Parameters.AddWithValue("@Password", user.Password);
                        sqlCommand.Parameters.AddWithValue("@Img", user.Img);

                        await sqlCommand.ExecuteNonQueryAsync();
                        await sqlCommand.DisposeAsync();
                    }
                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<User>>> Delete(int id)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[Users_DeleteUser]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        result.IsSuccess = Convert.ToBoolean(command.ExecuteScalar());

                        command.Parameters.AddWithValue("@Id", id);

                        int results = Convert.ToInt32(command.ExecuteScalar());
                        await command.DisposeAsync();

                        if (results != 0)
                        {
                            result.IsSuccess = false;
                        }
                    }
                    await sqlConnection.CloseAsync();
                    await sqlConnection.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }
            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<User>>> Update(User user)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[Users_UpdateUser]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        result.IsSuccess = Convert.ToBoolean(command.ExecuteScalar());

                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Surname", user.Surname);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Phone", user.Phone);
                        command.Parameters.AddWithValue("@UserType", user.UserType);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@Img", user.Img);

                        int results = Convert.ToInt32(command.ExecuteScalar());
                        await command.DisposeAsync();

                        if (results != 0)
                        {
                            result.IsSuccess = false;
                        }
                    }
                    await sqlConnection.DisposeAsync();
                    await sqlConnection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            result = await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<User>>> UpdatePassword(UpdatePasswordModel entity)
        {
            ProjectResult<List<User>> result = new ProjectResult<List<User>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[Users_UpdatePassword]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();
                        result.IsSuccess = Convert.ToBoolean(command.ExecuteScalar());

                        command.Parameters.AddWithValue("@Id", entity.UserId);
                        command.Parameters.AddWithValue("@Password", entity.Password);
                        command.Parameters.AddWithValue("@Token", entity.Token);

                        int results = Convert.ToInt32(command.ExecuteScalar());
                        await command.DisposeAsync();

                        if (results != 0)
                        {
                            result.IsSuccess = false;
                        }
                    }
                    await sqlConnection.DisposeAsync();
                    await sqlConnection.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                WebLogger.DataError.Error(ex);
                result.IsSuccess = false;
                result.Message = ex.Message;
            }

            result = await Task.Run(() => result);
            return result;
        }
    }
}
