namespace Task_Autofficina.Services
{
    public interface IService<T>
    {
        List<T> Lista();

        T? Cerca(string varCod);

        bool Inserisci(T varObj);
        bool Aggiorna(T varObj);
        bool Elimina(T varObj);
    }
}
