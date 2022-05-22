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
    public class DoctorManager
    {
        private static readonly object Lock = new object();
        private static volatile DoctorManager _instance;

        public static DoctorManager Instance
        {
            get
            {
                if(_instance==null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DoctorManager();
                        }
                    }
                }
                return _instance;
            }
        }

       

        public async Task<ProjectResult<List<Doctor>>> GetAll()
        {
            ProjectResult<List<Doctor>> result = new ProjectResult<List<Doctor>>();
            List<Doctor> datas = new List<Doctor>();

            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[dbo].[GetAllDoctors]",sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();
                        using (SqlDataReader rdr = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (rdr.HasRows)
                            {
                                while (await rdr.ReadAsync())
                                {
                                    Doctor doctor = new Doctor();
                                    doctor.Id = Convert.ToInt32(rdr["Id"]);
                                    doctor.Name = Convert.ToString(rdr["Name"]);
                                    doctor.Surname = Convert.ToString(rdr["Surname"]);
                                    doctor.Branch = Convert.ToString(rdr["Branch"]);

                                    datas.Add(doctor);

                                }
                            }
                            await rdr .CloseAsync();
                        }
                        result.Data = datas;
                        await sqlCommand .DisposeAsync();
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
            result =await Task.Run(() => result);
            return result;
        }

        public async Task<ProjectResult<List<Doctor>>> Create(Doctor entity)
        {
            ProjectResult<List<Doctor>> result = new ProjectResult<List<Doctor>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand("[dbo].[CreateDoctor]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                        sqlCommand.Parameters.AddWithValue("@Surname", entity.Surname);
                        sqlCommand.Parameters.AddWithValue("@Branch", entity.Branch);
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


        public async Task<ProjectResult<List<Doctor>>> GetById(int id)
        {
            List<Doctor> doctors = new List<Doctor>();
            ProjectResult<List<Doctor>> result = new ProjectResult<List<Doctor>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommand = new SqlCommand("[dbo].[GetDoctor]", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Clear();

                        sqlCommand.Parameters.AddWithValue("@Id", id);

                        using (SqlDataReader read = await sqlCommand.ExecuteReaderAsync())
                        {
                            if (read.HasRows)
                            {
                                while (await read.ReadAsync())
                                {
                                    Doctor doctor = new Doctor();
                                    doctor.Id = Convert.ToInt32(read["Id"]);
                                    doctor.Name = read["Name"].ToString();
                                    doctor.Surname = read["Surname"].ToString();
                                    doctor.Branch = read["Branch"].ToString();

                                    doctors.Add(doctor);
                                }
                            }
                            await read.CloseAsync();
                        }
                        result.Data = doctors;
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

        public async Task<ProjectResult<List<Doctor>>> Delete(int id)
        {
            ProjectResult<List<Doctor>> result = new ProjectResult<List<Doctor>>();
            SqlConnection sqlConnection = null;

            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection .CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommandCheck = new SqlCommand("[dbo].[GetDoctor]", sqlConnection))
                    {
                        sqlCommandCheck.CommandType = CommandType.StoredProcedure;
                        sqlCommandCheck.Parameters.Clear();

                        sqlCommandCheck.Parameters.AddWithValue("@Id", id);
                        int results = Convert.ToInt32(sqlCommandCheck.ExecuteScalar());
                        await sqlCommandCheck.DisposeAsync();

                        if (results != 0)
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("[dbo].[DeleteDoctor]", sqlConnection))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.Clear();

                                sqlCommand.Parameters.AddWithValue("@Id", id);

                                await sqlCommand.ExecuteNonQueryAsync();
                                await sqlConnection.CloseAsync();
                            }
                        }
                        else
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


        public async Task<ProjectResult<List<Doctor>>> Update(Doctor entity)
        {
            ProjectResult<List<Doctor>> result = new ProjectResult<List<Doctor>>();
            SqlConnection sqlConnection = null;
            try
            {
                using (sqlConnection = new SqlConnection(AppConfiguration.ConnectionString()))
                {
                    if (sqlConnection.State == ConnectionState.Broken)
                        await sqlConnection.CloseAsync();
                    if (sqlConnection.State == ConnectionState.Closed)
                        await sqlConnection.OpenAsync();

                    using (SqlCommand sqlCommandCheck = new SqlCommand("[dbo].[GetDoctor]", sqlConnection))
                    {
                        sqlCommandCheck.CommandType = CommandType.StoredProcedure;
                        sqlCommandCheck.Parameters.Clear();

                        sqlCommandCheck.Parameters.AddWithValue("@Id", entity.Id);
                        int results = Convert.ToInt32(sqlCommandCheck.ExecuteScalar());
                        await sqlCommandCheck.DisposeAsync();

                        if (results != 0)
                        {
                            using (SqlCommand sqlCommand = new SqlCommand("[dbo].[UpdateDoctor]", sqlConnection))
                            {
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.Clear();

                                sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                                sqlCommand.Parameters.AddWithValue("@Name", entity.Name);
                                sqlCommand.Parameters.AddWithValue("@Surname", entity.Surname);
                                sqlCommand.Parameters.AddWithValue("@Branch", entity.Branch);
                                await sqlCommand.ExecuteNonQueryAsync();

                                await sqlCommand.DisposeAsync();

                            }
                        }
                        else
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
