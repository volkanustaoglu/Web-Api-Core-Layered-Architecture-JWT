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
    public class DoctorVotingManager
    {
        private static readonly object Lock = new object();
        private static volatile DoctorVotingManager _instance;

        public static DoctorVotingManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DoctorVotingManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public async Task<ProjectResult<List<DoctorVoting>>> GetAll()
        {
            ProjectResult<List<DoctorVoting>> result = new ProjectResult<List<DoctorVoting>>();
            List<DoctorVoting> datas = new List<DoctorVoting>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[DoctorVoting_GetDoctorVotings]", sqlConnection))
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
                                    DoctorVoting data = new DoctorVoting();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                                    data.Vote = Convert.ToDouble(reader["Vote"]);

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

        public async Task<ProjectResult<List<DoctorVoting>>> GetById(int id)
        {
            ProjectResult<List<DoctorVoting>> result = new ProjectResult<List<DoctorVoting>>();
            List<DoctorVoting> datas = new List<DoctorVoting>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[DoctorVoting_GetById]", sqlConnection))
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
                                    DoctorVoting data = new DoctorVoting();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                                    data.Vote = Convert.ToDouble(reader["Vote"]);

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

        public async Task<ProjectResult<List<DoctorVoting>>> GetDoctorVotingByUserId(int id)
        {
            ProjectResult<List<DoctorVoting>> result = new ProjectResult<List<DoctorVoting>>();
            List<DoctorVoting> datas = new List<DoctorVoting>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[DoctorVoting_GetDoctorVotingByUserId]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@UserId", id);

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    DoctorVoting data = new DoctorVoting();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                                    data.Vote = Convert.ToDouble(reader["Vote"]);

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

        public async Task<ProjectResult<List<DoctorVoting>>> Create(DoctorVoting doctorVoting)
        {
            ProjectResult<List<DoctorVoting>> result = new ProjectResult<List<DoctorVoting>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[DoctorVoting_AddDoctorVotings]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@UserId", doctorVoting.UserId);
                        sqlCommand.Parameters.AddWithValue("@DoctorId", doctorVoting.DoctorId);
                        sqlCommand.Parameters.AddWithValue("@Vote", doctorVoting.Vote);

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

        public async Task<ProjectResult<List<DoctorVoting>>> Delete(int id)
        {
            ProjectResult<List<DoctorVoting>> result = new ProjectResult<List<DoctorVoting>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[DoctorVoting_DeleteDoctorVotings]", sqlConnection))
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

        public async Task<ProjectResult<List<DoctorVoting>>> Update(DoctorVoting doctorVoting)
        {
            ProjectResult<List<DoctorVoting>> result = new ProjectResult<List<DoctorVoting>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[DoctorVoting_UpdateDoctorVoting]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@Id", doctorVoting.Id);
                        command.Parameters.AddWithValue("@UserId", doctorVoting.UserId);
                        command.Parameters.AddWithValue("@Vote", doctorVoting.Vote);
                        command.Parameters.AddWithValue("@DoctorId", doctorVoting.DoctorId);

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
