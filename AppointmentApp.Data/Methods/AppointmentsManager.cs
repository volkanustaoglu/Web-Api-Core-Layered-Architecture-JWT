using AppointmentApp.Data.Models;
using System;
using System.Collections.Generic;
using AppointmentApp.API.AppConfig;
using System.Data.SqlClient;
using System.Data;
using AppointmentApp.Data.Tool;
using System.Threading.Tasks;

namespace AppointmentApp.Data.Methods
{
    public class AppointmentsManager
    {
        private static readonly object Lock = new object();
        private static volatile AppointmentsManager _instance;

        public static AppointmentsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppointmentsManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public async Task<ProjectResult<List<Appointment>>> GetAll()
        {
            ProjectResult<List<Appointment>> result = new ProjectResult<List<Appointment>>();
            List<Appointment> datas = new List<Appointment>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Appointment_GetAppointments]", sqlConnection))
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
                                    Appointment data = new Appointment();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.InsertDate = Convert.ToDateTime(reader["InsertDate"]);
                                    data.StartDate = Convert.ToDateTime(reader["StartDate"]);
                                    data.EndDate = Convert.ToDateTime(reader["EndDate"]);

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

        public async Task<ProjectResult<List<Appointment>>> GetById(int Id)
        {
            ProjectResult<List<Appointment>> result = new ProjectResult<List<Appointment>>();
            List<Appointment> datas = new List<Appointment>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[Appointment_GetById]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Id", Id);

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    Appointment data = new Appointment();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.InsertDate = Convert.ToDateTime(reader["InsertDate"]);
                                    data.StartDate = Convert.ToDateTime(reader["StartDate"]);
                                    data.EndDate = Convert.ToDateTime(reader["EndDate"]);

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

        public async Task<ProjectResult<List<Appointment>>> GetAppointmentByUserId(int userId)
        {
            ProjectResult<List<Appointment>> result = new ProjectResult<List<Appointment>>();
            List<Appointment> datas = new List<Appointment>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[Appointment_GetAppointmentByUserId]", sqlConnection))
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
                                    Appointment data = new Appointment();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.InsertDate = Convert.ToDateTime(reader["InsertDate"]);
                                    data.StartDate = Convert.ToDateTime(reader["StartDate"]);
                                    data.EndDate = Convert.ToDateTime(reader["EndDate"]);

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

        public async Task<ProjectResult<List<Appointment>>> GetAppointmentByDoctorId(int doctorId)
        {
            ProjectResult<List<Appointment>> result = new ProjectResult<List<Appointment>>();
            List<Appointment> datas = new List<Appointment>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[Appointment_GetAppointmentByDoctorId]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@DoctorId", doctorId);

                        using (SqlDataReader reader = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                while (await reader.ReadAsync())
                                {
                                    Appointment data = new Appointment();
                                    data.Id = Convert.ToInt32(reader["Id"]);
                                    data.DoctorId = Convert.ToInt32(reader["DoctorId"]);
                                    data.UserId = Convert.ToInt32(reader["UserId"]);
                                    data.InsertDate = Convert.ToDateTime(reader["InsertDate"]);
                                    data.StartDate = Convert.ToDateTime(reader["StartDate"]);
                                    data.EndDate = Convert.ToDateTime(reader["EndDate"]);

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

        public async Task<ProjectResult<List<Appointment>>> Create(Appointment appointment)
        {
            ProjectResult<List<Appointment>> result = new ProjectResult<List<Appointment>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[AppointmentDb_db1234].[Appointment_AddAppointment]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                        sqlCommand.Parameters.AddWithValue("@UserId", appointment.UserId);
                        sqlCommand.Parameters.AddWithValue("@InsertDate", DateTime.Now);
                        sqlCommand.Parameters.AddWithValue("@StartDate", appointment.StartDate);
                        sqlCommand.Parameters.AddWithValue("@EndDate", appointment.EndDate);

                        int effectedRow =  await sqlCommand.ExecuteNonQueryAsync();
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

        public async Task<ProjectResult<List<Appointment>>> Delete(int id)
        {
            ProjectResult<List<Appointment>> result = new ProjectResult<List<Appointment>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[Appointment_DeleteAppointment]", sqlConnection))
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

        public async Task<ProjectResult<List<Appointment>>> Update(Appointment appointment)
        {
            ProjectResult<List<Appointment>> result = new ProjectResult<List<Appointment>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[AppointmentDb_db1234].[Appointment_UpdateAppointment]", sqlConnection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@Id", appointment.Id);
                        command.Parameters.AddWithValue("@DoctorId", appointment.DoctorId);
                        command.Parameters.AddWithValue("@UserId", appointment.UserId);
                        command.Parameters.AddWithValue("@StartDate", appointment.StartDate);
                        command.Parameters.AddWithValue("@EndDate", appointment.EndDate);

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
