using AppointmentApp.Data.Abstract;
using AppointmentApp.Data.Concrete.Ado;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentApp.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {

        private AdoDoctorRepository _adoDoctorRepository;
        private AdoDoctorDetailRepository _adoDoctorDetailRepository;
        private AdoUserRepository _adoUserRepository;
        private AdoAppointmentsRepository _adoAppointmentsRepository;
        private AdoBlogCommentsRepository _adoBlogCommentsRepository;
        private AdoBlogRepository _adoBlogRepository;
        private AdoDoctorVotingRepository _adoDoctorVotingRepository;


        public IDoctorRepository Doctors => _adoDoctorRepository ??= new AdoDoctorRepository();

        public IDoctorDetailRepository DoctorDetails => _adoDoctorDetailRepository ??= new AdoDoctorDetailRepository();

        public IUserRepository Users => _adoUserRepository ??= new AdoUserRepository();

        public IAppointmentsRepository Appointments => _adoAppointmentsRepository ??= new AdoAppointmentsRepository();

        public IBlogCommentsRepository BlogComments => _adoBlogCommentsRepository ??= new AdoBlogCommentsRepository();

        public IBlogRepository Blogs => _adoBlogRepository ??= new AdoBlogRepository();

        public IDoctorVotingRepository DoctorVotings => _adoDoctorVotingRepository ??= new AdoDoctorVotingRepository();


    }
}
