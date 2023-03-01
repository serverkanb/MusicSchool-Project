using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IStudentService : IService<StudentModel>
    {

    }
    public class StudentService : IStudentService
    {
        private readonly StudentRepoBase _studentRepo;


        public StudentService(StudentRepoBase studentRepo)
        {
            _studentRepo = studentRepo;
        }

        public Result Add(StudentModel model)
        {
            if (_studentRepo.Exists(p => p.Name.ToLower() + p.Surname.ToLower() == model.Name.ToLower().Trim() + model.Surname.ToLower().Trim()))
                return new ErrorResult("Student with same name exist!");

            Student student = new Student()
            {

                Name = model.Name,
                Surname = model.Surname,
                Birthday = model.Birthday,
                Gender = model.Gender,
                ParentName = model.ParentName,
                ParentSurname = model.ParentSurname,
                RegistrationDate = model.RegistrationDate,
                TakenLessonCount = model.TakenLessonCount,
                

                StudentContact = new StudentContact()
                {
                    Address = model.StudentContact.Address.Trim(),
                    Email = model.StudentContact.Email.Trim(),

                    CountryId = model.StudentContact.CountryId.Value,
                    CityId = model.StudentContact.CityId.Value
                }
            };
            _studentRepo.Add(student);
            return new SuccessResult("Student added succesfully.");
        }

        public Result Delete(int id)
        {
            Student existingStudent = _studentRepo.Query().SingleOrDefault(p => p.Id == id);
            if (existingStudent == null)
            {
                return new ErrorResult("Record not found!");
            }
            var contact = _studentRepo.Query(s => s.StudentContact).ToList();
            //contact.
            _studentRepo.Delete(id);

            return new SuccessResult("Record deleted successfully.");
        }



        public void Dispose()
        {
            _studentRepo.Dispose();
        }

        public IQueryable<StudentModel> Query()
        {
            return _studentRepo.Query().Select(s => new StudentModel()
            {
                Name = s.Name,
                Surname = s.Surname,
                Birthday = s.Birthday,
                Id = s.Id,
                ParentName = s.ParentName,
                ParentSurname = s.ParentSurname,
                Gender = s.Gender,
                RegistrationDate = s.RegistrationDate,
                TakenLessonCount = s.TakenLessonCount,
                CityNameDisplay = s.StudentContact.City.Name,
                CountryNameDisplay = s.StudentContact.Country.Name,
                StudentContact = new StudentContactModel()
                {
                    Address = s.StudentContact.Address,
                    Email = s.StudentContact.Email,
                    CountryId = s.StudentContact.CountryId,
                    CityId = s.StudentContact.CityId
                }
            });
        }
        public Result Update(StudentModel model)
        {
            if (_studentRepo.Exists(s => s.Name.ToLower() + s.Surname.ToLower() == model.Name.ToLower().Trim() + model.Surname.ToLower().Trim() && s.Id != model.Id))
                return new ErrorResult("Student with same name&surname exists!");
            _studentRepo.Delete<StudentContact>(sc => sc.StudentId == model.Id);

            Student entity = new Student()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                Birthday = model.Birthday,
                Gender = model.Gender,
                ParentName = model.ParentName,
                ParentSurname = model.ParentSurname,
                RegistrationDate = model.RegistrationDate,
                TakenLessonCount = model.TakenLessonCount,
                StudentContact = new StudentContact()
                {
                    Address = model.StudentContact.Address.Trim(),
                    Email = model.StudentContact.Email.Trim(),
                    CountryId = model.StudentContact.CountryId,
                    CityId = model.StudentContact.CityId

                }
            };
            _studentRepo.Update(entity);
            return new SuccessResult("Student updated sucessfully.");
        }

    }
}
