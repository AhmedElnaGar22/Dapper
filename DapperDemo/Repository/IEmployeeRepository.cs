using DapperDemo.Models;

namespace DapperDemo.Repository
{
    public interface IEmployeeRepository
    {
        Employee Find(int id);
        List<Employee> GetAll();

        Employee Add(Employee company);
        Task<Employee> AddAsync(Employee employee);
        Employee Update(Employee company);

        void Remove(int id);
    }
}
