using DapperLib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperLib.Interface
{
    public interface IDepartmentsRepository
    {
        IList<Department> GetDepartmentsByQuery();
        Department GetDepartmentsById (int deptId);
        bool AddDepartment(Department department);
        bool UpdateDepartment(Department department);
        bool DeleteDepartment(int deptId);
    }
}
