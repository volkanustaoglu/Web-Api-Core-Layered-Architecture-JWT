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
    public class BlogCommentsManager
    {
        private static readonly object Lock = new object();
        private static volatile BlogCommentsManager _instance;

        public static BlogCommentsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BlogCommentsManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public async Task<ProjectResult<List<BlogComment>>> GetAll()
        {
            ProjectResult<List<BlogComment>> result = new ProjectResult<List<BlogComment>>();
            List<BlogComment> datas = new List<BlogComment>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[BlogComments_GetBlogComments]", sqlConnection))
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
                                    BlogComment data = new BlogComment();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Comment = Convert.ToString(reader["Comment"]);
                                    data.BlogId = Convert.ToInt32(reader["BlogId"]);
                                    data.UserId= Convert.ToInt32(reader["UserId"]);
                                    data.Description= Convert.ToString(reader["Description"]);

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

        public async Task<ProjectResult<List<BlogComment>>> GetById(int id)
        {
            ProjectResult<List<BlogComment>> result = new ProjectResult<List<BlogComment>>();
            List<BlogComment> datas = new List<BlogComment>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[BlogComments_GetById]", sqlConnection))
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
                                    BlogComment data = new BlogComment();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Comment = Convert.ToString(reader["Comment"]);
                                    data.BlogId = Convert.ToInt32(reader["BlogId"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.Description = Convert.ToString(reader["Description"]);

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

        public async Task<ProjectResult<List<BlogComment>>> GetBlogCommentsByBlogId(int blogId)
        {
            ProjectResult<List<BlogComment>> result = new ProjectResult<List<BlogComment>>();
            List<BlogComment> datas = new List<BlogComment>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[BlogComments_GetBlogCommentByBlog]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@BlogId", blogId);

                        result.IsSuccess = Convert.ToBoolean(sqlCommand.ExecuteScalar());

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    BlogComment data = new BlogComment();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.Comment = Convert.ToString(reader["Comment"]);
                                    data.BlogId = Convert.ToInt32(reader["BlogId"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.Description = Convert.ToString(reader["Description"]);

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

        public async Task<ProjectResult<List<BlogComment>>> Create(BlogComment blogComment)
        {
            ProjectResult<List<BlogComment>> result = new ProjectResult<List<BlogComment>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[BlogComments_AddBlogComment]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Comment", blogComment.Comment);
                        sqlCommand.Parameters.AddWithValue("@Description", blogComment.Description);
                        sqlCommand.Parameters.AddWithValue("@BlogId", blogComment.BlogId);
                        sqlCommand.Parameters.AddWithValue("@UserId", blogComment.UserId);

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

        public async Task<ProjectResult<List<BlogComment>>> Delete(int id)
        {
            ProjectResult<List<BlogComment>> result = new ProjectResult<List<BlogComment>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[BlogComments_DeleteBlogComment]", sqlConnection))
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

        public async Task<ProjectResult<List<BlogComment>>> Update(BlogComment blogComment)
        {
            ProjectResult<List<BlogComment>> result = new ProjectResult<List<BlogComment>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[BlogComments_UpdateBlogComment]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@Id", blogComment.Id);
                        command.Parameters.AddWithValue("@Comment", blogComment.Comment);
                        command.Parameters.AddWithValue("@Description", blogComment.Description);

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
