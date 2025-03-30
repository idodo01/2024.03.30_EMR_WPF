using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMR.interfaces;
using EMR.Models;

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

        public List<Staff>? Get()
        {
            return this._emrdbContext?.Staffs.ToList();
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
            this._emrdbContext?.Staffs.Update(entity);
            this._emrdbContext?.SaveChanges();
        }
    }
}
