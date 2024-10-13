using Microsoft.EntityFrameworkCore;
using Task_Autofficina.Models;

namespace Task_Autofficina.Repos
{
    public class ClienteRepo : IRepo<Cliente>
    {
        private readonly AutofficinaContext _context;

        public ClienteRepo(AutofficinaContext context)
        {
            _context = context;
        }
        public bool Create(Cliente entity)
        {
            bool risultato = false;
            try
            {
                _context.Clienti.Add(entity);
                _context.SaveChanges();

                risultato = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return risultato;
        }

        public bool Delete(Cliente entity)
        {
            throw new NotImplementedException();
        }

        public bool Elimina(Cliente varCliente)
        {
            bool risultato = false;
            try
            {
                var clienteFind = _context.Clienti.SingleOrDefault(s => s.Codice == varCliente.Codice);
                if (clienteFind != null)
                {
                    _context.Clienti.Remove(clienteFind);
                    _context.SaveChanges();
                    risultato = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                risultato = false;
            }
            return risultato;

        }

        public Cliente? Get(int id)
        {
            return _context.Clienti.FirstOrDefault(v => v.ClienteID == id);
        }
        public Cliente? GetByCodice(string cod)
        {
            return _context.Clienti.FirstOrDefault(v => v.Codice == cod);
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _context.Clienti.ToList();
        }

        public bool Update(Cliente entity)
        {
            bool risultato = false;
            try
            {
                _context.Update(entity).State = EntityState.Modified;
                _context.SaveChanges();

                risultato = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return risultato;
        }
    }
}
