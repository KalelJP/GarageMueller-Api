using GarageMueller.Database;
using GarageMueller.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageMueller.Controllers
{   /// <summary>
    /// API Auto CRUD
    /// </summary>
    [ApiController]
    [Route("[controller]")]

    public class AutoController : ControllerBase
    {
        private readonly GarageContext _context;

        public AutoController(GarageContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }


        /// <summary>
        /// Liefert alle Autos
        /// </summary>
        /// <remarks>Wenn keine Autos in der Datenbank sind wird trozdem Status 200 angezeigt.</remarks>
        /// <response code="200">Collection von Autos</response>
        [HttpGet]
        public IActionResult Get()
        {
            var autos = _context.Autos.ToList();

            if (autos.Count < 1)
            {
                return Ok("Keine Autos in der Datenbank");
            }

            return Ok(autos);
        }

        /// <summary>
        /// Erzeugt ein Auto
        /// </summary>
        /// <remarks>Erzeugt ein neues Auto in der Datenbank</remarks>
        /// <param name="auto">Auto</param>
        /// <response code="200">1 Auto erfolgreich hinzugefuegt</response>
        /// <response code="400">Auto erstellen nicht erfolgreich</response>
        [HttpPost]
        public IActionResult Create(Auto auto)
        {
            if (auto == null)
            {
                return BadRequest();
            }

            //if ("PMW".Equals(auto.Marke))
            //{
            //    ModelState.AddModelError("Automarke", "unbekannte Automarke");
            //}

            //if (ModelState.IsValid == false)
            //{
            //    return BadRequest(ModelState);
            //}
            _context.Autos.Add(auto);
            _context.SaveChanges();

            return Ok("Auto erfolgreich erzeugt");

        }

        /// <summary>
        /// 1 bestehendes Auto ändern
        /// </summary>
        /// <param name="auto">Auto</param>
        /// <response code="200">1 Auto erfolgreich geaendert</response>
        /// <response code="404">Auto anhand des Primary Keys nicht gefunden</response>
        [HttpPut]
        public IActionResult Update(Auto auto)
        {
            Auto autoFromDB = _context.Autos.SingleOrDefault(a => a.Id == auto.Id);

            if (autoFromDB == null)
            {
                return NotFound();
            }

            autoFromDB.Marke = auto.Marke;
            autoFromDB.Modell = auto.Modell;
            autoFromDB.Leistung = auto.Leistung;

            _context.SaveChanges();

            return Ok("Autodaten erfolgreich geändert");
        }

        /// <summary>
        /// Loescht ein bestehendes Auto
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <response code="200">1 Auto erfolgreich geloescht</response>
        /// <response code="404">Auto anhand des Primary Keys nicht gefunden</response>
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            Auto autoFromDB = _context.Autos.SingleOrDefault(a => a.Id == id);

            if (autoFromDB == null)
            {
                return NotFound();
            }

            _context.Autos.Remove(autoFromDB);
            _context.SaveChanges();

            return Ok("Autodaten erfolgreich gelöscht");

        }

        /// <summary>
        /// Liefert 1 Auto
        /// </summary>
        /// <param name="id">Primary Key</param>
        /// <response code="200">1 Auto erfolgreich geliefert</response>
        /// <response code="404">Auto anhand des Primary Keys nicht gefunden</response>
        [HttpGet]
        [Route("GetEinAuto")]
        public IActionResult Get(int id)
        {
            Auto autoFromDB = _context.Autos.SingleOrDefault(a => a.Id == id);

            if (autoFromDB == null)
            {
                return NotFound();
            }

            return Ok(autoFromDB);
        }

        /// <summary>
        /// Aendert 1 Auto
        /// </summary>
        /// <remarks>Aendert auf Wunsch nur Teile der Auto Atribute</remarks>
        /// <param name="id"></param>
        /// <param name="auto"></param>
        /// <response code="200">1 Auto erfolgreich geaendert</response>
        /// <response code="404">Auto anhand des Primary Keys nicht gefunden</response>
        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Auto> auto)
        {
            // 1 Auto aus der Datenbank
            Auto autoFromDB = _context.Autos.SingleOrDefault(a => a.Id == id);

            if (autoFromDB == null)
                return Ok("Kein Auto in der Datenbank mit diesem Primary Key");

            // Clientwerte werden auf das Auto in der DB Uebertragen
            auto.ApplyTo(autoFromDB, ModelState);

            _context.SaveChanges();

            // Test ob ein Fehler aufgetreten ist
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            return Ok(autoFromDB);
        }
    }
}
