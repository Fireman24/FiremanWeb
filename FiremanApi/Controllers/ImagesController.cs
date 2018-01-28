// //FiremanApi->FiremanApi->ImagesController.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using FiremanApi.DataBase;

using FiremanModel;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiremanApi.Controllers
{
    [Route("[controller]")]
    [EnableCors("CorsPolicy")]
    public class ImagesController:Controller
    {
        private readonly FireContext _dbContext;
        private readonly IHostingEnvironment _appEnvironment;

        private static Dictionary<int, IQueryable<Broadcast>> _videoCache = new Dictionary<int, IQueryable<Broadcast>>();
        private static Dictionary<int,DateTime> _videoCacheLastUpdate = new Dictionary<int, DateTime>();

        public ImagesController(FireContext context, IHostingEnvironment appEnvironment)
        {
            _dbContext = context;
            _appEnvironment = appEnvironment;
        }


        [HttpPost("{fireId}/add")]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile, int fireId)
        {
            if (uploadedFile != null)
            {
                Random rnd = new Random();

                var fileInfo = new FileInfo(uploadedFile.FileName);
                string path;
                do
                {
                    var randPath = new string(Enumerable.Repeat(rnd, 15).Select(x => (char)x.Next(97, 123)).ToArray());
                    path = "/Images/" + randPath + fileInfo.Extension;
                    fileInfo = new FileInfo(path);
                }
                while ( fileInfo.Exists );

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var file = new Image() { Name = uploadedFile.FileName, Url = path };
                _dbContext.Images.Add(file);
                _dbContext.SaveChanges();
                var fire = _dbContext.Fires.Include(f=>f.Images).FirstOrDefault(f=>f.Id == fireId);
                fire.Images.Add(file);
                _dbContext.SaveChanges();
            }

            return Json("Ok");
        }

        [HttpGet("files")]
        public IActionResult GetFiles()
        {
            return Json(_dbContext.Images);
        }

        [HttpGet("{fileName}/get")]
        public IActionResult GetFile(string fileName)
        {
            var filepath = Path.Combine("~/Images", fileName);
            return File(filepath, "application/octet-stream");
        }

        [HttpDelete("{idFile}/remove")]
        public IActionResult RemoveFile(int idFile)
        {
            var image = _dbContext.Images.FirstOrDefault(f => f.Id == idFile);
            if ( image == null )
            {
                return Json("Ошибка!");
            }
            var path = image.Url;
            foreach (var fire in _dbContext.Fires.Include(f=>f.Images))
            {
                fire.Images.Remove(image);
            }
            _dbContext.SaveChanges();
            _dbContext.Images.Remove(image);
            var file = new FileInfo(_appEnvironment.WebRootPath + path);
            if ( file.Exists )
            {
                System.IO.File.Delete(_appEnvironment.WebRootPath + path);
            }
            _dbContext.SaveChanges();
            return Json("Ok");
        }


        [HttpGet("cars/{idCar}/get")]
        public IActionResult GetCarPhoto(string idCar)
        {                                               
            var filepath = Path.Combine("/Images/CarsPhoto", idCar+".jpg");
           
            var file = new FileInfo("wwwroot/Images/CarsPhoto/"+ idCar + ".jpg");
            if ( file.Exists )
            {
                return File(filepath, "image/jpeg");
            }
            return File(Path.Combine("/Images/CarsPhoto", "none.jpg"), "image/jpeg");
        }



        [HttpGet("video/{idCar}/{video}")]
        public IActionResult GetVideo(int idCar, string video)
        {
            var filepath = video;

            var file = new FileInfo("wwwroot"+video);
            if (file.Exists)
            {
                return File(filepath, "image/jpeg");
            }
            return File(Path.Combine("/Images/CarsPhoto", "none.jpg"), "image/jpeg");
        }

       

    }
}
