using AppCore.Business.Services.Bases;
using AppCore.Results.Bases;
using AppCore.Results;
using Business.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace Business.Services
{


    public interface IInstrumentService : IService<InstrumentModel>
    {
        Result DeleteImage(int id);

    }

    public class InstrumentService : IInstrumentService
    {
        private readonly InstrumentRepoBase _instrumentRepo;

        public InstrumentService(InstrumentRepoBase instrumentRepo)
        {
            _instrumentRepo = instrumentRepo;
        }

        public Result Add(InstrumentModel model)
        {

            if (_instrumentRepo.Exists(i => i.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("instrument with same name exists!");


            Instrument entity = new Instrument()
            {
                Id=model.Id,
                Name = model.Name.Trim(),
                StockAmount = model.StockAmount.Value,
                UnitPrice = model.UnitPrice.Value,
                Image = model.Image,
                ImageExtension = model.ImageExtension?.ToLower()

            };
            _instrumentRepo.Add(entity);
            return new SuccessResult("Instrument added successfully.");
        }

        public Result Delete(int id)
        {

            Instrument existingInstrument = _instrumentRepo.Query().SingleOrDefault(i => i.Id == id);
            if (existingInstrument == null)
            {
                return new ErrorResult("Record not found!");
            }
            _instrumentRepo.Delete(id);
            return new SuccessResult("Record deleted successfully.");
        }
        public Result DeleteImage(int id)
        {
            var entity = _instrumentRepo.Query(i => i.Id == id).SingleOrDefault();
            entity.Image = null;
            entity.ImageExtension = null;
            _instrumentRepo.Update(entity);
            return new SuccessResult("Product image deleted succesfully");
        }

        public void Dispose()
        {
            _instrumentRepo.Dispose();
        }

        public IQueryable<InstrumentModel> Query()
        {
            return _instrumentRepo.Query().Select(i => new InstrumentModel()
            {
                Id = i.Id,
                Name = i.Name,
                StockAmount = i.StockAmount.Value,
                UnitPrice = i.UnitPrice,
                UnitPriceDisplay=i.UnitPrice.ToString("C2"),
                Image= i.Image,
                ImageExtension= i.ImageExtension, //büyük veri olduğu için bunu obje olarak tutmamız zorunlu değil.
                ImgSrcDisplay=i.Image!=null ? 
                ( 
                i.ImageExtension == ".jpg" || i.ImageExtension == ".jpeg" ? "data:image/jpeg;base64," : "data:image/png;base64,"
                                                                     //data=>srcDisplayde string olarak yakalamak için yaptık.
                ) + Convert.ToBase64String(i.Image):null                 //image null olabilr mi kontrolü
            });
        }
        public Result Update(InstrumentModel model)
        {
            if (_instrumentRepo.Exists(i => i.Name.ToLower() == model.Name.ToLower().Trim() && i.Id != model.Id))
                return new ErrorResult("Instrument with same name exists!");

            Instrument entity = _instrumentRepo.Query().SingleOrDefault(i => i.Id == model.Id);

            entity.Id = model.Id;
            entity.Name = model.Name.Trim();
            entity.StockAmount = model.StockAmount.Value;
            entity.UnitPrice = model.UnitPrice.Value;
            
            if (model.Image != null)
            {
                entity.Image= model.Image;
                entity.ImageExtension = model.ImageExtension.ToLower(); //resimden başka bir yeri
                                                                        //editlediğimizde resmin null dönmemesi için
            }
           
            _instrumentRepo.Update(entity);
            return new SuccessResult("Instrument updated sucessfully.");
        }
    }
}
