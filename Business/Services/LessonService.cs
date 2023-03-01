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
    public interface ILessonService : IService<LessonModel>
    {

    }

    public class LessonService : ILessonService
    {
        private readonly LessonRepoBase _lessonRepo;

        public LessonService(LessonRepoBase lessonRepo)
        {
            _lessonRepo = lessonRepo;
        }

        public Result Add(LessonModel model)
        {
            if (_lessonRepo.Exists(l => l.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Lesson with same name exist!");
            Lesson lesson = new Lesson()
            {
                Name = model.Name,
                IsOnline = model.IsOnline

            };
            _lessonRepo.Add(lesson);
            return new SuccessResult("Lesson added succesfully.");
        }

        public Result Delete(int id)
        {
            Lesson existingLesson = _lessonRepo.Query().SingleOrDefault(l => l.Id == id);
            if (existingLesson == null)
            {
                return new ErrorResult("Record not found");
            }
            _lessonRepo.Delete(id);
            return new SuccessResult("Record deleted succesfully");
        }

        public void Dispose()
        {
            _lessonRepo.Dispose();
        }

        public IQueryable<LessonModel> Query()
        {
            return _lessonRepo.Query().Select(l => new LessonModel()
            {
                Id = l.Id,
                Name = l.Name,
                IsOnline = l.IsOnline,
                IsOnlineDisplay = l.IsOnline ? "Online" : "In Class"

            });
        }

            public Result Update(LessonModel model)
            {
                if (_lessonRepo.Exists(l => l.Name.ToLower() == model.Name.ToLower().Trim() && model.IsOnline==l.IsOnline))
                {
                    return new ErrorResult("Lesson with same name exist!");
                }
                Lesson entity = new Lesson()
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsOnline = model.IsOnline
                };
                _lessonRepo.Update(entity);
                return new SuccessResult("Lesson updated succesfully!");
            }
        }
    }

