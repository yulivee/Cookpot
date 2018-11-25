namespace cookpot_al
{
    interface IManager<T> {
        T Create(T);
        IEnumerable<T> Create(IEnumerable<T> );
        T Update(T);
        IEnumerable<T> Update(IEnumerable<T> ); 
        boolean Delete(T);
    }
}
