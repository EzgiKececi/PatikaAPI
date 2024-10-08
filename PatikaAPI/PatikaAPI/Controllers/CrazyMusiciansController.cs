using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatikaAPI.Entities;

namespace PatikaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {

        public static List<CrazyMusicianEntity> _crazyMusicians = new List<CrazyMusicianEntity>()
        {
            new CrazyMusicianEntity {Id=1, Name="Ahmet", LastName="Çalgı", Job="Ünlü Çalgı Çalar", FunnyTrait="Her zaman yanlış nota çalar." },
            new CrazyMusicianEntity {Id=2, Name="Zeynep", LastName="Melodi", Job="Popüler Melodi Yazarı", FunnyTrait="Şarkıları yanlış anlaşılır." },
            new CrazyMusicianEntity {Id=3, Name="Cemil", LastName="Akor", Job="Çılgın Akorist", FunnyTrait="Akorları sık değiştirir." },
            new CrazyMusicianEntity {Id=4, Name="Fatma", LastName="Nota", Job="Sürpriz Nota Üreticisi", FunnyTrait="Nota üretirken sürekli sürprizler hazırlar." },
        };

        [HttpGet]  //Listedeki tüm verileri getirmek için
        public IActionResult GetAll()
        {
            return Ok(_crazyMusicians); 
        }

        [HttpGet("{id:int:min(1)}")] // İstenilen Id değerine sahip veriyi getirmek için
        public IActionResult Get(int id)
        {
            var musician = _crazyMusicians.FirstOrDefault(x=>x.Id ==id); //Parametre olarak gönderilen id eşleşen Id'yi bulma

            return Ok(musician);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string keyword)
        {

            var lowerKeyword = keyword.ToLower();
            
            var musicians = _crazyMusicians.Where(x=>x.Name.ToLower().Contains(lowerKeyword) || x.LastName.ToLower().Contains(lowerKeyword) || x.Job.ToLower().Contains(lowerKeyword)).ToList();

            if(musicians.Count == 0)   
                return NotFound();

            return Ok(musicians);

        }

        [HttpPost] // Yeni veri oluşturmak için
        public IActionResult Create([FromBody] CrazyMusicianEntity newMusician)
        {
            var id = _crazyMusicians.Max(x=> x.Id) +1 ; // Oluşuturulacak yeni nesnenin Id'si otomatik olarak oluşturulacak ve en sonuncu değere +1 eklenecek

            newMusician.Id = id;

            _crazyMusicians.Add(newMusician);

            return CreatedAtAction(nameof(Get), new { id = newMusician.Id }, newMusician); // Get isimli metoda yönlendirme

        }

        [HttpPut("{id:int:min(1)}")] // Varolan veriyi tamamen güncelleme
        public IActionResult Put(int id, [FromBody] CrazyMusicianEntity request)
        {
            if (!ModelState.IsValid) // Requirement kontrolü
                return BadRequest();


            if (request is null || id != request.Id) // Parametre olarak alınan Id yok ise BadRequest dönecek
                return BadRequest();

            var musician = _crazyMusicians.FirstOrDefault(x=>x.Id==id); // İstenilen Id ile eşleşen veriyi bulma

            if(musician == null) // Veri yoksa NotFound döndürme
                return NotFound();

          // Yeni değerlerin atanması
            musician.Id = request.Id;
            musician.Name = request.Name;
            musician.LastName =request.LastName;    
            musician.FunnyTrait = request.FunnyTrait;
            
            return Ok(musician);
        }

        [HttpPatch("{id:int:min(1)}")]
        public IActionResult Patch(int id, [FromBody] CrazyMusicianEntity newTrait)
        {       

            var musician = _crazyMusicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
                return NotFound();

            musician.FunnyTrait= newTrait.FunnyTrait;

            return Ok(musician);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var musician = _crazyMusicians.FirstOrDefault( x => x.Id == id);

            if(musician is null)
                return NotFound();

            musician.IsDeleted = true;
            return Ok(musician);
        }


           


    }
}
