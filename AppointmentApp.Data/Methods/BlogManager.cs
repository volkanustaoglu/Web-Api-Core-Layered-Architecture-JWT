using AppointmentApp.API.AppConfig;
using AppointmentApp.Data.Models;
using AppointmentApp.Data.Tool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Methods
{
    public class BlogManager
    {
        private static readonly object Lock = new object();
        private static volatile BlogManager _instance;

        public static BlogManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BlogManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public async Task<ProjectResult<List<Blog>>> GetAll()
        {
            ProjectResult<List<Blog>> result = new ProjectResult<List<Blog>>();
            List<Blog> datas = new List<Blog>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Blog_GetBlogs]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    Blog data = new Blog();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Title = Convert.ToString(reader["Title"]);
                                    data.Subtitle = Convert.ToString(reader["Subtitle"]);
                                    data.Description = Convert.ToString(reader["Description"]);
                                    data.Date = Convert.ToDateTime(reader["Date"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
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

        public async Task<ProjectResult<List<Blog>>> GetById(int id)
        {
            ProjectResult<List<Blog>> result = new ProjectResult<List<Blog>>();
            List<Blog> datas = new List<Blog>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Blog_GetById]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    Blog data = new Blog();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Title = Convert.ToString(reader["Title"]);
                                    data.Subtitle = Convert.ToString(reader["Subtitle"]);
                                    data.Description = Convert.ToString(reader["Description"]);
                                    data.Date = Convert.ToDateTime(reader["Date"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
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

        public async Task<ProjectResult<List<Blog>>> GetBlogByUserId(int userId)
        {
            ProjectResult<List<Blog>> result = new ProjectResult<List<Blog>>();
            List<Blog> datas = new List<Blog>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Blog_GetBlogByUserId]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        
                        sqlCommand.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    Blog data = new Blog();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Title = Convert.ToString(reader["Title"]);
                                    data.Subtitle = Convert.ToString(reader["Subtitle"]);
                                    data.Description = Convert.ToString(reader["Description"]);
                                    data.Date = Convert.ToDateTime(reader["Date"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
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

        public async Task<ProjectResult<List<Blog>>> Create(Blog blog)
        {
            ProjectResult<List<Blog>> result = new ProjectResult<List<Blog>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Blog_AddBlogs]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Title", blog.Title);
                        sqlCommand.Parameters.AddWithValue("@Subtitle", blog.Subtitle);
                        sqlCommand.Parameters.AddWithValue("@Description", blog.Description);
                        sqlCommand.Parameters.AddWithValue("@Date", blog.Date);
                        sqlCommand.Parameters.AddWithValue("@UserId", blog.UserId);
                        sqlCommand.Parameters.AddWithValue("@Img", blog.Img);

                        int effectedRow = await sqlCommand.ExecuteNonQueryAsync();
                        result.IsSuccess = effectedRow > 0;
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

        public async Task<ProjectResult<List<Blog>>> Delete(int id)
        {
            ProjectResult<List<Blog>> result = new ProjectResult<List<Blog>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[Blog_DeleteBlogs]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@Id", id);

                        int effectedRow = await command.ExecuteNonQueryAsync();
                        result.IsSuccess = effectedRow > 0;
                        await command.DisposeAsync();
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

        public async Task<ProjectResult<List<Blog>>> Update(Blog blog)
        {
            ProjectResult<List<Blog>> result = new ProjectResult<List<Blog>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[Blog_UpdateBlogs]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@Id", blog.Id);
                        command.Parameters.AddWithValue("@Title", blog.Title);
                        command.Parameters.AddWithValue("@Subtitle", blog.Subtitle);
                        command.Parameters.AddWithValue("@Description", blog.Description);
                        command.Parameters.AddWithValue("@Date", blog.Date);
                        command.Parameters.AddWithValue("@Img", blog.Img);

                        int effectedRow = await command.ExecuteNonQueryAsync();
                        result.IsSuccess = effectedRow > 0;
                        await command.DisposeAsync();
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
