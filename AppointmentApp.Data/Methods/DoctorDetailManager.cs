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
    public class DoctorDetailManager
    {
        private static readonly object Lock = new object();
        private static volatile DoctorDetailManager _instance;

        public static DoctorDetailManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DoctorDetailManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public async Task<ProjectResult<List<DoctorDetail>>> GetAll()
        {
            ProjectResult<List<DoctorDetail>> result = new ProjectResult<List<DoctorDetail>>();
            List<DoctorDetail> datas = new List<DoctorDetail>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[DoctorDetail_GetDoctorDetails]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    DoctorDetail data = new DoctorDetail();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.Vote = Convert.ToDouble(reader["Vote"]);
                                    data.Subtitle = Convert.ToString(reader["Subtitle"]);
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

        public async Task<ProjectResult<List<DoctorDetail>>> GetById(int id)
        {
            ProjectResult<List<DoctorDetail>> result = new ProjectResult<List<DoctorDetail>>();
            List<DoctorDetail> datas = new List<DoctorDetail>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[DoctorDetail_GetById]", sqlConnection))
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
                                    DoctorDetail data = new DoctorDetail();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.Vote = Convert.ToDouble(reader["Vote"]);
                                    data.Subtitle = Convert.ToString(reader["Subtitle"]);
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

        public async Task<ProjectResult<List<DoctorDetail>>> Create(DoctorDetail doctorDetail)
        {
            ProjectResult<List<DoctorDetail>> result = new ProjectResult<List<DoctorDetail>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[DoctorDetail_AddDoctorDetail]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@UserId", doctorDetail.UserId);
                        sqlCommand.Parameters.AddWithValue("@Vote", doctorDetail.Vote);
                        sqlCommand.Parameters.AddWithValue("@Subtitle", doctorDetail.Subtitle);
                        sqlCommand.Parameters.AddWithValue("@Description", doctorDetail.Description);

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

        public async Task<ProjectResult<List<DoctorDetail>>> Delete(int id)
        {
            ProjectResult<List<DoctorDetail>> result = new ProjectResult<List<DoctorDetail>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[DoctorDetail_DeletyeDoctorDetail]", sqlConnection))
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

        public async Task<ProjectResult<List<DoctorDetail>>> Update(DoctorDetail doctorDetail)
        {
            ProjectResult<List<DoctorDetail>> result = new ProjectResult<List<DoctorDetail>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[DoctorDetail_UpdateDoctorDetail]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@Id", doctorDetail.Id);
                        command.Parameters.AddWithValue("@UserId", doctorDetail.UserId);
                        command.Parameters.AddWithValue("@Vote", doctorDetail.Vote);
                        command.Parameters.AddWithValue("@Subtitle", doctorDetail.Subtitle);
                        command.Parameters.AddWithValue("@Description", doctorDetail.Description);

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
