﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using AppCore.Results.Bases;
using DataAccess.Enum;
using Microsoft.AspNetCore.Authorization;
using AppCore.Results;

//Generated by ScaffoldApp.
namespace MusicSchool.Controllers
{
    public class InstrumentsController : Controller
    {
        // Add service injections here
        private readonly IInstrumentService _instrumentService;

        public InstrumentsController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService;
        }

        // GET: Instruments
        public IActionResult Index()
        {
            var instruments = _instrumentService.Query().ToList();
            return View(instruments);
        }

        // GET: Instruments/Details/5
        public IActionResult Details(int id)
        {
            InstrumentModel instrument = _instrumentService.Query().SingleOrDefault(i => i.Id == id); ; // TODO: Add get item service logic here
            if (instrument == null)
            {
                return View("_Error", "Intrument cannot be found!");
            }
            return View(instrument);
        }

        // GET: Instruments/Create
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View();
        }

        // POST: Instruments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InstrumentModel instrument , IFormFile? uploadedImage)
        {
            if (ModelState.IsValid)
            {
                Result result;
                result = UpdateImage(instrument, uploadedImage);
                if (result.IsSuccessful)
                { 
                result = _instrumentService.Add(instrument);
                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Index));
                    }
                }
                ViewData["Message"] = result.Message;
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(instrument);
        }

        // GET: Instruments/Edit/5
        public IActionResult Edit(int id)
        {
            InstrumentModel instrument = _instrumentService.Query().SingleOrDefault(p => p.Id == id); // TODO: Add get item service logic here
            if (instrument == null)
            {
                return View("_Error", "Intrument cannot be found!");
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(instrument);
        }

        // POST: Instruments/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InstrumentModel instrument,IFormFile? uploadedImage) //validasyona takılmaması için
        {
            if (ModelState.IsValid)
            {
                Result result = UpdateImage(instrument, uploadedImage);
                if (result.IsSuccessful)
                {
                    result = _instrumentService.Update(instrument);

                    if (result.IsSuccessful)
                    {
                        TempData["Message"] = result.Message; // success
                        return RedirectToAction(nameof(Index));
                    } 
                }
                ModelState.AddModelError("", result.Message); // error
            }
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            return View(instrument);
        }

        // GET: Instruments/Delete/5
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            InstrumentModel instrument = _instrumentService.Query().SingleOrDefault(s => s.Id == id);
            if (instrument == null)
            {
                return NotFound();
            }

            return View(instrument);
        }

        // POST: Instruments/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult DeleteConfirmed(int id)
        {
            Result result = _instrumentService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteImage (int id)
        {
        var result=_instrumentService.DeleteImage(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Details),new {id =id});
        }
        private Result UpdateImage (InstrumentModel resultModel,IFormFile uploadedImage )
        {
            Result result = new SuccessResult();

            if (uploadedImage != null && uploadedImage.Length > 0)
            {
                string uploadedFileName = uploadedImage.FileName;
                string uploadedFileExtension = Path.GetExtension(uploadedFileName);

                if (!AppSettings.AcceptedExtensions.Split(',').Any(ae => ae.ToLower
                ().Trim() == uploadedFileExtension.ToLower()))// yüklediğimiz dosya uzantısı bizde varmı?
                    result = new ErrorResult($"Image cant be uploaded because accepted extensions are {AppSettings.AcceptedExtensions}!");
                if (result.IsSuccessful)
                {
                    double acceptedFileLength = AppSettings.AcceptedLength;
                    double acceptedFileLengthInBytes = acceptedFileLength * Math.Pow(1024, 2);

                    if (uploadedImage.Length > acceptedFileLengthInBytes) 
                    {
                        result = new ErrorResult("Image cant be uploaded because acceğted file size is" + AppSettings.AcceptedLength.ToString
                            ("N1") + "!");
                    }
                if (result.IsSuccessful)
                    {
                        using(MemoryStream memoryStream= new MemoryStream())
                        {
                            uploadedImage.CopyTo(memoryStream); //uploaded imagedeki binary veriyi memorystreame kopyaladık
                            resultModel.Image = memoryStream.ToArray();
                            resultModel.ImageExtension = uploadedFileExtension; // özellikleri doldurmasını sağladık

                        }
                    }
                }

            }
            return result;
        }
        [Obsolete("yeni olanı kullan!")]
        public IActionResult DeleteImageEski(int id)
        {
            var result = _instrumentService.DeleteImage(id);
            TempData["Message"]= result.Message;
            return RedirectToAction(nameof(Details),new {id}); //details id parametresi alldığı için route ppar olarak id gönderdik
        }
    }
}
