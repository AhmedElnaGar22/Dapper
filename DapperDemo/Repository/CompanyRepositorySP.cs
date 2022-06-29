using Dapper;
using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperDemo.Repository
{
    public class CompanyRepositorySP : ICompanyRepository
    {
        private IDbConnection db;
        public CompanyRepositorySP(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Add(Company company)
        {
            var paramters = new DynamicParameters();
            paramters.Add("@CompanyId", 0, DbType.Int32, direction: ParameterDirection.Output);
            paramters.Add("@Name", company.Name);
            paramters.Add("@Address", company.Address);
            paramters.Add("@City", company.City);
            paramters.Add("@State", company.State);
            paramters.Add("@PostalCode", company.PostalCode);

            this.db.Execute("usp_AddCompany", paramters, commandType: CommandType.StoredProcedure);
            company.CompanyId = paramters.Get<int>("@CompanyId");

            return company;
        }

        public Company Find(int id)
        {
            return db.Query<Company>("usp_GetCompany",new { CompanyId = id}, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public List<Company> GetAll()
        {
            return db.Query<Company>("usp_GetALLCompany", commandType: CommandType.StoredProcedure).ToList();
        }

        public void Remove(int id)
        {
             db.Execute("usp_RemoveCompany", new { CompanyId = id }, commandType: CommandType.StoredProcedure);
        }

        public Company Update(Company company)
        {
            var paramters = new DynamicParameters();
            paramters.Add("@CompanyId", company.CompanyId, DbType.Int32);
            paramters.Add("@Name", company.Name);
            paramters.Add("@Address", company.Address);
            paramters.Add("@City", company.City);
            paramters.Add("@State", company.State);
            paramters.Add("@PostalCode", company.PostalCode);

            this.db.Execute("usp_UpdateCompany", paramters, commandType: CommandType.StoredProcedure);

            return company;
        }
    }
}
