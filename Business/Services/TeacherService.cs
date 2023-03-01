using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
	public interface ITeacherService : IService<TeacherModel>
	{


	}
	public class TeacherService : ITeacherService
	{
		private readonly TeacherRepoBase _teacherRepo;

		public TeacherService(TeacherRepoBase teacherRepo)
		{
			_teacherRepo = teacherRepo;
		}

		public Result Add(TeacherModel model)
		{
			if (_teacherRepo.Exists(p => p.Name.ToLower() + p.Surname.ToLower() == model.Name.ToLower().Trim() + model.Surname.ToLower().Trim() && p.Id != model.Id))
				return new ErrorResult("Teacher with same name exist!");

			Teacher teacher = new Teacher()
			{
				Name = model.Name,
				Surname = model.Surname,
				Birthday = model.Birthday,
				RegistrationDate = model.RegistrationDate,
				InstrumentId = model.InstrumentId.Value,

				TeacherContact = new TeacherContact()
				{
					Address = model.TeacherContact.Address.Trim(),
					Email = model.TeacherContact.Email.Trim(),
					Gender = model.TeacherContact.Gender
				}
			};
			_teacherRepo.Add(teacher);
			return new SuccessResult("Teacher added succesfully.");
		}


		public Result Delete(int id)
		{
			Teacher existingTeacher = _teacherRepo.Query().SingleOrDefault(p => p.Id == id);
			if (existingTeacher == null)
			{
				return new ErrorResult("Record not found!");
			}
			_teacherRepo.Delete(id);
			return new SuccessResult("Record deleted successfully.");
		}

		public void Dispose()
		{
			_teacherRepo.Dispose();
		}

		public IQueryable<TeacherModel> Query()
		{
			return _teacherRepo.Query().Select(t => new TeacherModel()
			{
				Name = t.Name,
				Surname = t.Surname,
				Birthday = t.Birthday,
				Id = t.Id,
				RegistrationDate = t.RegistrationDate,
				InstrumentId = t.InstrumentId,
				InstrumentDisplay = t.Instrument.Name,

				TeacherContact = new TeacherContactModel()
				{
					Address = t.TeacherContact.Address,
					Email = t.TeacherContact.Email,
					Gender = t.TeacherContact.Gender
				}
			});

		}

		public Result Update(TeacherModel model)
		{
			if (_teacherRepo.Exists(p => p.Name.ToLower() + p.Surname.ToLower() == model.Name.ToLower().Trim() + model.Surname.ToLower().Trim() && p.TeacherContact.Gender==model.TeacherContact.Gender && p.Id != model.Id))
				return new ErrorResult("Teacher with same name&surname exists!");

			//var selectedGender=_teacherRepo.Query(t => t.TeacherContact.Gender).SingleOrDefault(t => t.Id == model.Id);


			_teacherRepo.Delete<TeacherContact>(sc => sc.TeacherId == model.Id);


			Teacher entity = _teacherRepo.Query().SingleOrDefault(t => t.Id == model.Id);

			entity.Id = model.Id;
			entity.Name = model.Name.Trim();
			entity.Surname = model.Surname.Trim();
			entity.Birthday = model.Birthday;
			entity.RegistrationDate = model.RegistrationDate;
			entity.InstrumentId = model.InstrumentId.Value;

			entity.TeacherContact = new TeacherContact()
			{
				Address= model.TeacherContact.Address,
				Email= model.TeacherContact.Email,
				Gender= model.TeacherContact.Gender
			};
			_teacherRepo.Update(entity);
			return new SuccessResult("Teacher updated sucessfully.");
		}
	}
}
