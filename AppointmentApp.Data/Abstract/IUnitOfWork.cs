using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentApp.Data.Abstract
{
    public interface IUnitOfWork
    {
        IDoctorRepository Doctors { get; }
        IDoctorDetailRepository DoctorDetails { get; }
        IUserRepository Users { get; }
        IAppointmentsRepository Appointments { get; }
        IBlogCommentsRepository BlogComments { get; }
        IBlogRepository Blogs { get; }
        IDoctorVotingRepository DoctorVotings { get; }

    }
}
