using Microsoft.AspNetCore.Mvc;
using Task_Autofficina.Models;
using Task_Autofficina.Services;

namespace Task_Autofficina.Controllers
{
    [ApiController]
    [Route("api/veicoli")]
    public class VeicoloController : Controller
    {
        private readonly VeicoloService _service;
        private readonly ClienteService _clienteService;


        public VeicoloController(VeicoloService service, ClienteService clienteService)
        {
            _service = service;
            _clienteService = clienteService;
        }

        //------------------------READ---------------------

        [HttpGet("{varCodice}")]
        public ActionResult<VeicoloDTO?> CercaPerCodice(string varCodice)
        {
            if (string.IsNullOrWhiteSpace(varCodice))
                return BadRequest();

            VeicoloDTO? risultato = _service.Cerca(varCodice);
            if (risultato is not null)
                return Ok(risultato);

            return NotFound();
        }

        [HttpGet]
        public ActionResult<List<VeicoloDTO>> StampaVeicoli()
        {

            List<VeicoloDTO> risultato = _service.Lista();

            if (risultato is not null)
                return Ok(risultato);

            return NotFound();
        }

        //--------------------------------CREATE-----------------------

        [HttpPost]
        public ActionResult<VeicoloDTO?> InserisciVeicolo(string varCodiceCli, string varTar, string varMod, string varMar, DateOnly varAnnoImm, decimal varPrezzoInt, string varStatoInt, DateOnly varDataIng)
        {
            if (varTar.Trim() is not null && varMod.Trim() is not null && varMar.Trim() is not null && varAnnoImm != default && varPrezzoInt >= 0 && varStatoInt.Trim() is not null && varDataIng != default)
            {
                ClienteDTO? cliente = _clienteService.Cerca(varCodiceCli);
                if (cliente is not null)
                {
                    VeicoloDTO risultato;
                    risultato = new VeicoloDTO()
                    {
                        
                        Cod = null,
                        Tar = varTar,
                        Mod = varMod,
                        Mar = varMar,
                        AnnoImm = varAnnoImm,
                        Prez = varPrezzoInt,
                        StatoInt = varStatoInt,
                        DataIng = varDataIng,
                        Clie = cliente
                    };
                    if (_service.Inserisci(risultato))
                    {
                        return Ok();
                    }
                    else { return BadRequest(); }
                }
                else
                {
                    return NotFound("Cliente non trovato.");
                }
            }
            else
            {
                return BadRequest();
            }

        }

        //------------------------DELETE----------------------

        [HttpDelete("{varCodice}")]
        public IActionResult EliminaVeicolo(string varCodice)
        {
            if (varCodice.Trim() is not null)
            {
                VeicoloDTO risultato;
                risultato = new VeicoloDTO()
                {
                    Cod = varCodice
                };
                if (_service.Elimina(risultato))
                {
                    return Ok();
                }
                else { return BadRequest(); }
            }
            else
            {
                return BadRequest();
            }
        }

        //-----------------------------UPDATE-----------------------

        [HttpPut]
        public ActionResult<VeicoloDTO?> UpdateVeicolo(string varCod, string? varTar, string? varMod, string? varMar, DateOnly varAnnoImm, decimal varPrezzoInt, string? varStatoInt, DateOnly varDataIng, DateOnly varDataUsc)
        {

            if ((string.IsNullOrWhiteSpace(varCod) )||
        (string.IsNullOrWhiteSpace(varTar) && string.IsNullOrWhiteSpace(varMod) && string.IsNullOrWhiteSpace(varMar) 
        && varAnnoImm != default && varPrezzoInt >= 0 && !string.IsNullOrWhiteSpace(varStatoInt) && varDataIng != default && varDataUsc != default))
            {
                return BadRequest();
            }
            else
            {
                if (varDataUsc != default)
                {
                    varStatoInt = "Completato";
                }
                VeicoloDTO risultato = new VeicoloDTO()
                {

                    Cod = varCod,
                    Tar = varTar,
                    Mod = varMod,
                    Mar = varMar,
                    AnnoImm = varAnnoImm,
                    Prez = varPrezzoInt,
                    StatoInt = varStatoInt,
                    DataIng = varDataIng,
                    DataUsc = varDataUsc,
                };
                if (_service.Aggiorna(risultato))
                {
                    return Ok();
                }
                else { return BadRequest(); }
            }

        }
    }
}
