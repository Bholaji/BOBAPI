using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IUserRepository User {get;}
		IAddressRepository Address { get;}
		IUserContactRepository Contact { get; }
		IUserSocialRepository Social { get; }
		IUserPayrollRepository Payroll { get; }
		IUserFinancialRepository Financial { get; }
		IUserEmploymentInformationRepository EmploymentInformation { get; }
		IOrganizationRepository OrganizationRepository { get; }
		IPostRepository Post { get; }
		ICommentRepository Comment { get; }
		ITaskRepository UserTask { get; }
		IActivityLogRepository ActivityLog { get; }
		ITaskJobRepository TaskJob { get; }
		void BeginTransaction();
		void CommitTransaction();
		void RollbackTransaction();
		Task SaveAsync();
	}
}
