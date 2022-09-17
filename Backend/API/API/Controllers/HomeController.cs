using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private MathsContext _db;

        public HomeController(MathsContext db)
        {
            _db = db;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var all = _db.Institutiis.ToList();
            return new JsonResult(all);
        }

        [HttpGet("licee")]
        public IActionResult GetLicee()
        {
            var licee = _db.Institutiis.Where(x => x.Categorie == "Liceu").ToList();

            return new JsonResult(licee);
        }

        [HttpGet("academii")]
        public IActionResult GetAcademii()
        {
            var academii = _db.Institutiis.Where(x => x.Categorie == "Facultate").ToList();

            return new JsonResult(academii);
        }
        [HttpGet("scoli")]
        public IActionResult GetScoli()
        {
            var scoli = _db.Institutiis.Where(x => x.Categorie == "Scoala").ToList();

            return new JsonResult(scoli);
        }

        
        [HttpPost("subiecte")]
        public  IActionResult SaveSubiect([FromForm] FileType file)
        {
            Subiecte subjects = new Subiecte();
            using (var ms = new MemoryStream())
            {
                file.file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                // act on the Base64 data
                subjects.Subiect = fileBytes;
            }
            subjects.Nume = file.file.FileName;
            subjects.FileType = file.file.ContentType;
            subjects.An = Int32.Parse(file.year);
            subjects.IdInstitutie = Int32.Parse(file.id);
            _db.Subiectes.Add(subjects);
            _db.SaveChanges();
            //Console.Write(HttpContext.Request.Form.Files[0]);
            return new JsonResult("Subiectul a fost salvat cu success");
        }

        [HttpGet("getsubject")]
        public async Task<IActionResult> GetSubiecte(string id,string year)
        {
            
            var doc =await Task.Run(()=> _db.Subiectes.Where(x => x.IdInstitutie == Int32.Parse(id) && x.An == Int32.Parse(year)).FirstOrDefault());
            if (doc == null)
            {
                return new JsonResult("no pieces");
            }
            else
            {
                using (var fs = new FileStream(doc.Nume, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(doc.Subiect, 0, doc.Subiect.Length);
                    return File(doc.Subiect, doc.FileType, doc.Nume);
                }
            }

        }
        [HttpPost("rezolvari")]
        public IActionResult SaveRezolvare([FromForm] FileType file)
        {
            Rezolvari subjects = new Rezolvari();
            using (var ms = new MemoryStream())
            {
                file.file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                // act on the Base64 data
                subjects.Rezolvare = fileBytes;
            }
            subjects.Nume = file.file.FileName;
            subjects.FileType = file.file.ContentType;
            subjects.An = Int32.Parse(file.year);
            subjects.IdInstitutie = Int32.Parse(file.id);
            _db.Rezolvaris.Add(subjects);
            _db.SaveChanges();
            //Console.Write(HttpContext.Request.Form.Files[0]);
            return new JsonResult("Rezolvarea a fost salvata cu success");
        }

        [HttpGet("getrezolvari")]
        public async Task<IActionResult> GetRezolvari(string id, string year)
        {

            var doc =await Task.Run(() => _db.Rezolvaris.Where(x => x.IdInstitutie == Int32.Parse(id) && x.An == Int32.Parse(year)).FirstOrDefault());
            if (doc == null)
            {
                return new JsonResult("no pieces");
            }
            else
            {
                using (var fs = new FileStream(doc.Nume, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(doc.Rezolvare, 0, doc.Rezolvare.Length);
                    return File(doc.Rezolvare, doc.FileType, doc.Nume);
                }
            }

        }

        [HttpDelete("deletesubiect")]
        public IActionResult DeleteSubiect([FromBody] DeleteParam param)
        {
            var row=_db.Subiectes.Where(x => x.IdInstitutie == Int32.Parse( param.Id) && x.An== Int32.Parse(param.Year)).FirstOrDefault();
            if (row != null)
            {
                _db.Subiectes.Remove(row);
                _db.SaveChanges();
                return new JsonResult("Inregistrarea a fost stearsa cu succses!");
            }
            else
            {
                return new JsonResult("Inregistrarea nu a fost gasita");
            }
        }

        [HttpDelete("deleterezolvare")]
        public IActionResult DeleteRezolvare([FromBody] DeleteParam param)
        {
            var row = _db.Rezolvaris.Where(x => x.IdInstitutie == Int32.Parse(param.Id) && x.An == Int32.Parse(param.Year)).FirstOrDefault();
            if (row != null)
            {
                _db.Rezolvaris.Remove(row);
                _db.SaveChanges();
                return new JsonResult("Inregistrarea a fost stearsa cu succses!");
            }
            else
            {
                return new JsonResult("Inregistrarea nu a fost gasita");
            }
        }


        [HttpPost("about")]
        public IActionResult SaveDetails([FromBody] Detail detalii)
        {
            Detail detail = new Detail();
            detail.Me=detalii.Me.ToString();
            detail.Work=detalii.Work.ToString();
            detail.Experience=detalii.Experience.ToString();
            
            var row = _db.Details.Where(x => x.Id == 1).SingleOrDefault();

            row.Me=detail.Me.ToString();
            row.Work=detail.Work.ToString();
            row.Experience=detail.Experience.ToString();
            _db.SaveChanges();

            return new JsonResult("Modificarile au fost efectuate cu success");
        }

        [HttpGet("getabout")]
        public IActionResult GetDetails()
        {
            var row=_db.Details.FirstOrDefault();
            return new JsonResult(row);
        }
    }
}
