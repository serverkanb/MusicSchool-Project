using AppCore.Business.Services.Bases;
using AppCore.Results;
using AppCore.Results.Bases;
using Business.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IClassroomService : IService<ClassroomModel>
    {
    
    }

    public class ClassroomService : IClassroomService
    {
        private readonly ClassroomRepoBase _classroomRepo;

        public ClassroomService(ClassroomRepoBase classroomRepo)
        {
            _classroomRepo = classroomRepo;
        }

        public Result Add(ClassroomModel model)
        {
            if (_classroomRepo.Exists(c => c.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Classroom with same name exist!");
            Classroom classroom = new Classroom()
            {
                Name= model.Name,
                Id = model.Id
            };
            _classroomRepo.Add(classroom);
            return new SuccessResult("Classroom added succesfully");
        }

        public Result Delete(int id)
        {
			Classroom existingClassroom = _classroomRepo.Query().SingleOrDefault(c => c.Id == id);
			if (existingClassroom == null)
			{
				return new ErrorResult("Record not found!");
			}
			_classroomRepo.Delete(id);
			return new SuccessResult("Record deleted successfully.");
		}

        public void Dispose()
        {
            _classroomRepo.Dispose();
        }

        public IQueryable<ClassroomModel> Query()
        {
			return _classroomRepo.Query().Select(c => new ClassroomModel()
			{
                Name= c.Name,
                Id = c.Id

			});
		}

        public Result Update(ClassroomModel model)
        {
			if (_classroomRepo.Exists(p => p.Name.ToLower() == model.Name.ToLower().Trim() ))
				return new ErrorResult("Classroom with same name exists!");

			Classroom entity = new Classroom()
			{
                Name= model.Name,
                Id= model.Id
			
			};
			_classroomRepo.Update(entity);
			return new SuccessResult("Classroom updated sucessfully.");
		}
    }
}
