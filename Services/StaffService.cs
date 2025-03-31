using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMR.Interfaces;
using EMR.Models;
using Microsoft.EntityFrameworkCore;

namespace EMR.Services
{
    internal class StaffService : IDatabase<Staff>
    {
        private readonly EmrdbContext? _emrdbContext;

        public StaffService(EmrdbContext emrdbContext)
        {
            this._emrdbContext = emrdbContext;
        }

        public void Create(Staff entity)
        {
            if (this._emrdbContext != null)
            {
                Debug.WriteLine("_emrdbContext, null아님");
            }
            
            this._emrdbContext?.Staffs.Add(entity);
            this._emrdbContext?.SaveChanges();
        }

        public void Delete(int? id)
        {
            var validData = this._emrdbContext?.Patients.FirstOrDefault(c => c.Id == id);

            if (validData != null)
            {
                this._emrdbContext?.Patients.Remove(validData);
                this._emrdbContext?.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

    
        public async Task<List<Staff>> GetAsync()
        {
            if (this._emrdbContext == null)
            {
                return new List<Staff>();  // null 방지
            }

            return await this._emrdbContext.Staffs.ToListAsync();
        }

        public Staff? GetDetail(int? id)
        {
            var vaildData = this._emrdbContext?.Staffs.FirstOrDefault(c => c.Id == id);

            if (vaildData != null)
            {
                return vaildData;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        public void Update(Staff entity)
        {
            var existingStaff = _emrdbContext.Staffs.FirstOrDefault(s => s.Id == entity.Id);
            if (existingStaff != null)
            {
                existingStaff.Name = entity.Name;
                existingStaff.Department = entity.Department;
                existingStaff.Position = entity.Position;
                existingStaff.Age = entity.Age;
                existingStaff.Email = entity.Email;
                existingStaff.Phone = entity.Phone;

                _emrdbContext.SaveChanges(); // ✅ 변경사항 저장 필수
            }
        }

    }
}
