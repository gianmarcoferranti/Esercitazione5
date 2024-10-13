using Task_Autofficina.Models;
using Task_Autofficina.Repos;

namespace Task_Autofficina.Services
{
    public class ClienteService : IService<ClienteDTO>
    {
        private readonly ClienteRepo _repository;

        public ClienteService(ClienteRepo repository)
        {
            _repository = repository;
        }
        public bool Aggiorna(ClienteDTO varCliente)
        {
            bool risultato = false;

            if (varCliente != null && !string.IsNullOrWhiteSpace(varCliente.Cod))
            {
                var clienteRitorno = _repository.GetByCodice(varCliente.Cod);
                if (clienteRitorno != null)
                {
                    if (!string.IsNullOrWhiteSpace(varCliente.Nom))
                    {
                        clienteRitorno.Nome = varCliente.Nom;
                    }
                    if (!string.IsNullOrWhiteSpace(varCliente.Cog))
                    {
                        clienteRitorno.Cognome = varCliente.Cog;
                    }

                    if (!string.IsNullOrWhiteSpace(varCliente.Ind))
                    {
                        clienteRitorno.Indirizzo = varCliente.Ind;
                    }
                    if (!string.IsNullOrWhiteSpace(varCliente.Tel))
                    {
                        clienteRitorno.Telefono = varCliente.Tel;
                    }
                    if (!string.IsNullOrWhiteSpace(varCliente.Ema))
                    {
                        clienteRitorno.Email = varCliente.Ema;
                    }

                    if (_repository.Update(clienteRitorno))
                    {
                        risultato = true;
                    }
                }
            }

            return risultato;
        }

        public ClienteDTO? Cerca(string varCod)
        {
            ClienteDTO? risultato = null;

            Cliente? cliente = _repository.GetByCodice(varCod);
            if (cliente is not null)
            {
                risultato = new ClienteDTO()
                {
                    Cod = cliente.Codice,
                    Nom = cliente.Nome,
                    Cog = cliente.Cognome,
                    Ind = cliente.Indirizzo,
                    Tel = cliente.Telefono,
                    Ema = cliente.Email
                };

            }

            return risultato;
        }

        public Cliente? CercaCliente(string varCod)
        {
            Cliente? risultato = null;

            Cliente? cliente = _repository.GetByCodice(varCod);
            if (cliente is not null)
            {
                risultato = new Cliente()
                {
                    ClienteID = cliente.ClienteID,
                    Codice = cliente.Codice,
                    Nome = cliente.Nome,
                    Cognome = cliente.Cognome,
                    Indirizzo = cliente.Indirizzo,
                    Telefono = cliente.Telefono,
                    Email = cliente.Email
                };

            }

            return risultato;
        }

        public ClienteDTO? CercaPerId(int varId)
        {
            ClienteDTO? risultato = null;

            Cliente? cliente = _repository.Get(varId);
            if (cliente is not null)
            {
                risultato = new ClienteDTO()
                {
                    Cod = cliente.Codice,
                    Nom = cliente.Nome,
                    Cog = cliente.Cognome,
                    Ind = cliente.Indirizzo,
                    Tel = cliente.Telefono,
                    Ema = cliente.Email
                };

            }

            return risultato;
        }

        public bool Elimina(ClienteDTO varCliente)
        {
            bool risultato = false;
            if (varCliente is not null && varCliente.Cod is not null)
            {
                Cliente? vid = null;
                vid = new Cliente()
                {
                    Codice = varCliente.Cod
                };
                if (_repository.Elimina(vid))
                {
                    risultato = true;
                }
            }
            return risultato;
        }

        public bool Inserisci(ClienteDTO varCliente)
        {
            bool risultato = false;
            if (varCliente is not null && varCliente.Nom is not null && varCliente.Cog is not null && varCliente.Ind is not null && varCliente.Tel is not null && varCliente.Ema is not null)
            {
                varCliente.Cod = Guid.NewGuid().ToString().ToUpper();
                Cliente? vid = null;
                vid = new Cliente()
                {
                    Codice = varCliente.Cod,
                    Nome = varCliente.Nom,
                    Cognome = varCliente.Cog,
                    Indirizzo = varCliente.Ind,
                    Telefono = varCliente.Tel,
                    Email = varCliente.Ema

                };
                if (_repository.Create(vid))
                {
                    risultato = true;
                }
            }
            return risultato;
        }

        public List<ClienteDTO> Lista()
        {
            List<ClienteDTO> risultato = new List<ClienteDTO>();

            IEnumerable<Cliente> elenco = _repository.GetAll();
            foreach (var cliente in elenco)
                if (cliente is not null)
                {
                    ClienteDTO risultatoTemp;
                    risultatoTemp = new ClienteDTO()
                    {
                        Cod = cliente.Codice,
                        Nom = cliente.Nome,
                        Cog = cliente.Cognome,
                        Ind = cliente.Indirizzo,
                        Tel = cliente.Telefono,
                        Ema = cliente.Email
                    };
                    risultato.Add(risultatoTemp);
                }

            return risultato;
        }
    }
}
