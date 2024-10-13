using Microsoft.EntityFrameworkCore;
using Task_Autofficina.Models;

namespace Task_Autofficina.Repos
{
    public class VeicoloRepo : IRepo<Veicolo>
    {
        private readonly AutofficinaContext _context;

        public VeicoloRepo(AutofficinaContext context)
        {
            _context = context;
        }
        public bool Create(Veicolo entity)
        {
            bool risultato = false;
            try
            {
                _context.Veicoli.Add(entity);
                _context.SaveChanges();

                risultato = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return risultato;
        }

        public bool Delete(Veicolo entity)
        {
            throw new NotImplementedException();
        }

        public bool Elimina(Veicolo varVeicolo)
        {
            bool risultato = false;
            try
            {
                var veicoloFind = _context.Veicoli.SingleOrDefault(s => s.Codice == varVeicolo.Codice);
                if (veicoloFind != null)
                {
                    _context.Veicoli.Remove(veicoloFind);
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

        public Veicolo? Get(int id)
        {
            throw new NotImplementedException();
        }
        public Veicolo? GetByCodice(string cod)
        {
            return _context.Veicoli.FirstOrDefault(v => v.Codice == cod);
        }

        public IEnumerable<Veicolo> GetAll()
        {
            return _context.Veicoli.ToList();
        }

        public bool Update(Veicolo entity)
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
