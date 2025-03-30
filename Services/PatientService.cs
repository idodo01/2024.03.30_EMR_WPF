using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMR.interfaces;
using EMR.Models;

namespace EMR.Services
{
    internal class PatientService : IDatabase<Patient>
    {
        private readonly EmrdbContext? _emrdbContext;

        public PatientService(EmrdbContext emrdbContext)
        {
            this._emrdbContext = emrdbContext;
        }

        public void Create(Patient entity)
        {
            this._emrdbContext?.Patients.Add(entity);
            this._emrdbContext?.SaveChanges();
        }

        public void Delete(int? id)
        {
            var validData = this._emrdbContext?.Patients.FirstOrDefault(c => c.Id == id);

            if (validData != null)
            {
                this._emrdbContext ?.Patients.Remove(validData);
                this._emrdbContext ?.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public List<Patient>? Get()
        {
            return this._emrdbContext?.Patients.ToList();
        }

        public Patient? GetDetail(int? id)
        {
            var vaildData = this._emrdbContext ?.Patients.FirstOrDefault(c => c.Id == id);

            if (vaildData != null)
            {
                return vaildData;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void Update(Patient entity)
        {
            this._emrdbContext ?.Patients.Update(entity);
            this._emrdbContext ?.SaveChanges();
        }
    }
}
