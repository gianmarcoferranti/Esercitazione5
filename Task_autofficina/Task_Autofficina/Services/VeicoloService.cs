using Task_Autofficina.Models;
using Task_Autofficina.Repos;

namespace Task_Autofficina.Services
{
    public class VeicoloService : IService<VeicoloDTO>
    {
        private readonly VeicoloRepo _repository;
        private readonly ClienteService _clienteService;


        public VeicoloService(VeicoloRepo repository, ClienteService clienteService)
        {
            _repository = repository;
            _clienteService = clienteService;
        }
        public bool Aggiorna(VeicoloDTO varVeicolo)
        {
            bool risultato = false;

            if (varVeicolo != null && !string.IsNullOrWhiteSpace(varVeicolo.Cod))
            {
                var veicoloRitorno = _repository.GetByCodice(varVeicolo.Cod);
                if (veicoloRitorno != null)
                {
                    if (!string.IsNullOrWhiteSpace(varVeicolo.Tar))
                    {
                        veicoloRitorno.Targa = varVeicolo.Tar;
                    }
                    if (!string.IsNullOrWhiteSpace(varVeicolo.Mod))
                    {
                        veicoloRitorno.Modello = varVeicolo.Mod;
                    }

                    if (!string.IsNullOrWhiteSpace(varVeicolo.Mar))
                    {
                        veicoloRitorno.Marca = varVeicolo.Mar;
                    }
                    if (varVeicolo.AnnoImm is not null)
                    {
                        veicoloRitorno.AnnoImmatricolazione = varVeicolo.AnnoImm;
                    }
                    if (varVeicolo.Prez != 0)
                    {
                        veicoloRitorno.PrezzoIntervento = varVeicolo.Prez;
                    }
                    if (!string.IsNullOrWhiteSpace(varVeicolo.StatoInt))
                    {
                        veicoloRitorno.StatoIntervento = varVeicolo.StatoInt;
                    }
                    if (varVeicolo.DataIng is not null)
                    {
                        veicoloRitorno.DataIngresso = varVeicolo.DataIng;
                    }
                    if (varVeicolo.DataUsc is not null)
                    {
                        veicoloRitorno.DataUscita = varVeicolo.DataUsc;
                    }

                    if (_repository.Update(veicoloRitorno))
                    {
                        risultato = true;
                    }
                }
            }

            return risultato;
        }

        public VeicoloDTO? Cerca(string varCod)
        {
            VeicoloDTO? risultato = null;

            Veicolo? veicolo = _repository.GetByCodice(varCod);
            if (veicolo is not null)
            {
                ClienteDTO? cliente = _clienteService.CercaPerId(veicolo.ClienteRif);

                risultato = new VeicoloDTO()
                {
                    Cod = veicolo.Codice,
                    Tar = veicolo.Targa,
                    Mod = veicolo.Modello,
                    Mar = veicolo.Marca,
                    AnnoImm = veicolo.AnnoImmatricolazione,
                    Prez = veicolo.PrezzoIntervento,
                    StatoInt = veicolo.StatoIntervento,
                    DataIng = veicolo.DataIngresso,
                    DataUsc = veicolo.DataUscita,
                    Clie = cliente
                };

            }

            return risultato;
        }

        public bool Elimina(VeicoloDTO varVeicolo)
        {
            bool risultato = false;
            if (varVeicolo is not null && varVeicolo.Cod is not null)
            {
                Veicolo? veicolo = null;
                veicolo = new Veicolo()
                {
                    Codice = varVeicolo.Cod
                };
                if (_repository.Elimina(veicolo))
                {
                    risultato = true;
                }
            }
            return risultato;
        }

        public bool Inserisci(VeicoloDTO varVeicolo)
        {
            bool risultato = false;
            if (varVeicolo is not null && varVeicolo.Tar is not null && varVeicolo.Mod is not null && varVeicolo.Mar is not null
                && varVeicolo.AnnoImm != default && varVeicolo.Prez is not null && varVeicolo.StatoInt is not null && varVeicolo.DataIng != default && varVeicolo.Clie.Cod is not null)
            {
                Cliente? cliente = _clienteService.CercaCliente(varVeicolo.Clie.Cod);
                if (cliente is not null)
                {
                    varVeicolo.Cod = Guid.NewGuid().ToString().ToUpper();
                    Veicolo? veicolo = null;
                    veicolo = new Veicolo()
                    {
                        ClienteRif = cliente.ClienteID,
                        Codice = varVeicolo.Cod,
                        Targa = varVeicolo.Tar,
                        Modello = varVeicolo.Mod,
                        Marca = varVeicolo.Mar,
                        AnnoImmatricolazione = varVeicolo.AnnoImm,
                        PrezzoIntervento = varVeicolo.Prez,
                        StatoIntervento = varVeicolo.StatoInt,
                        DataIngresso = varVeicolo.DataIng,
                    };
                    if (_repository.Create(veicolo))
                    {
                        risultato = true;
                    }
                }
                else {
                    throw new Exception("Cliente non trovato.");
                }

            }
            return risultato;
        }

        public List<VeicoloDTO> Lista()
        {
            List<VeicoloDTO> risultato = new List<VeicoloDTO>();

            IEnumerable<Veicolo> elenco = _repository.GetAll();
            foreach (var veicolo in elenco)

                if (veicolo is not null)
                {
                    ClienteDTO? cliente = _clienteService.CercaPerId(veicolo.ClienteRif);

                    VeicoloDTO risultatoTemp;
                    risultatoTemp = new VeicoloDTO()
                    {
                        Cod = veicolo.Codice,
                        Tar = veicolo.Targa,
                        Mod = veicolo.Modello,
                        Mar = veicolo.Marca,
                        AnnoImm = veicolo.AnnoImmatricolazione,
                        Prez = veicolo.PrezzoIntervento,
                        StatoInt = veicolo.StatoIntervento,
                        DataIng = veicolo.DataIngresso,
                        DataUsc = veicolo.DataUscita,
                        Clie = cliente
                    };
                    risultato.Add(risultatoTemp);
                }

            return risultato;
        }
    }
}
