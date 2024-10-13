using Microsoft.AspNetCore.Mvc;
using Task_Autofficina.Models;
using Task_Autofficina.Services;

namespace Task_Autofficina.Controllers
{
    [ApiController]
    [Route("api/clienti")]
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        //------------------------READ---------------------

        [HttpGet("{varCodice}")]
        public ActionResult<ClienteDTO?> CercaPerCodice(string varCodice)
        {
            if (string.IsNullOrWhiteSpace(varCodice))
                return BadRequest();

            ClienteDTO? risultato = _service.Cerca(varCodice);
            if (risultato is not null)
                return Ok(risultato);

            return NotFound();
        }

        [HttpGet]
        public ActionResult<List<ClienteDTO>> StampaClienti()
        {

            List<ClienteDTO> risultato = _service.Lista();

            if (risultato is not null)
                return Ok(risultato);

            return NotFound();
        }

        //--------------------------------CREATE-----------------------

        [HttpPost]
        public ActionResult<ClienteDTO?> InserisciCliente(string varNom, string varCogn, string varIndi, string varTel, string varEma)
        {
            if (varNom.Trim() is not null && varCogn.Trim() is not null && varIndi.Trim() is not null && varTel.Trim() is not null && varEma.Trim() is not null)
            {
                ClienteDTO risultato;
                risultato = new ClienteDTO()
                {
                    Cod = null,
                    Nom = varNom,
                    Cog = varCogn,
                    Ind = varIndi,
                    Tel = varTel,
                    Ema = varEma
                };
                if (_service.Inserisci(risultato))
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

        //------------------------DELETE----------------------

        [HttpDelete("{varCodice}")]
        public IActionResult EliminaUtente(string varCodice)
        {
            if (varCodice.Trim() is not null)
            {
                ClienteDTO risultato;
                risultato = new ClienteDTO()
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
        public ActionResult<ClienteDTO?> UpdateVideoteca(string varCod, string? varNom, string? varCogn, string? varIndi, string? varTel, string? varEma)
        {

            if (varCod.Trim() is null || (varNom is null && varCogn is null && varIndi is null && varTel is null && varEma is null))
            {
                return BadRequest();
            }
            else
            {
                ClienteDTO risultato = new ClienteDTO()
                {
                    Cod = varCod,
                    Nom = varNom,
                    Cog = varCogn,
                    Ind = varIndi,
                    Tel = varTel,
                    Ema = varEma
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
